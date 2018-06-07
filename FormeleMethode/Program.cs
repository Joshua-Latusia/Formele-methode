using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormeleMethode
{
	class Program
	{
		static void Main(string[] args)
		{
			//Console.WriteLine(TestAutomata.GetExampleSlide5Week2().IsDFA());
			//Console.WriteLine(TestAutomata.GetExampleSlide5Week2().Accept("ab") + "\n");
			//Console.WriteLine(TestAutomata.GetExampleSlide5Week2().Accept("aba") + "\n");
			//Console.WriteLine(TestAutomata.GetExampleSlide5Week2().Accept("abab") + "\n");
			//Console.WriteLine(TestAutomata.GetExampleSlide5Week2().Accept("ababc") + "\n");

			//Automata<string> m = new Automata<string>("Worste");
			//Console.WriteLine(TestAutomata.GetExampleSlide14Week2().IsDFA());
			//TestAutomata.GetExampleSlide5Week2();

			//Automata<string> aut = TestAutomata.GetExampleSlide5Week2();
			//aut.GenerateGraphFile("Test1");
			//aut.ReverseAutomata();
			//aut.GenerateGraphFile("Test2");
			//aut.ReverseAutomata();
			//aut.GenerateGraphFile("Test3");

			// Create NDFA
			//CreateExampleNDFA();

			// Create DFA
			//CreateExampleDFA();

			// Create NDFA => DFA
			CreateExampleNDFAToDFA();

			// Create Regexp
			//new TestRegExp().TestLanguage();

			new TestRegExp();


			CreateExampleRegExp();

			Console.ReadLine();
		}

		/// <summary>
		/// Creates a NDFA and converts it to a DFA
		/// </summary>
		public static void CreateExampleNDFAToDFA()
		{
			// With epsilon
			Automata<string> exmapleNDFA1 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\ndfaExample1");
			List<string> ecloseList = exmapleNDFA1.EClosure("S");

			Console.WriteLine("\n\nTesting Delta function with NDFA1 with both a and b");

			Console.WriteLine($"NDFA1 eClosure S with Delta a should be = F,M,N,O,P,Q" +
							  $"\nResult = {String.Join(",", exmapleNDFA1.Delta(ecloseList, 'a').OrderBy(q => q).ToList())}");
			Console.WriteLine($"NDFA1 eClosure S with Delta B should be = F,J,L,M,N,O,Q,R" +
							  $"\nResult = {String.Join(",", exmapleNDFA1.Delta(ecloseList, 'b').OrderBy(q => q).ToList())}");

		}

		/// <summary>
		/// Creates 3 example NDFA.
		/// </summary>
		public static void CreateExampleNDFA()
		{
			// With epsilon
			Automata<string> exmapleNDFA1 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\ndfaExample1");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\ndfaExample1", "ndfaExample1Pic");
			Console.WriteLine($"\nExample NDFA1 is a dfa = {exmapleNDFA1.IsDFA()}");

			// EClosure test 
			Console.WriteLine($"NDFA1 eClosure should be = A,B,C,E,F,H,M,O,Q,S  From state S\n " +
							  $"Result = {String.Join(",",exmapleNDFA1.EClosure("S").OrderBy(q => q).ToList())}\n");
			Console.WriteLine($"NDFA1 eClosure should be = C,E,H  From state C\n " +
							  $"Result = {String.Join(",", exmapleNDFA1.EClosure("C").OrderBy(q => q).ToList())}\n");


			Automata<string> exmapleNDFA2 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\ndfaExample2");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\ndfaExample2", "ndfaExample2Pic");
			Console.WriteLine($"\nExample NDFA2 is a dfa = {exmapleNDFA2.IsDFA()}");

			Automata<string> exmapleNDFA3 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\ndfaExample3");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\ndfaExample3", "ndfaExample3Pic");
			Console.WriteLine($"\nExample NDFA3 is a dfa = {exmapleNDFA3.IsDFA()}");

			// With epsilon
			Automata<string> exmapleNDFA4 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\ndfaExample4");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\ndfaExample4", "ndfaExample4Pic");
			Console.WriteLine($"\nExample NDFA4 is a dfa = {exmapleNDFA4.IsDFA()}");

			// EClosure test 
			Console.WriteLine($"NDFA4 eClosure should be = B,C \n " +
							  $"Result = {String.Join(",", exmapleNDFA4.EClosure("B").OrderBy(q => q).ToList())}\n");

		}

		/// <summary>
		/// Creates 3 example DFA.
		/// </summary>
		public static void CreateExampleDFA()
		{
			Automata<string> exmapleDFA1 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\dfaExample1");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\dfaExample1", "dfaExample1Pic");
			Console.WriteLine($"Example DFA1 is a dfa = {exmapleDFA1.IsDFA()}\n\n");
			Console.WriteLine($"DFA1 Word: abaa Should be accepted      Accepted: {exmapleDFA1.Accept("abaa")}");
			Console.WriteLine($"DFA1 Word: abba Shouldn't be accepted      Accepted: {exmapleDFA1.Accept("abba")}");

			exmapleDFA1.PrintLanguage(4);

			Automata<string> exmapleDFA2 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\dfaExample2");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\dfaExample2", "dfaExample2Pic");
			Console.WriteLine($"\n\nExample DFA2 is a dfa = {exmapleDFA2.IsDFA()}");
			Console.WriteLine($"DFA2 Word: abab Should be accepted      Accepted: {exmapleDFA2.Accept("abab")}");
			Console.WriteLine($"DFA2 Word: ababa Shouldn't be accepted      Accepted: {exmapleDFA2.Accept("ababa")}");

			Automata<string> exmapleDFA3 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\dfaExample3");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\dfaExample3", "dfaExample3Pic");
			Console.WriteLine($"\n\nExample DFA2 is a dfa = {exmapleDFA3.IsDFA()}");
			Console.WriteLine($"DFA3 Word: bbbba  Should be accepted      Accepted: {exmapleDFA3.Accept("bbbba")}");
			Console.WriteLine($"DFA4 Word: aaaba Shouldn't be accepted      Accepted: {exmapleDFA3.Accept("aaaba")}");
		}

		/// <summary>
		/// Creates multiple example regexp.
		/// </summary>
		public static void CreateExampleRegExp()
		{
			// regex : (aa)*(aa)+
			string regex1 = "(aa)*(aa)+" ;
			RegExpression expression1 = new RegExpression(regex1);
			Console.WriteLine($"Language (aa)*(aa)+ \n" +
							  $"Language via toString method = {expression1.ToString()}\n" +
							   $"Word 'aaaa' should be true and the result = WORSTE\n\n");

			// regex : a* (aa+ | ba*b ) * (abba | baab | bbbb)+
			string regex2 = "a* (aa+ | ba*b ) * (abba | baab | bbbb)+";
			RegExpression expression2 = new RegExpression(regex2);
			Console.WriteLine("Language a* (aa+ | ba*b ) * (abba | baab | bbbb)+ \n" +
							  $"Language via toString method = {expression2.ToString()}\n" +
							  $"Word asdasdsd \n\n");

			string regex3 = "(a*b*)+ (bb*b | ab*baa)+";
			RegExpression expression3 = new RegExpression(regex3);
			Console.WriteLine("Language (a*b*)+ (bb*b | ab*baa)+ \n" +
							  $"Language via toString method = {expression3.ToString()}\n" +
							  $"Word asdadsad \n\n");
		}
	}
}
