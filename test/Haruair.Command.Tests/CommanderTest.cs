using NUnit.Framework;
using System;
using System.IO;

namespace Haruair.Command.Tests
{
	[TestFixture ()]
	public class CommanderTest
	{
		Commander commander;
		TextWriter originalSw;
		StringWriter sw;

		[SetUp()]
		public void Init ()
		{
			this.commander = new Commander ();
			this.commander.Add (typeof(HelloCommand));
			this.commander.Add (typeof(TimeCommand));

			this.originalSw = Console.Out;

			this.sw = new StringWriter ();
			Console.SetOut (sw);
		}

		[TearDown()]
		public void Dispose()
		{
			this.commander = null;
			this.sw = null;

			Console.SetOut (this.originalSw);
		}

		[Test ()]
		public void NoInputTestCase ()
		{
			var mockArgs = new string[0];
			this.commander.Parse (mockArgs);

			var expected = @"Example: 
  hello, h	Hello Command. Nothing Special.
  time, t	Check the system time.
";
			expected = expected.Replace ("\n", Environment.NewLine).TrimEnd();
			Assert.AreEqual (expected, sw.ToString ().TrimEnd());
		}

		[Test ()]
		public void HelloTestCase()
		{
			var mockArgs = new string[] { "hello" };
			this.commander.Parse (mockArgs);

			var expected = @"Example of hello:
  say, s	When you want to say something, you can use it.
";
			expected = expected.Replace ("\n", Environment.NewLine).TrimEnd();
			Assert.AreEqual (expected, sw.ToString ().TrimEnd());
		}

		[Test ()]
		public void HelloSayTestCase()
		{
			var mockArgs = new string[] { "hello", "say" };
			this.commander.Parse (mockArgs);

			var expected = String.Format ("Yo. You called me.{0}", Environment.NewLine).TrimEnd();

			Assert.AreEqual (expected, sw.ToString ().TrimEnd());

		}

		[Test ()]
		public void TimeTestCase()
		{
			var mockArgs = new string[] { "t" };
			this.commander.Parse (mockArgs);

			var expected = @"Example of t:
  now
";
			expected = expected.Replace ("\n", Environment.NewLine).TrimEnd();
			Assert.AreEqual (expected, sw.ToString ().TrimEnd());

		}

		[Test ()]
		public void TimeNowTestCase()
		{
			var mockArgs = new string[] { "t", "now" };
			this.commander.Parse (mockArgs);

			var expected = String.Format("{0}{1}", DateTime.Now.ToString(), Environment.NewLine).TrimEnd();
			Assert.AreEqual (expected, sw.ToString().TrimEnd());
		}
	}

	[Command("hello", "h")]
	[Usage("Hello Command. Nothing Special.")]
	public class HelloCommand
	{
		[Command("say", "s")]
		[Usage("When you want to say something, you can use it.")]
		public void Say() {
			Console.WriteLine ("Yo. You called me.");
		}
	}

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
