using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormeleMethode
{
	public class ThompsonConstruction
	{
		/// <summary>
		/// Converts regular expression to NDFA.
		/// </summary>
		/// <param name="regExpression">The reg expression.</param>
		/// <returns></returns>
		public static Automata<string> RegExpToNDFA(RegExpression regExpression)
		{
			Automata<string> ndfa = new Automata<string>();

			ndfa.DefineAsStartState("0");
			ndfa.DefineAsFinalState("1");
			int stateCounter = 2;

			Convert(regExpression, ref ndfa, ref stateCounter, 0, 1);
			ndfa.symbols = new SortedSet<char>(ndfa.transitions.Distinct().Select(e => e.GetSymbol()).ToList());

			// Remove epsilons
			ndfa.symbols.Remove('$');
			return ndfa;

		}

		/// <summary>
		/// Converts the regexpress units.
		/// </summary>
		/// <param name="regExp">The reg exp.</param>
		/// <param name="ndfa">The ndfa.</param>
		/// <param name="stateCounter">The state counter.</param>
		/// <param name="leftState">State of the left.</param>
		/// <param name="rightState">State of the right.</param>
		public static void Convert(RegExpression regExp, ref Automata<string> ndfa, ref int stateCounter, int leftState, int rightState)
		{
			switch (regExp.o)
			{
				case RegExpression.Operator.PLUS:
					Plus(regExp, ref ndfa, ref stateCounter, leftState, rightState);
					break;
				case RegExpression.Operator.STAR:
					Star(regExp, ref ndfa, ref stateCounter, leftState, rightState);
					break;
				case RegExpression.Operator.OR:
					Or(regExp, ref ndfa, ref stateCounter, leftState, rightState);
					break;
				case RegExpression.Operator.DOT:
					Dot(regExp, ref ndfa, ref stateCounter, leftState, rightState);
					break;
				case RegExpression.Operator.ONE:
					One(regExp, ref ndfa, ref stateCounter, leftState, rightState);
					break;
			}
		}

		/// <summary>
		/// Converts + in the regex
		/// </summary>
		/// <param name="regExp">The reg exp.</param>
		/// <param name="automaat">The automaat.</param>
		/// <param name="stateCounter">The state counter.</param>
		/// <param name="leftState">State of the left.</param>
		/// <param name="rightState">State of the right.</param>
		public static void Plus(RegExpression regExp, ref Automata<string> automaat, ref int stateCounter, int leftState, int rightState)
		{
			int stateTwo = stateCounter;
			int stateThree = stateCounter + 1;
			stateCounter = stateCounter + 2;

			// Create epsilon transitions
			automaat.AddTransition(new Transition<string>(leftState.ToString(), '$', stateTwo.ToString()));
			automaat.AddTransition(new Transition<string>(stateThree.ToString(), '$', stateTwo.ToString()));
			automaat.AddTransition(new Transition<string>(stateThree.ToString(), '$', rightState.ToString()));

			// Convert the middle part
			Convert(regExp.left, ref automaat, ref stateCounter, stateTwo, stateThree);
		}

		/// <summary>
		/// Converts * in the regex
		/// </summary>
		/// <param name="regExp">The reg exp.</param>
		/// <param name="automaat">The automaat.</param>
		/// <param name="stateCounter">The state counter.</param>
		/// <param name="leftState">State of the left.</param>
		/// <param name="rightState">State of the right.</param>
		public static void Star(RegExpression regExp, ref Automata<string> automaat, ref int stateCounter, int leftState,
			int rightState)
		{
			int stateTwo = stateCounter;
			int stateThree = stateCounter + 1;
			stateCounter = stateCounter + 2;

			// Create epsilon transitions
			automaat.AddTransition(new Transition<string>(leftState.ToString(), '$', stateTwo.ToString()));
			automaat.AddTransition(new Transition<string>(stateThree.ToString(), '$', stateTwo.ToString()));
			automaat.AddTransition(new Transition<string>(stateThree.ToString(), '$', rightState.ToString()));
			automaat.AddTransition(new Transition<string>(leftState.ToString(), '$', rightState.ToString()));

			// Convert middle part
			Convert(regExp.left, ref automaat, ref stateCounter, stateTwo, stateThree);
		}

		/// <summary>
		/// Converts | in the regex
		/// </summary>
		/// <param name="regExp">The reg exp.</param>
		/// <param name="automaat">The automaat.</param>
		/// <param name="stateCounter">The state counter.</param>
		/// <param name="leftState">State of the left.</param>
		/// <param name="rightState">State of the right.</param>
		public static void Or(RegExpression regExp, ref Automata<string> automaat, ref int stateCounter, int leftState, int rightState)
		{
			int state2 = stateCounter;
			int state3 = stateCounter + 1;
			int state4 = stateCounter + 2;
			int state5 = stateCounter + 3;
			stateCounter = stateCounter + 4;

			// Create epsilon transitions
			automaat.AddTransition(new Transition<string>(leftState.ToString(), '$', state2.ToString()));
			automaat.AddTransition(new Transition<string>(leftState.ToString(), '$', state4.ToString()));
			automaat.AddTransition(new Transition<string>(state3.ToString(), '$', rightState.ToString()));
			automaat.AddTransition(new Transition<string>(state5.ToString(), '$', rightState.ToString()));

			// Convert the middle part of both middle parts
			Convert(regExp.left, ref automaat, ref stateCounter, state2, state3);
			Convert(regExp.right, ref automaat, ref stateCounter, state4, state5);
		}

		/// <summary>
		/// Converts . in the regex
		/// </summary>
		/// <param name="regExp">The reg exp.</param>
		/// <param name="automaat">The automaat.</param>
		/// <param name="stateCounter">The state counter.</param>
		/// <param name="leftState">State of the left.</param>
		/// <param name="rightState">State of the right.</param>
		public static void Dot(RegExpression regExp, ref Automata<string> automaat, ref int stateCounter, int leftState, int rightState)
		{
			int midState = stateCounter;
			stateCounter++;
			Convert(regExp.left, ref automaat, ref stateCounter, leftState, midState);
			Convert(regExp.right, ref automaat, ref stateCounter, midState, rightState);
		}

		/// <summary>
		/// Ones the specified reg exp.
		/// </summary>
		/// <param name="regExp">The reg exp.</param>
		/// <param name="automaat">The automaat.</param>
		/// <param name="stateCounter">The state counter.</param>
		/// <param name="leftState">State of the left.</param>
		/// <param name="rightState">State of the right.</param>
		public static void One(RegExpression regExp, ref Automata<string> automaat, ref int stateCounter, int leftState, int rightState)
		{
			char[] characters = regExp.terminals.ToCharArray();
			if (characters.Length == 1)
			{
				// Create 1 letter transition
				automaat.AddTransition(
					new Transition<string>(leftState.ToString(), characters[0], rightState.ToString()));
			}
			else
			{
				// Create transition for multiple letters
				automaat.AddTransition(
					new Transition<string>(leftState.ToString(), characters[0], stateCounter.ToString()));
				int i = 1;
				while (i < characters.Length - 1)
				{
					automaat.AddTransition(new Transition<string>(stateCounter.ToString(), characters[i],
						(stateCounter + 1).ToString()));
					stateCounter++;
					i++;
				}
				automaat.AddTransition(
					new Transition<string>(stateCounter.ToString(), characters[i], rightState.ToString()));
				stateCounter++;
			}
		}


	}
}
