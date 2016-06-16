using System;
using System.IO;
using Haruair.Command.Tests.Fixtures;
using NUnit.Framework;

namespace Haruair.Command.Tests
{
	[TestFixture]
	public class CommanderTest
	{
		Commander commander;
		TextWriter originalSw;
		StringWriter sw;

		[SetUp]
		public void Init()
		{
			commander = new Commander();

			commander.Add<HelloCommand>();
			commander.Add(typeof(TimeCommand));

			originalSw = Console.Out;

			sw = new StringWriter();
			Console.SetOut(sw);
		}

		[TearDown]
		public void Dispose()
		{
			commander = null;
			sw = null;

			Console.SetOut(originalSw);
		}

		[Test]
		public void NoInputTestCase()
		{
			var mockArgs = new string[0];
			commander.Parse(mockArgs);

			var expected = @"Example: 
  hello, h	Hello Command. Nothing Special.
  time, t	Check the system time.
";
			expected = expected.Replace("\n", Environment.NewLine);
			Assert.AreEqual(expected, sw.ToString());
		}

		[Test]
		public void HelloTestCase()
		{
			var mockArgs = new string[] { "hello" };
			commander.Parse(mockArgs);

			var expected = @"Example of hello:
  say, s	When you want to say something, you can use it.
";
			expected = expected.Replace("\n", Environment.NewLine);
			Assert.AreEqual(expected, sw.ToString());
		}

		[Test]
		public void HelloSayTestCase()
		{
			var mockArgs = new string[] { "hello", "say" };
			commander.Parse(mockArgs);

			var expected = string.Format("Yo. You called me.{0}", Environment.NewLine);

			Assert.AreEqual(expected, sw.ToString());

		}

		[Test]
		public void TimeTestCase()
		{
			var mockArgs = new string[] { "t" };
			commander.Parse(mockArgs);

			var expected = @"Example of t:
  now
";
			expected = expected.Replace("\n", Environment.NewLine);
			Assert.AreEqual(expected, sw.ToString());

		}

		[Test]
		public void TimeNowTestCase()
		{
			var mockArgs = new string[] { "t", "now" };
			commander.Parse(mockArgs);

			var expected = string.Format("{0}{1}", DateTime.Now, Environment.NewLine);
			Assert.AreEqual(expected, sw.ToString());
		}
	}
}
