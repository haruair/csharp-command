using System;

namespace Haruair.Command.Tests.Fixtures
{
	[Command("hello", "h")]
	[Usage("Hello Command. Nothing Special.")]
	public class HelloCommand
	{
		[Command("say", "s")]
		[Usage("When you want to say something, you can use it.")]
		public void Say()
		{
			Console.WriteLine("Yo. You called me.");
		}
	}
}
