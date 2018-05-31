using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormeleMethode
{
	public class TestRegExp
	{
		private RegExpression expr1, expr2, expr3, expr4, expr5, a, b, all;

		/// <summary>
		/// Reg expression 3 testData
		/// </summary>
		public TestRegExp()
		{
			a = new RegExpression("a");
			b = new RegExpression("b");

			// expr1: "baa"
			expr1 = new RegExpression("baa");
			// expr2: "bb"
			expr2 = new RegExpression("bb");
			// expr3: "baa | baa"
			expr3 = expr1.Or(expr2);

			// all: "(a|b)*"
			all = (a.Or(b)).Star();

			// expr4: "(baa | baa)+"
			expr4 = expr3.Plus();

			// expr5: "(baa | baa)+ (a|b)*"
			expr5 = expr4.Dot(all);

			StringToRegExpression("a* (aa+ | ba*b )* (abba | baab | bbbb)+");
		}


		/// <summary>
		/// Converts a string to a regular expression
		/// </summary>
		/// <returns></returns>
		public RegExpression StringToRegExpression(string expression)
		{
			// TODO CONTINUE HERE

			// Remove spaces
			expression = expression.Replace(" ", String.Empty);

			// Individual expressions
			List<RegExpression> expressions = new List<RegExpression>();

			// All terminals from regex
			List<string> terminals = new List<string>();

			// Current terminal
			string tempString = "";
			string lastChar = " ";

			// Split the string by delim
			foreach (char c in expression)
			{
				switch (c)
				{
					case '(':
						// TODO do something with dot ???
						break;
					case ')':
					case ' ':
						lastChar = c.ToString();
						if (tempString.Length != 0)
						{
							terminals.Add(tempString);
							// TODO Create regex with the string 
							tempString = "";
						}
						break;

					case '*':
						// TODO if lastchar ) we need to apply right part off expressions
						if (lastChar == ")")
						{
							Console.WriteLine("Haakje");
						}
						else if (tempString.Length != 0)
						{
							// Create part of expression
							RegExpression tempReg = new RegExpression(tempString).Star();
							expressions.Add(tempReg);
							terminals.Add(tempString);
							tempString = "";
							
						}
					break;

					case '+':
						if (lastChar == ")")
						{
							Console.WriteLine("Haakje");
						}
						if (tempString.Length != 0)
						{
							RegExpression tempReg = new RegExpression(tempString).Plus();
							expressions.Add(tempReg);
							terminals.Add(tempString);
							tempString = "";
						}
						break;

					case '|':
						// TODO remember the next string needs to be orred ????
						if (tempString.Length != 0)
						{
							terminals.Add(tempString);
							tempString = "";
						}
						break;

					default:
						tempString += c;
					break;
				}
			}
			//string[] split = expression.Split(new Char[] {'','' });

			return new RegExpression();
		}


		public void TestLanguage()
		{
			Console.WriteLine("taal van (baa):\n");
			expr1.PrintLanguageAsString(expr1.GetLanguage(4));
			Console.WriteLine("\nAlles wat taal (baa) niet accepteerd:\n");
			Console.WriteLine(String.Join(",", expr1.GetNotLanguage(3).OrderBy(x => x)));

			Console.WriteLine("\ntaal van (bb):\n");
			expr2.PrintLanguageAsString(expr2.GetLanguage(4));

			Console.WriteLine("\ntaal van (baa | bb):\n");
			expr3.PrintLanguageAsString(expr3.GetLanguage(4));

			Console.WriteLine("\ntaal van (a|b)*:\n");
			all.PrintLanguageAsString(all.GetLanguage(4));
			Console.WriteLine("\nAlles wat taal (a|b)* niet accepteerd:\n");
			Console.WriteLine(String.Join(",", all.GetNotLanguage(4).OrderBy(x => x)));

			Console.WriteLine("\ntaal van (baa | bb)+:\n");
			expr4.PrintLanguageAsString(expr4.GetLanguage(3)); // TODO fix doesn't work with 2 idk why..
			Console.WriteLine("\nAlles wat taal (baa | bb)+ niet accepteerd:\n");
			Console.WriteLine(String.Join(",", expr4.GetNotLanguage(3).OrderBy(x => x)));

			Console.WriteLine("\ntaal van (baa | bb)+ (a|b)*:\n");
			expr5.PrintLanguageAsString(expr5.GetLanguage(5));


		}
	}
}
