using System;
using Haruair.Command.Interface;

namespace Haruair.Command
{
	public class BasicConsolePrompter : IPrompter
	{
		public void Write(string value) {
			Console.Write (value);
		}

		public void Write(string value, params object[] arg) {
			Console.Write(value, arg);
		}

		public void WriteLine() {
			Console.WriteLine ();
		}

		public void WriteLine(string value) {
			Console.WriteLine (value);
		}

		public void WriteLine(string value, params object[] arg) {
			Console.WriteLine(value, arg);
		}
	}
}

