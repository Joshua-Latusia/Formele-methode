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
		}

		public void TestLanguage()
		{
			Console.WriteLine("taal van (baa):\n");
			expr1.PrintLanguageAsString(expr1.GetLanguage(4));

			Console.WriteLine("\ntaal van (bb):\n");
			expr2.PrintLanguageAsString(expr2.GetLanguage(4));

			Console.WriteLine("\ntaal van (baa | bb):\n");
			expr3.PrintLanguageAsString(expr3.GetLanguage(4));

			Console.WriteLine("\ntaal van (a|b)*:\n");
			all.PrintLanguageAsString(all.GetLanguage(4));

			Console.WriteLine("\ntaal van (baa | bb)+:\n");
			expr4.PrintLanguageAsString(expr4.GetLanguage(4));

			Console.WriteLine("\ntaal van (baa | bb)+ (a|b)*:\n");
			expr5.PrintLanguageAsString(expr5.GetLanguage(5));


		}
	}
}
