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

		public Operator o;
		public string terminals;

		public RegExpression left;
		public RegExpression right;

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
		public RegExpression(string regex)
		{
			RegExpression regExp = StringToRegExpression(new RegExpression(), regex);
			o = regExp.o;
			terminals = regExp.terminals;
			left = regExp.left;
			right = regExp.right;
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
		/// Converts a string to a regExpressionObject.
		/// </summary>
		/// <param name="regexString">The regex string.</param>
		/// <returns></returns>
		public RegExpression StringToRegExpression(RegExpression regex, string regexString)
		{
			// Remove spaces
			regexString = regexString.Replace(" ", String.Empty);

			// Seperate all terminals
			char[] seperators = { '+', '*', '|', '(', ')' };
			List<string> terminalParts = regexString.Split(seperators).ToList();
			terminalParts.RemoveAll(x => x == String.Empty);
			int terminalIndex = 0;
			int maxTerminals = terminalParts.Count();

			int i = 0;
			while (i < regexString.Length)
			{
				char currentChar = regexString[i];

				// For everything between ( )
				if (currentChar == '(')
				{
					int closingBracketPosition = -1;
					int bracketCount = 0;
					for (int j = i + 1; i < regexString.Length; j++)
					{
						if (regexString[j] == '(') bracketCount++;

						// We found the matching closing bracket
						if (regexString[j] == ')' && bracketCount == 0)
						{
							closingBracketPosition = j;
							break;
						}
						if (regexString[j] == ')' && bracketCount != 0)
						{
							bracketCount--;
						}
					}

					// Get the regex for the part between  ()
					string between = regexString.Substring(i + 1, closingBracketPosition - 1 - i);
					RegExpression regExpression = StringToRegExpression(new RegExpression(), between);

					// Look for the part after closing bracket 
					if (closingBracketPosition + 1 < regexString.Length)
					{
						i = closingBracketPosition + 1;
						currentChar = regexString[i];
						if (currentChar == '+')
						{
							regExpression = regExpression.Plus();
						}
						else if (currentChar == '*')
						{
							regExpression = regExpression.Star();
						}
					}

					if (regex.terminals == "" && regex.o == Operator.ONE)
					{
						regex = regExpression;
					}
					else
					{
						regex = regex.Dot(regExpression);
					}
				}

				// For all the operators not related to ()
				else if (currentChar == '+')
				{
					regex = regex.Plus();
				}
				else if (currentChar == '*')
				{
					regex = regex.Star();
				}
				else if (currentChar == '|')
				{
					regex = regex.Or(new RegExpression(terminalParts[terminalIndex].ToString()));
					terminalIndex++;
					i++;
				}
				else
				{
					if (regex.terminals == "" && regex.o == Operator.ONE)
					{
						regex.terminals = terminalParts[terminalIndex];

						// Skip rest of the terminal part in the loop
						int num = regex.terminals.Count();
						i += terminalParts[terminalIndex].Length - 1;
						terminalIndex++;
					}
					else
					{
						//regex = regex.Dot(new RegExpression(currentChar.ToString()));
					}
				}
				i++;
			}


			//for (int i = 0; i < regexString.Length; i++)
			//{
			//	char currentChar = regexString[i];

			//	// For everything between ( )
			//	if (currentChar == '(')
			//	{
			//		int closingBracketPosition = -1;
			//		int bracketCount = 0;
			//		for (int j = i + 1; i < regexString.Length; j++)
			//		{
			//			if (regexString[j] == '(') bracketCount++;

			//			// We found the matching closing bracket
			//			if (regexString[j] == ')' && bracketCount == 0)
			//			{
			//				closingBracketPosition = j;
			//				break;
			//			}
			//			if (regexString[j] == ')' && bracketCount != 0)
			//			{
			//				bracketCount--;
			//			}
			//		}

			//		// Get the regex for the part between  ()
			//		string between = regexString.Substring(i + 1, closingBracketPosition - 1 - i);
			//		RegExpression regExpression = StringToRegExpression(new RegExpression(), between);

			//		// Look for the part after closing bracket 
			//		if (closingBracketPosition + 1 < regexString.Length)
			//		{
			//			i = closingBracketPosition + 1;
			//			currentChar = regexString[i];
			//			if (currentChar == '+')
			//			{
			//				regExpression = regExpression.Plus();
			//			}
			//			else if (currentChar == '*')
			//			{
			//				regExpression = regExpression.Star();
			//			}
			//		}

			//		if (regex.terminals == "" && regex.o == Operator.ONE)
			//		{
			//			regex = regExpression;
			//		}
			//		else
			//		{
			//			regex = regex.Dot(regExpression);
			//		}
			//	}

			//	// For all the operators not related to ()
			//	else if (currentChar == '+')
			//	{
			//		regex = regex.Plus();
			//	}
			//	else if (currentChar == '*')
			//	{
			//		regex = regex.Star();
			//	}
			//	else if (currentChar == '|')
			//	{
			//		regex = regex.Or(new RegExpression(terminalParts[terminalIndex].ToString()));
			//		terminalIndex++;
			//		i++;
			//	}
			//	else
			//	{
			//		if (regex.terminals == "" && regex.o == Operator.ONE)
			//		{
			//			regex.terminals = terminalParts[terminalIndex];

			//			// Skip rest of the terminal part in the loop
			//			int num = regex.terminals.Count();
			//			i += terminalParts[terminalIndex].Length - 1;
			//			terminalIndex++;
			//		}
			//		else
			//		{
			//			//regex = regex.Dot(new RegExpression(currentChar.ToString()));
			//		}
			//	}

			//}
			return regex;
		}


		/// <summary>
		/// Gets all the words that are not in the language.
		/// </summary>
		/// <param name="maxSteps">The maximum steps.</param>
		/// <returns></returns>
		public List<string> GetNotLanguage(int maxSteps)
		{
			// Gets the language
			SortedSet<string> language = GetLanguage(maxSteps);
			List<string> combinations = new List<string>();
			SortedSet<string> notLangue = new SortedSet<string>();
			List<char> alphabeth = new List<char>();

			// Get longest string length - 1 based
			if (language.Count <= 0)
				return combinations;

			int longestString = language.Last().Length;

			// Gets the different chars in alphabeth
			alphabeth = language.Last().ToCharArray().Distinct().ToList();

			// Generate all possible strings upto the longestString length
			var q = alphabeth.Select(x => x.ToString());
			for (int i = 0; i < longestString - 1; i++)
				q = q.SelectMany(x => alphabeth, (x, y) => x + y);

			// Convert to list
			combinations = q.ToList();

			// Subtract language from all possible things
			combinations.RemoveAll(x => language.Contains(x));


			// Return not language
			return combinations;

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
				case Operator.ONE:
					languageRes.Add(terminals); 
					break;

				case Operator.OR:
					lanLeft = left == null ? emptyLan : left.GetLanguage(maxSteps - 1);
					lanRight = right == null ? emptyLan : right.GetLanguage(maxSteps - 1);
					languageRes.UnionWith(lanLeft);
					languageRes.UnionWith(lanRight);
					break;

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

		/// <summary>
		/// Easier visualisation of the regexpression as string
		/// </summary>
		public override string ToString()
		{
			string regexString = "";

			if (terminals != "")
			{
				regexString = terminals;
			}
			else
			{
				if (right != null)
				{
					regexString += "(";
				}
				regexString += left.ToString();
				switch (o)
				{
					case Operator.PLUS:
						regexString += '+';
						break;
					case Operator.STAR:
						regexString += '*';
						break;
					case Operator.OR:
						regexString += '|';
						break;
					
				}
				if (right != null)
				{
					regexString += right.ToString();
					regexString += ')';
				}
			}
			return regexString;
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
