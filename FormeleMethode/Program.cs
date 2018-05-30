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

			Automata<string> aut = TestAutomata.GetExampleSlide5Week2();
			aut.GenerateGraphFile("Test1");
			aut.ReverseAutomata();
			aut.GenerateGraphFile("Test2");
			aut.ReverseAutomata();
			aut.GenerateGraphFile("Test3");

			Automata<string> aut2 = TestAutomata.ReadGraphFile(@".\..\..\graphviz\dotfiles\Test1.dot");
			aut2.GenerateGraphFile("Test1Dup");
			FileDotEngine.Run(@".\..\..\graphviz\dotfiles\Test1Dup","Test1PicDup");

			//FileDotEngine.Run(@".\..\..\graphviz\dotfiles\Test1","Test1Pic");
			//FileDotEngine.Run(@".\..\..\graphviz\dotfiles\Test2", "Test2Pic");


			//TestAutomata.GetExampleSlide5Week2().ReverseAutomata().GenerateGraphFile("Test1");




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
	}
}
