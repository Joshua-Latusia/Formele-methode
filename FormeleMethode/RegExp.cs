using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormeleMethode
{
	/// <summary>
	/// Representation of regular expressions
	/// 
	/// By Joshua Latusia
	/// </summary>
	public class RegExp
	{
		/// <summary>
		///   (+, *, |, .) 
		/// </summary>
		public enum Operator { PLUS, STAR, OR, DOT, ONE };

		Operator o;
		string terminals;

		RegExp left;
		RegExp right;

	

		/// <summary>
		/// Initializes a new instance of the <see cref="RegExp"/> class.
		/// </summary>
		public RegExp()
		{
			o = Operator.ONE;
			terminals = "";
			left = null;
			right = null;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RegExp"/> class.
		/// </summary>
		/// <param name="p">The p.</param>
		public RegExp(string p)
		{
			o = Operator.ONE;
			terminals = p;
			left = null;
			right = null;
		}

		/// <summary>
		/// Pluses this instance.
		/// </summary>
		/// <returns></returns>
		public RegExp Plus()
		{
			RegExp res = new RegExp
			{
				o = Operator.PLUS,
				left = this
			};
			return res;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Object" /> class.
		/// </summary>
		/// <returns></returns>
		public RegExp Star()
		{
			RegExp res = new RegExp
			{
				o = Operator.STAR,
				left = this,
			};
			return res;
		}

		/// <summary>
		/// Ors this instance.
		/// </summary>
		/// <returns></returns>
		public RegExp Or()
		{
			RegExp res = new RegExp
			{
				o = Operator.OR,
				left = this
			};
			return res;
		}

		/// <summary>
		/// Dots the specified e2.
		/// </summary>
		/// <param name="e2">The e2.</param>
		/// <returns></returns>
		public RegExp Dot(RegExp e2)
		{
			RegExp res = new RegExp
			{
				o = Operator.DOT,
				left = this,
				right = e2
			};
			return res;
		}

		/// <summary>
		/// Gets the language.
		/// </summary>
		/// <param name="maxSteps">The maximum steps.</param>
		/// <returns></returns>
		public SortedSet<String> GetLanguage(int maxSteps)
		{
			SortedSet<string> emptyLan = new SortedSet<string>(/*compare by lengt??*/)

		}

	}
}
