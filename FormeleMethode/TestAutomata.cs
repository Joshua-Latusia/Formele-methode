using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormeleMethode
{
	public class TestAutomata
	{
		/// <summary>
		/// Reads the file and set objects and returns an automata.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		public static Automata<string> ReadGraphFile(string fileName)
		{
			List<string> endStates = new List<string>();
			List<string> startStates = new List<string>();
			List<Transition<string>> transitions = new List<Transition<string>>();
			SortedSet<char> alphabet = new SortedSet<char>();

			// Open file
			string[] lines = System.IO.File.ReadAllLines(fileName);

			// Read lines
			foreach (string line in lines)
			{
				// If endstate
				if (line.Contains("doublecircle"))
				{
					endStates.Add(line.Split('"')[1]);
				}

				// Transitions
				if (line.Contains("->"))
				{
					// If normal transition or starttransition
					if (line.Contains("label"))
					{
						string[] l = line.Split('"');
						// l1 is from l5 is symbol l3 is tostate
						transitions.Add(new Transition<string>(l[1], Convert.ToChar(l[5]), l[3]));
						alphabet.Add(Convert.ToChar(l[5]));
					}
					else
					{
						string[] l = line.Split('"');
						startStates.Add(l[3]);
					}
				}
			}

			// Create automata
			Automata<string> m = new Automata<string>(alphabet);

			foreach (Transition<string> t in transitions) {
				m.AddTransition(t);
			}

			foreach (string end in endStates)
			{
				m.DefineAsFinalState(end);
			}

			foreach (string start in startStates)
			{
				m.DefineAsStartState(start);
			}

			return m;
		}


		// is a DFA
		public static Automata<string> GetExampleSlide5Week2()
		{
			char[] alphabet = {'a','b' };
			Automata<string> m = new Automata<string>(alphabet);

			m.AddTransition(new Transition<string>("q0", 'a', "q1"));
			m.AddTransition(new Transition<string>("q0", 'b', "q4"));

			m.AddTransition(new Transition<string>("q1", 'a', "q4"));
			m.AddTransition(new Transition<string>("q1", 'b', "q2"));

			m.AddTransition(new Transition<string>("q2", 'a', "q3"));
			m.AddTransition(new Transition<string>("q2", 'b', "q4"));

			m.AddTransition(new Transition<string>("q3", 'a', "q1"));
			m.AddTransition(new Transition<string>("q3", 'b', "q2"));

			m.AddTransition(new Transition<string>("q4", 'a'));
			m.AddTransition(new Transition<string>("q4", 'b'));

			m.DefineAsStartState("q0");

			m.DefineAsFinalState("q2");
			m.DefineAsFinalState("q3");

			return m;
		}

		// is a NDFA
		public static Automata<string> GetExampleSlide14Week2()
		{
			char[] alphabet = { 'a', 'b' };
			Automata<string> m = new Automata<string>(alphabet);

			m.AddTransition(new Transition<string>("A", 'a', "C"));
			m.AddTransition(new Transition<string>("A", 'b', "B"));
			m.AddTransition(new Transition<string>("A", 'c', "C"));

			m.AddTransition(new Transition<string>("B", 'b', "C"));
			m.AddTransition(new Transition<string>("B", "C"));

			m.AddTransition(new Transition<string>("C", 'a', "D"));
			m.AddTransition(new Transition<string>("C", 'a', "E"));
			m.AddTransition(new Transition<string>("C", 'b', "D"));

			m.AddTransition(new Transition<string>("D", 'a', "B"));
			m.AddTransition(new Transition<string>("D", 'a', "C"));

			m.AddTransition(new Transition<string>("E", 'a'));
			m.AddTransition(new Transition<string>("E", "D"));

			m.DefineAsStartState("A");

			m.DefineAsFinalState("C");
			m.DefineAsFinalState("E");

			return m;
		}



	}
}
