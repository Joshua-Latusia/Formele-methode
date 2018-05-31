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
			CreateExampleNDFA();

			// Create DFA
			CreateExampleDFA();

			// Create Regexp
			new TestRegExp().TestLanguage();

		

			String graphVizString = @" digraph g{ label=""Graph""; labelloc=top;labeljust=left;}";
			String testje = @" digraph {
 """" [shape=none]
 ""C""  [shape=doublecircle]
 ""E""[shape = doublecircle]

""""-> ""A""
""A""-> ""C""[label = ""a"", weight = ""a""];
			""A""-> ""B""[label = ""b"", weight = ""b""];
			""A""-> ""C""[label = ""b"", weight = ""b""];
			""B""-> ""C""[label = ""$"", weight = ""$""];
			""B""-> ""C""[label = ""b"", weight = ""b""];
			""C""-> ""D""[label = ""a"", weight = ""a""];
			""C""-> ""E""[label = ""a"", weight = ""a""];
			""C""-> ""D""[label = ""b"", weight = ""b""];
			""D""-> ""B""[label = ""a"", weight = ""a""];
			""D""-> ""C""[label = ""a"", weight = ""a""];
			""E""-> ""D""[label = ""$"", weight = ""$""];
			""E""-> ""E""[label = ""a"", weight = ""a""];
		}
";
			//FileDotEngine.Run(testje);

			Console.ReadLine();
		}

		/// <summary>
		/// Creates 3 example NDFA.
		/// </summary>
		public static void CreateExampleNDFA()
		{
			// With epsilon
			Automata<string> exmapleNDFA1 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\ndfaExample1");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\ndfaExample1", "ndfaExample1Pic");
			Console.WriteLine($"Example NDFA1 is a dfa = {exmapleNDFA1.IsDFA()}");

			Automata<string> exmapleNDFA2 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\ndfaExample2");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\ndfaExample2", "ndfaExample2Pic");
			Console.WriteLine($"Example NDFA2 is a dfa = {exmapleNDFA2.IsDFA()}");

			Automata<string> exmapleNDFA3 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\ndfaExample3");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\ndfaExample3", "ndfaExample3Pic");
			Console.WriteLine($"Example NDFA3 is a dfa = {exmapleNDFA3.IsDFA()}");

			// With epsilon
			Automata<string> exmapleNDFA4 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\ndfaExample4");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\ndfaExample4", "ndfaExample4Pic");
			Console.WriteLine($"Example NDFA4 is a dfa = {exmapleNDFA4.IsDFA()}");

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
	}
}
