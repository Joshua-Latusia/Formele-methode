using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormeleMethode
{
	public static class FileDotEngine
	{
		public static void Run(string dotFileName, string fileName)
		{
			string executable = @".\..\..\external\dot.exe";
			string output = $@".\..\..\graphviz\{fileName}.png";

			System.Diagnostics.Process process = new System.Diagnostics.Process();

			// Stop the process from opening a new window
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.CreateNoWindow = true;

			// Setup executable and parameters
			process.StartInfo.FileName = executable;
			process.StartInfo.Arguments = $@"-Tjpg {dotFileName}.dot -O {output}";
			Console.WriteLine(process.StartInfo.Arguments);

			// Go
			process.Start();
			// and wait dot.exe to complete and exit
			process.WaitForExit();
		}
		
	}
}
