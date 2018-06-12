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

		public HashSet<Transition<T>> transitions;

		public SortedSet<T> states;
		public SortedSet<T> startStates;
		public SortedSet<T> endStates;
		public SortedSet<char> symbols;

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

		// geefTaalTotLengte TODO
		public List<string> GetLanguage(int maxLength)
		{
			List<string> language = new List<string>();
			GenerateWords(String.Empty, maxLength, ref language, true);

			return language;
		}

		// geefNietTaalTotLengte
		public List<string> GetNotLanguage(int maxLength)
		{
			List<string> notLanguage = new List<string>();
			GenerateWords(String.Empty, maxLength, ref notLanguage, false);
			return notLanguage;
		}

		/// <summary>
		/// Generates the words in the language or not language
		/// isLangue is bool for determin if it is the language or notLanguage
		/// </summary>
		/// <param name="word">The word.</param>
		/// <param name="maxLength">The maximum length.</param>
		/// <param name="wordList">The word list.</param>
		/// <param name="isLanguage">if set to <c>true</c> [is language].</param>
		private void GenerateWords(string word, int maxLength, ref List<string> wordList, bool isLanguage)
		{
			// Stop generating words
			if (word.Length >= maxLength)
				return;

			for (int i = 0; i < symbols.Count; i++)
			{
				var newWord = word + symbols.ToList()[i];

				// If we search language and word is accepted add it
				if (Accept(newWord) && isLanguage)
				{
					wordList.Add(newWord);
				}
				else if (!Accept(newWord) && !isLanguage)
				{
					wordList.Add(newWord);
				}
				GenerateWords(newWord, maxLength, ref wordList, isLanguage);
			}
		}

		/// <summary>
		/// Recursive function to gett all possible Words in the language
		/// </summary>
		/// <param name="letterIndex">Index of the letter.</param>
		/// <param name="currentWord">The current word.</param>
		/// <param name="words">The words.</param>
		/// <param name="maxLength">The maximum length.</param>
		/// <returns></returns>
		public List<string> NextLetter(int letterIndex, char[] currentWord, List<string> words, int maxLength)
		{
			// For all possible symbols
			for (int i = 0; i < symbols.Count; i++)
			{
				currentWord[letterIndex] = symbols.ToList()[i];

				if (letterIndex == maxLength - 1)
				{
					words.Add(new string(currentWord));
				}
				else
				{
					words.Add(new string(currentWord));
					NextLetter(letterIndex + 1, currentWord, words, maxLength);
				}
			}
			return words;
		}

		/// <summary>
		/// Check if S is accepted in the language
		/// </summary>
		/// <param name="s">The s.</param>
		/// <returns></returns>
		public bool Accept(string s, bool printTransitions = false)
		{
			char[] charArray = s.ToCharArray();

			// Try for all startStates
			foreach (T startState in startStates)
			{
				T currentState = startState;
				if(printTransitions)
					Console.WriteLine($"Start State : {currentState}  \nString : {s}");
				CharEnumerator c = s.GetEnumerator();

				// Check all chars
				while (c.MoveNext())
				{
					// search for matching transition with fromState = currenState && symbol = c.Current
					Transition<T> transition = new Transition<T>(currentState, c.Current);
					currentState = GetMatchingState(transition, printTransitions);

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
		public T GetMatchingState(Transition<T> t2, bool printTransitions = false)
		{
			foreach (Transition<T> t in transitions)
			{
				if (t2.CompareTo(t) == 1)
				{
					if(printTransitions)
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
		/// 3 REVERSE
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
			List<T> reachableStates = new List<T>();

			// Loop through transitions untill transition.from == state
			foreach (Transition<T> trans in transitions)
			{
				// If we found a transition with fromstate == state
				if(trans.GetFromState().Equals(state))
				{
					// If epsilon add ToState to list and look for all children
					if (trans.GetSymbol() == '$')
					{
						reachableStates.Add(trans.GetFromState());
						reachableStates.Add(trans.GetToState());
						reachableStates.AddRange(EClosure(trans.GetToState()));
					}
					else // Else add fromstate since its the last node
					{
						reachableStates.Add(trans.GetFromState()); // TODO we should look further 
					}
				}
			}

			return reachableStates.Distinct().ToList();
		}

		/// <summary>
		/// Looks to which state is reachable for a certain symbol
		/// </summary>
		/// <param name="states">The states.</param>
		public List<T> Delta(List<T> states, char symbol)
		{
			SortedSet<T> reachableStates = new SortedSet<T>();

			// Check for transistions where the from state is present in the 'states' parameter and matching symbol
			foreach (Transition<T> trans in transitions)
			{
				if (states.Contains(trans.GetFromState()) && trans.GetSymbol() == symbol)
				{
					// Add the toState to the reachables states
					reachableStates.Add(trans.GetToState());
				}
			}

			// Check for all reachable states if there are states connected with epsilon which are per 
			// Definition also reachable
			List<T> reachableWithEpsilon = reachableStates.ToList();
			foreach (T reachableState in reachableStates)
			{
				reachableWithEpsilon.AddRange(EClosure(reachableState));
			}
			// Return list without duplicates
			return reachableWithEpsilon.Distinct().ToList();
		}

		/// <summary>
		/// Gets the transistions with state being the from state in the transitions.
		/// </summary>
		/// <param name="state">The state.</param>
		/// <returns></returns>
		public List<Transition<T>> GetTransitions(T state)
		{
			// Get all the transitions for the given state
			List<Transition<T>> trans = transitions.Where(t => t.GetFromState().Equals(state)).ToList();

			// Take all the transitions where symbol is epsilon
			List<T> epsilonStates = trans.Where(t => t.GetSymbol() == '$' && !t.GetFromState().Equals(t.GetToState())).Select(t => t.GetToState()).ToList();
			foreach (T epsilonState in epsilonStates)
			{
				trans.AddRange(GetTransitions(epsilonState));
			}

			return trans;
		}
	}
}
