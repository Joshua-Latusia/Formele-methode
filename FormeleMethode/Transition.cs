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
	public class Transition<T> : IEquatable<Transition<T>> where T : IComparable<T>
	{
		/// <summary>
		/// The Empty symbol
		/// </summary>
		public static readonly char EPSILON = '$';

		private T fromState;
		private char symbol;
		private T toState;

		public Transition(T fromOrTo, char s) : this(fromOrTo,s,fromOrTo)
		{
			
		}

		public Transition(T from, T to) : this(from,EPSILON,to)
		{ 

		}

		public Transition(T from, char s, T to)
		{
			this.fromState = from;
			this.symbol = s;
			this.toState = to;
		}

		public bool Equals(Transition<T> other)
		{
			if (other == null)
			{
				return false;
			}
			else if (other is Transition<T> )
			{
				return this.fromState.Equals(((Transition<T>)other).fromState) &&
					   this.toState.Equals(((Transition<T>)other).toState) && 
					   this.symbol.Equals(((Transition<T>)other).symbol);
			}
			return false;
		}


		//public int CompareTo(Transition<T> t)
		//{
		//	int fromCmp = fromState.CompareTo(t.fromState);
		//	int symbolCmp = symbol.CompareTo(t.symbol);
		//	int toCmp = toState.CompareTo(t.toState);

		//	return (fromCmp != 0 ? fromCmp : (symbolCmp != 0 ? symbolCmp : toCmp));
		//}

		public int CompareTo(Transition<T> t)
		{
			if (this.fromState.Equals(t.GetFromState()) && this.symbol.Equals(t.GetSymbol()))
				return 1;

			return 0;
		}

		public T GetFromState()
		{
			return fromState;
		}

		public T GetToState()
		{
			return toState;
		}

		public char GetSymbol()
		{
			return symbol;
		}

		/// <summary>
		/// To the string.
		/// </summary>
		/// <returns></returns>
		public override String ToString()
		{
			return $"( {fromState}, {symbol} ) --> {toState}";
		}

		
	}
}
