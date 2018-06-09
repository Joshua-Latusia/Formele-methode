using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormeleMethode
{
	// Class used for converting NDFA to DFA
	public class NdfaToDfaConverter
	{
		public static Automata<string> ConvertToDFA(Automata<string> ndfa)
		{
			Automata<string> dfa = new Automata<string>(ndfa.GetAlphabet());
			string combinedStartState = "";
			SortedSet<string> completeStartState = new SortedSet<string>();

			bool isFinalState = false;

			// Create list of startstates (startstates + reachable states via EClosure)
			foreach (string startState in ndfa.startStates)
			{
				List<string> reachableStates = ndfa.EClosure(startState);

				// split each reachable state up in indivual parts if its a combination of states
				foreach (string s in reachableStates)
				{
					string[] states = s.Split('_');
					completeStartState.UnionWith(states);
				}
			}

			// Create a the complete startstate by seperating all the reachable "startstates" with a _
			foreach (string s in completeStartState)
			{
				combinedStartState += s + "_";
				if (ndfa.endStates.Contains(s)) // If 1 of these states is a endstate the combined startstate is an endstate
					isFinalState = true;
			}

			//trim last "_" off of string
			combinedStartState = combinedStartState.TrimEnd('_');

			// Start if the conversion to DFA
			ConvertState(combinedStartState, ref dfa, ref ndfa);

			// Define the only combined startstate
			dfa.DefineAsStartState(combinedStartState);
			if (isFinalState)
				dfa.DefineAsFinalState(combinedStartState);

			// Create all transitions for each symbol to the fuik itself
			if (dfa.states.Contains("Fuik"))
			{
				foreach (char symbol in dfa.GetAlphabet())
				{
					dfa.AddTransition(new Transition<string>("Fuik", symbol, "Fuik"));
				}
			}

			// Do final stuff 
			//return dfa;
			return RemoveStateSeperators(dfa); // return finalized conversion. -> 

		}

		/// <summary>
		/// Finalises the conversion by removing the _ part from each state.
		/// </summary>
		/// <param name="dfa">The merged.</param>
		/// <returns></returns>
		private static Automata<string> RemoveStateSeperators(Automata<string> dfa)
		{
			Automata<string> finaleDFA = new Automata<string>(dfa.symbols);

			foreach (Transition<string> trans in dfa.transitions)
			{
				finaleDFA.AddTransition(new Transition<string>(trans.GetFromState().Replace("_", string.Empty),
															   trans.GetSymbol(),
															   trans.GetToState().Replace("_", string.Empty)));
			}

			foreach (string startState in dfa.startStates)
			{
				finaleDFA.DefineAsStartState(startState.Replace("_", string.Empty));
			}

			foreach (string endState in dfa.endStates)
			{
				finaleDFA.DefineAsFinalState(endState.Replace("_", string.Empty));
			}

			return finaleDFA;
		}

		/// <summary>
		/// Renames the states and transitions states.
		/// </summary>
		public static Automata<string> RenameStates(Automata<string> dfa)
		{
			// key = old name value = new name
			Dictionary<string, string> dictionay = new Dictionary<string, string>();
			int index = 0;

			// Link shorter name to each state
			foreach (string s in dfa.states)
			{
				dictionay.Add(s, $"q{index}");
				index++;
			}

			// Rename the states
			SortedSet<string> newStates = new SortedSet<string>();
			foreach (string state in dfa.states)
			{
				newStates.Add(dictionay[state]);
			}
			dfa.states = newStates;

			// Rename start states
			SortedSet<string> newStartStates = new SortedSet<string>();
			foreach (string startState in dfa.startStates)
			{
				newStartStates.Add(dictionay[startState]);
			}
			dfa.startStates = newStartStates;

			// Rename end states
			SortedSet<string> newEndStates = new SortedSet<string>();
			foreach (string endState in dfa.endStates)
			{
				newEndStates.Add(dictionay[endState]);
			}
			dfa.endStates = newEndStates;

			// Rename trans states
			HashSet<Transition<string>> newTransitions = new HashSet<Transition<string>>();
			foreach (Transition<string> oldTransition in dfa.transitions)
			{
				newTransitions.Add(new Transition<string>(dictionay[oldTransition.GetFromState()], oldTransition.GetSymbol(), dictionay[oldTransition.GetToState()]));
			}
			dfa.transitions = newTransitions;

			return dfa;
		}

		/// <summary>
		/// Converts the state.
		/// </summary>
		/// <param name="currentState">State of the current.</param>
		/// <param name="dfa">The dfa.</param>
		/// <param name="ndfa">The ndfa.</param>
		private static void ConvertState(string currentState, ref Automata<string> dfa, ref Automata<string> ndfa)
		{
			// If state is already done just return
			if (dfa.GetTransitions(currentState).Count == dfa.GetAlphabet().Count)
				return;

			// Split states
			string[] states = currentState.Split('_');

			// Go through all symbols
			foreach (char symbol in dfa.GetAlphabet())
			{
				// Check if already has a route
				if (HasExistingRoutes(currentState, symbol, dfa))
					return;

				int AmountOfCorrectRoutes = CheckAvailableRoutes(states,symbol,ndfa);

				string toState = "";

				// Add transitions to the fuik
				if (AmountOfCorrectRoutes == 0)
				{
					dfa.AddTransition(new Transition<string>(currentState, symbol, "Fuik"));
				}
				else
				{
					bool isFinalState = GenerateToState(ref toState, states, symbol, ndfa);

					dfa.AddTransition(new Transition<string>(currentState, symbol, toState));


					// Define final states
					if (ndfa.endStates.Contains(currentState))
					{
						dfa.DefineAsFinalState(currentState);
					}

					if (isFinalState)
						dfa.DefineAsFinalState(toState);

					// Check if its not a loop transition.
					if (currentState != toState)
						ConvertState(toState, ref dfa, ref ndfa);
				}
			}
		}

		private static bool GenerateToState(ref string toState, string[] states, char symbol, Automata<string> ndfa)
		{
			bool isFinalState = false;

			SortedSet<string> newStates = new SortedSet<string>();

			foreach (string state in states)
			{
				// Get the transitions of the NDFA
				List<Transition<string>> trans = ndfa.GetTransitions(state);

				foreach (Transition<string> t in trans)
				{
					if (t.GetSymbol() == symbol)
					{
						var ep = ndfa.EClosure(t.GetFromState());
						var ep2 = ndfa.EClosure(t.GetToState());

						newStates.UnionWith(ndfa.EClosure(t.GetToState())); // TODO see if this works correct
					}
				}

			}

			foreach (string subState in newStates)
			{
				toState += subState + "_";
				if (ndfa.endStates.Contains(subState))
					isFinalState = true;
			}
			toState = toState.TrimEnd('_');
			return isFinalState;

		}

		/// <summary>
		/// Checks how many routes havent been taken if result is 0 it means it has to go to a fuik.
		/// </summary>
		/// <param name="states">The states.</param>
		/// <param name="symbol">The symbol.</param>
		/// <param name="ndfa">The ndfa.</param>
		/// <returns></returns>
		private static int CheckAvailableRoutes(string[] states, char symbol, Automata<string> ndfa)
		{
			int[] possibleRoutes = new int[states.Length];

			int AmountOfCorrectRoutes = 0;

			foreach (string state in states)
			{
				var ding = ndfa.GetTransitions(state);
				if (ndfa.GetTransitions(state).Count(t => t.GetSymbol() == symbol) > AmountOfCorrectRoutes)
				{
					AmountOfCorrectRoutes = ndfa.GetTransitions(state).Count(t => t.GetSymbol() == symbol);
				}
			}

			return AmountOfCorrectRoutes;
		}

		/// <summary>
		/// Determines whether there is a transition with the current state and the give symbol in the dfa
		/// </summary>
		/// <param name="currentState">State of the current.</param>
		/// <param name="symbol">The symbol.</param>
		/// <param name="dfa">The dfa.</param>
		/// <returns>
		///   <c>true</c> if [has existing routes] [the specified current state]; otherwise, <c>false</c>.
		/// </returns>
		private static bool HasExistingRoutes(string currentState, char symbol, Automata<string> dfa)
		{
			List<Transition<string>> trans = dfa.GetTransitions(currentState);
			foreach (Transition<string> t in trans)
			{
				if (t.GetSymbol() == symbol)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Minimizes the dfa by reverse -> ToDFA -> reverse -> toDFA.
		/// </summary>
		/// <param name="dfa">The dfa.</param>
		/// <returns></returns>
		public static Automata<string> MinimizeDfa(Automata<string> dfa)
		{
			dfa.ReverseAutomata(); // Gaat nog goed
			dfa = ConvertToDFA(dfa);
			dfa.ReverseAutomata();
			return ConvertToDFA(dfa);
		}
	}
}
