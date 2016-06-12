using NUnit.Framework;
using System;
using System.IO;

namespace Haruair.Command.Tests
{
	[TestFixture ()]
	public class Test
	{
		Commander commander;
		StringWriter sw;

		[SetUp()]
		public void Init ()
		{
			this.commander = new Commander ();
			this.commander.Add (typeof(HelloCommand));
			this.commander.Add (typeof(TimeCommand));

			this.sw = new StringWriter ();
			Console.SetOut (sw);
		}

		[Test ()]
		public void NoInputTestCase ()
		{
			var mockArgs = new string[0];
			this.commander.Run (mockArgs);

			var expected = @"Example: 
  hello, h	Hello Command. Nothing Special.
  time, t	Check the system time.
";
			Assert.AreEqual (expected, sw.ToString ());
		}

		[Test ()]
		public void HelloTestCase()
		{
			var mockArgs = new string[] { "hello" };
			this.commander.Run (mockArgs);

			var expected = @"Example of hello:
  say, s	When you want to say something, you can use it.
";
			Assert.AreEqual (expected, sw.ToString ());
		}

		[Test ()]
		public void HelloSayTestCase()
		{
			var mockArgs = new string[] { "hello", "say" };
			this.commander.Run (mockArgs);

			var expected = String.Format ("Yo. You called me.{0}", Environment.NewLine);

			Assert.AreEqual (expected, sw.ToString ());

		}

		[Test ()]
		public void TimeTestCase()
		{
			var mockArgs = new string[] { "t" };
			this.commander.Run (mockArgs);

			var expected = @"Example of t:
  now
";
			Assert.AreEqual (expected, sw.ToString ());

		}

		[Test ()]
		public void TimeNowTestCase()
		{
			var mockArgs = new string[] { "t", "now" };
			this.commander.Run (mockArgs);

			var expected = String.Format("{0}{1}", DateTime.Now.ToString(), Environment.NewLine);
			Assert.AreEqual (expected, sw.ToString());
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
