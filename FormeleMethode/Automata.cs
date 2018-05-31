using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormeleMethode
{
	/// <summary>
	/// The class Automata represents both DFA and NDFA: some NDFA's are also DFA
	/// Using the method isDFA we can check this
	/// 
	/// We use '$' to denote the empty symbol epsilon
	/// 
	/// By Joshua Latusia
	/// </summary>
	public class Automata<T> where T : IComparable<T>
	{

		private HashSet<Transition<T>> transitions;

		private SortedSet<T> states;
		private SortedSet<T> startStates;
		private SortedSet<T> endStates;
		private SortedSet<char> symbols;

		public Automata() : this(new SortedSet<char>())
		{
		}

		public Automata(char [] s) : this(new SortedSet<char>(s.ToList()))
		{
		}

		public Automata(SortedSet<char> symbols)
		{
			this.transitions = new HashSet<Transition<T>>();
			this.states = new SortedSet<T>();
			this.startStates = new SortedSet<T>();
			this.endStates = new SortedSet<T>();
			this.symbols = symbols;
		}

		public void SetAlphabet(char [] s)
		{
			this.SetAlphabet(new SortedSet<char>(s.ToList()));
		}

		public void SetAlphabet(SortedSet<char> symbols)
		{
			this.symbols = symbols;
		}

		public SortedSet<char> GetAlphabet()
		{
			return symbols;
		}

		public void AddTransition(Transition<T> t)
		{
			transitions.Add(t);
			states.Add(t.GetFromState());
			states.Add(t.GetToState());
		}

		public void DefineAsStartState(T t)
		{
			// if already in states no problem because a Set will remove duplicates.
			states.Add(t);
			startStates.Add(t);
		}

		public void DefineAsFinalState(T t)
		{
			// if already in states no problem because a Set will remove duplicates.
			states.Add(t);
			endStates.Add(t);
		}

		public void PrintTransitions()
		{ 
			foreach(Transition<T> t in transitions)
			{
				Console.WriteLine(t);
			}
		}

		// geefTaalTotLengte
		public void PrintLanguage(int maxLength)
		{
			SortedSet<string> combinations = new SortedSet<string>();

			for (int i = 0; i < maxLength; i++)
			{
				
				// Add per lengte de combinations naar sortedset
			}
		}

		/// <summary>
		/// Check if S is accepted in the language
		/// </summary>
		/// <param name="s">The s.</param>
		/// <returns></returns>
		public bool Accept(string s)
		{
			char[] charArray = s.ToCharArray();

			// Try for all startStates
			foreach (T startState in startStates)
			{
				T currentState = startState;
				Console.WriteLine($"Start State : {currentState}  \nString : {s}");
				CharEnumerator c = s.GetEnumerator();

				// Check all chars
				while (c.MoveNext())
				{
					// search for matching transition with fromState = currenState && symbol = c.Current
					Transition<T> transition = new Transition<T>(currentState, c.Current);
					currentState = GetMatchingState(transition);

					if (currentState == null)
						break;
				}

				// Move to next startState
				if (currentState == null)
					continue;

				// If the last char in given string is a final state -> true else -> false
				return endStates.Contains(currentState);
			}

			// if there was no match found string is invalid
			return false;
		}

		// Returns matching state or null
		public T GetMatchingState(Transition<T> t2)
		{
			foreach (Transition<T> t in transitions)
			{
				if (t2.CompareTo(t) == 1)
				{
					Console.WriteLine(t.ToString());
					return t.GetToState();
				}
			}
			return default(T);
		}

		/// <summary>
		/// Determines whether this instance is dfa.
		/// DFA means all states have 1 Transition for each symbol in the alphabet
		/// </summary>
		public bool IsDFA()
		{
			bool isDFA = true;
			foreach(T from in states)
			{
				foreach(char c in symbols)
				{
					isDFA = isDFA && GetToStates(from, c).Count() == 1;
				}
			}

			return isDFA;
		}

		public List<T> GetToStates(T from, char symbol)
		{
			List<T> toStates = new List<T>();

			// Loop all transitions
			foreach (Transition<T> t in transitions)
			{
				// If a transition has the same from state
				if (t.GetFromState().Equals(from))
				{
					// If the transition contains the input symbol add the To state.
					if (t.GetSymbol() == symbol)
					{
						toStates.Add(t.GetToState());
					}
				}
			}
			return toStates;
		}

		/// <summary>
		/// Generates a graphfile.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		public void GenerateGraphFile(string fileName)
		{
			List<string> lines = new List<string>();

			// Generate start of the graph file
			lines.Add("digraph { ");
			lines.Add(" \"\" [shape=none]");

			// Generate end states
			foreach (T endState in endStates)
			{
				lines.Add(GetEndStateString(endState));
			}

			// Empty line
			lines.Add("");

			// Generate startstate transitions
			foreach (T startState in startStates)
			{
				lines.Add(GetStartStateString(startState));
			}

			// Generate The different transitions
			foreach (Transition<T> t in transitions)
			{
				lines.Add(GetTransitionString(t));
			}
			// Generate end of the graph file
			lines.Add("}");

			// Write all lines
			System.IO.File.WriteAllLines($@".\..\..\graphviz\dotfiles\{fileName}.dot", lines);
		}

		/// <summary>
		/// Gets the transition string.
		/// </summary>
		/// <param name="t">The t.</param>
		/// <returns></returns>
		public string GetTransitionString(Transition<T> t)
		{
			return $" \"{t.GetFromState()}\" -> \"{t.GetToState()}\"[label=\"{t.GetSymbol()}\", weight=\"{t.GetSymbol()}\"]; ";
		}

		/// <summary>
		/// Appends the state to the dotfile string.
		/// </summary>
		/// <param name="String">The string.</param>
		/// <returns></returns>
		public string GetStartStateString(T startState)
		{
			return $" \"\" -> \"{startState.ToString()}\"";
		}

		/// <summary>
		/// Appends the end state to the dotfile.
		/// </summary>
		/// <param name="s">The s.</param>
		/// <returns></returns>
		public string GetEndStateString(T endState)
		{
			return $" \"{endState.ToString()}\" [shape=doublecircle]";
		}

		/// <summary>
		/// Reverses the DFA or NDFA.
		/// All transitions are reversed
		/// Start states and end states switch
		/// </summary>
		public void ReverseAutomata()
		{
			// Reverse Start and endstates
			SortedSet<T> tempStates = startStates;

			startStates = endStates;
			endStates = tempStates;

			// Reverse Transitions
			HashSet<Transition<T>> tempTransitions = new HashSet<Transition<T>>();
			foreach (Transition<T> transition in transitions)
			{
				tempTransitions.Add(new Transition<T>(transition.GetToState(),transition.GetSymbol(),transition.GetFromState()));
			}

			transitions = tempTransitions;
		}

		/// <summary>
		/// Returns a list with states that can be reached from given state via epsilons
		/// </summary>
		/// <param name="state">The state.</param>
		/// <returns></returns>
		public List<T> EClosure(T state)
		{
			if (!IsDFA())
			{
				// Loop through transitions untill transition.from == state

				// Check if symbol == epsilon

				// Add to list

				return new List<T>();
			}
			else
			{
				Console.WriteLine("Error AutomatA is not a NDFA so eclosure is not possible");
				return new List<T>();
			}
		}

		public void Delta()
		{
			//Loomp door transities
			//Check of er een transitie is met een van de States als from in de lijst en dat symbool
			//Sla to state op
			//Repeat
		}

		/// <summary>
		/// Converts from NDFA to a DFA.
		/// </summary>
		public void NdfaToDfa()
		{
			// Only do this if its an NDFA
			if (!IsDFA())
			{
				// Create fuik ???

				// Create possible states

				// Create defined transitions

				// Create rest of states

				// Remove niet bereikbare states en bijbehorende transitions.

			}
			else
			{
				Console.WriteLine("Error AutomatA is not a NDFA so it cant be converted to DFA");
				// Display error && do nothing
			}
		}
	}
}
