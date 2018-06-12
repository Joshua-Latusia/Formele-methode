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
			//CreateExampleNDFAToDFA();

			// Create DFA -> minimized DFA
			//CreateExampleMinimizeDfa();

			// Printing of language and not language
			//new TestRegExp().TestLanguage();

			// Parsing of string to RegExp object
			//CreateExampleRegExp();

			// Thompson construction
			//CreateExampleThompsonConstruction();

			// So app doesn't close
			Console.ReadLine();
		}

		/// <summary>
		/// Creates 2 DFA and minimize them
		/// </summary>
		public static void CreateExampleMinimizeDfa()
		{
			// Creating pic of the dfa1 -> minimize dfa and create graphfile and pic
			Automata<string> DFAToMin1 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\dfaToMinExample1");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\dfaToMinExample1", "dfaToMinExample1Pic");
			Automata<string> MinDFA1 = NdfaToDfaConverter.RenameStates(NdfaToDfaConverter.MinimizeDfa(DFAToMin1));
			MinDFA1.GenerateGraphFile("MinDfaExample1");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\MinDfaExample1", "MinDfaExample1Pic");

			// Aftekenlijst opdracht 6 Creating pic of the dfa2 -> minimize dfa and create graphfile and pic
			Automata<string> DFAToMin2 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\dfaToMinExample2");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\dfaToMinExample2", "dfaToMinExample2Pic");
			Automata<string> MinDFA2 = NdfaToDfaConverter.RenameStates(NdfaToDfaConverter.MinimizeDfa(DFAToMin2));
			MinDFA2.GenerateGraphFile("MinDfaExample2");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\MinDfaExample2", "MinDfaExample2Pic");


		}

		/// <summary>
		/// Creates the example ndfa to dfa.
		/// </summary>
		public static void CreateExampleNDFAToDFA()
		{
			// Aftekenlijst Opdracht 5   Creating pic of the ndfa1 -> toDfa + graphfile and new pic
			Automata<string> ndfaToDfa1 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\ndfaToDfaExample1");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\ndfaToDfaExample1", "ndfaToDfaExample1Pic");
			Automata<string> Dfa1 = NdfaToDfaConverter.ConvertToDFA(ndfaToDfa1);
			Dfa1.GenerateGraphFile("ndfaToDfaExample1Result");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\ndfaToDfaExample1Result", "ndfaToDfaExample1ResultPic");

			// Aftekenlijst Opdracht 5 Creating pic of the ndfa2 -> toDfa + graphfile and new pic DOESNT work
			Automata<string> ndfaToDfa2 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\ndfaToDfaExample2");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\ndfaToDfaExample2", "ndfaToDfaExample2Pic");
			Automata<string> Dfa2 = NdfaToDfaConverter.ConvertToDFA(ndfaToDfa2);
			Dfa2.GenerateGraphFile("ndfaToDfaExample2Result");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\ndfaToDfaExample2Result", "ndfaToDfaExample2ResultPic");

			// NDFA without epsilon
			Automata<string> ndfaToDfa3 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\ndfaExample3");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\ndfaExample3", "ndfaExample3Pic");
			Automata<string> Dfa3 = NdfaToDfaConverter.ConvertToDFA(ndfaToDfa3);
			Dfa3.GenerateGraphFile("ndfaToDfaExample3Result");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\ndfaToDfaExample3Result", "ndfaToDfaExample3ResultPic");
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
			Console.WriteLine($"DFA1 Word: abaa Should be accepted      Accepted: {exmapleDFA1.Accept("abaa",true)}");
			Console.WriteLine($"DFA1 Word: abba Shouldn't be accepted      Accepted: {exmapleDFA1.Accept("abba", true)}");

			// 2A GetAcceptedLanguage
			Console.WriteLine($"\n\nLanguage for DFA1 \n");
			Console.WriteLine(String.Join(",", exmapleDFA1.GetLanguage(4).OrderBy(x => x)));

			// 2B GetNotAcceptedLanguage
			Console.WriteLine($"\n\nNot Language for DFA1\n");
			Console.WriteLine(String.Join(",", exmapleDFA1.GetNotLanguage(4).OrderBy(x => x)));

			Automata<string> exmapleDFA2 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\dfaExample2");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\dfaExample2", "dfaExample2Pic");
			Console.WriteLine($"\n\nExample DFA2 is a dfa = {exmapleDFA2.IsDFA()}");
			Console.WriteLine($"DFA2 Word: abab Should be accepted      Accepted: {exmapleDFA2.Accept("abab", true)}");
			Console.WriteLine($"DFA2 Word: ababa Shouldn't be accepted      Accepted: {exmapleDFA2.Accept("ababa", true)}");

			Console.WriteLine($"\n\nLanguage for DFA2 \n");
			Console.WriteLine(String.Join(",", exmapleDFA2.GetLanguage(4).OrderBy(x => x)));

			Console.WriteLine($"\n\nNot Language for DFA2\n");
			Console.WriteLine(String.Join(",", exmapleDFA2.GetNotLanguage(4).OrderBy(x => x)));

			Automata<string> exmapleDFA3 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\dfaExample3");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\dfaExample3", "dfaExample3Pic");
			Console.WriteLine($"\n\nExample DFA2 is a dfa = {exmapleDFA3.IsDFA()}");
			Console.WriteLine($"DFA3 Word: bbbba  Should be accepted      Accepted: {exmapleDFA3.Accept("bbbba", true)}");
			Console.WriteLine($"DFA4 Word: aaaba Shouldn't be accepted      Accepted: {exmapleDFA3.Accept("aaaba", true)}");
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
							  $"Language via toString method = {expression1.ToString()}\n");

			// regex : a* (aa+ | ba*b ) * (abba | baab | bbbb)+
			string regex2 = "a* (aa+ | ba*b ) * (abba | baab | bbbb)+";
			RegExpression expression2 = new RegExpression(regex2);
			Console.WriteLine("Language a* (aa+ | ba*b ) * (abba | baab | bbbb)+ \n" +
							  $"Language via toString method = {expression2.ToString()}\n" +
							  $"Word asdasdsd \n\n");

			// regex : (a*b*)+ (bb*b | ab*baa)+
			string regex3 = "(a*b*)+ (bb*b | ab*baa)+";
			RegExpression expression3 = new RegExpression(regex3);
			Console.WriteLine("Language (a*b*)+ (bb*b | ab*baa)+ \n" +
							  $"Language via toString method = {expression3.ToString()}\n" +
							  $"Word asdadsad \n\n");
		}

		/// <summary>
		/// Creates the example thompson construction.
		/// </summary>
		public static void CreateExampleThompsonConstruction()
		{
			// regex : (aa)*(aa)+
			string regex1 = "(aa)*(aa)+";
			RegExpression expression1 = new RegExpression(regex1);
			Automata<string> ndfa1 = ThompsonConstruction.RegExpToNDFA(expression1);

			ndfa1.GenerateGraphFile("ThompsonNDFA1");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\ThompsonNDFA1", "ThompsonNDFA1Pic");

			//// regex : a(a|b)*
			string regex2 = "a(a|b)*";
			RegExpression expression2 = new RegExpression(regex2);
			Automata<string> ndfa2 = ThompsonConstruction.RegExpToNDFA(expression2);

			ndfa2.GenerateGraphFile("ThompsonNDFA2");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\ThompsonNDFA2", "ThompsonNDFA2Pic");

			// regex : a*(aa+|(ba))* (abba|baab|bbbb)+
			string regex3 = "a*(aa+|(ba))* (abba|baab|bbbb)+";
			RegExpression expression3 = new RegExpression(regex3);
			Automata<string> ndfa3 = ThompsonConstruction.RegExpToNDFA(expression3);

			ndfa3.GenerateGraphFile("ThompsonNDFA3");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\ThompsonNDFA3", "ThompsonNDFA3Pic");



		}
	}
}
