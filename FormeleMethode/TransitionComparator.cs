using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormeleMethode
{
	public class TransitionComparator<T> : IComparer<Transition<T>> where T : IComparable<T>
	{
		public int Compare(Transition<T> x, Transition<T> y)
		{
			// Return 1 if matching
			if (x.GetFromState().Equals(y.GetFromState()) && x.GetSymbol().Equals(y.GetSymbol()))
				return 1;

			return 0;
		}
	}
}
