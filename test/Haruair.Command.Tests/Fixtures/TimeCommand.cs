using System;

namespace Haruair.Command.Tests.Fixtures
{
	[Command("time", "t")]
	[Usage("Check the system time.")]
	public class TimeCommand
	{
		[Command("now")]
		public void Now() {
			Console.WriteLine ("{0}", DateTime.Now);
		}
	}
}

