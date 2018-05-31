using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormeleMethode
{
	/// <summary>
	/// Class for representation of Reg express
	/// </summary>
	public class RegExpression
	{
		/// <summary>
		///   (+, *, |, .) 
		/// </summary>
		public enum Operator { PLUS, STAR, OR, DOT, ONE };

		Operator o;
		string terminals;

		RegExpression left;
		RegExpression right;

		/// <summary>
		/// Initializes a new instance of the <see cref="RegExp"/> class.
		/// </summary>
		public RegExpression()
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
		public RegExpression(string p)
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
		public RegExpression Plus()
		{
			RegExpression res = new RegExpression
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
		public RegExpression Star()
		{
			RegExpression res = new RegExpression
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
		public RegExpression Or(RegExpression e2)
		{
			RegExpression res = new RegExpression
			{
				o = Operator.OR,
				left = this,
				right = e2
			};
			return res;
		}

		/// <summary>
		/// Dots the specified e2.
		/// </summary>
		/// <param name="e2">The e2.</param>
		/// <returns></returns>
		public RegExpression Dot(RegExpression e2)
		{
			RegExpression res = new RegExpression
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
		public SortedSet<string> GetLanguage(int maxSteps)
		{
			SortedSet<string> emptyLan = new SortedSet<string>(new LengthComperator());
			SortedSet<string> languageRes = new SortedSet<string>(new LengthComperator());

			SortedSet<string> lanLeft, lanRight;

			if (maxSteps < 1)
				return emptyLan;

			switch (this.o)
			{
				// TODO comment
				case Operator.ONE:
					languageRes.Add(terminals); 
					break;

				// Add both possibilities to the list
				case Operator.OR:
					lanLeft = left == null ? emptyLan : left.GetLanguage(maxSteps - 1);
					lanRight = right == null ? emptyLan : right.GetLanguage(maxSteps - 1);
					languageRes.UnionWith(lanLeft);
					languageRes.UnionWith(lanRight);
					break;

				// TODO comment
				case Operator.DOT:
					lanLeft = left == null ? emptyLan : left.GetLanguage(maxSteps - 1);
					lanRight = right == null ? emptyLan : right.GetLanguage(maxSteps - 1);
					foreach (string s1 in lanLeft)
					{
						foreach (string s2 in lanRight)
						{
							languageRes.Add(s1 + s2);
						}
					}
					// implement
					break;

				// Star(*) and (+) can be worked out in the same way
				case Operator.STAR:
				case Operator.PLUS:
					lanLeft = left == null ? emptyLan : left.GetLanguage(maxSteps - 1);
					languageRes.UnionWith(lanLeft);

					for (int i = 1; i < maxSteps; i++)
					{
						HashSet<string> languageTemp = new HashSet<string>(languageRes);
						foreach (string s1 in lanLeft)
						{
							foreach (string s2 in languageTemp)
							{
								languageRes.Add(s1 + s2);
							}
						}
					}
					if (this.o == Operator.STAR)
					{
						languageRes.Add("");
					}
					break;

				default:
					Console.WriteLine($"GetLanguage is not defined for operator: {this.o}");
					break;

			}
			return languageRes;
		}

		public void PrintLanguageAsString(SortedSet<string> set)
		{
			var sb = new StringBuilder();
	
			foreach (string str in set)
			{
				sb.Append($" {str}, ");
			}

			Console.WriteLine(sb.ToString());
		}

	}

	

	// Comperator for  comparing length
	public class LengthComperator : IComparer<string>
	{
		public int Compare(string s1, string s2)
		{
			if (s1.Length == s2.Length)
			{
				return s1.CompareTo(s2);
			}
			else
			{
				return s1.Length - s2.Length;
			}
		}
	}
}
