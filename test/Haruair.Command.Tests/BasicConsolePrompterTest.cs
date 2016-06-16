using NUnit.Framework;
using System;
using Haruair.Command.Interface;
using System.IO;

namespace Haruair.Command.Tests
{
	[TestFixture()]
	public class BasicConsolePrompterTest
	{
		IPrompter prompter;

		TextWriter originalSw;
		StringWriter sw;

		[SetUp()]
		public void Init()
		{
			prompter = new BasicConsolePrompter();

			this.originalSw = Console.Out;

			this.sw = new StringWriter();
			Console.SetOut(sw);
		}

		[TearDown()]
		public void Dispose()
		{
			Console.SetOut(this.originalSw);
			sw = null;
			prompter = null;
		}

		[Test()]
		public void WriteUsualTestCase()
		{
			prompter.Write("Hello World");
			var expected = @"Hello World";
			Assert.AreEqual(expected, sw.ToString());
		}

		[Test()]
		public void WriteWithFormatTestCase()
		{
			prompter.Write("{0} {1} {2}", "Hey", "Hello", "World");
			var expected = @"Hey Hello World";
			Assert.AreEqual(expected, sw.ToString());
		}

		[Test()]
		public void WriteLineEmptyTestCase()
		{
			prompter.WriteLine();
			var expected = @"
";
			expected = expected.Replace("\n", Environment.NewLine);
			Assert.AreEqual(expected, sw.ToString());
		}

		[Test()]
		public void WriteLineUsualTestCase()
		{
			prompter.WriteLine("Hello World");
			var expected = @"Hello World
";
			expected = expected.Replace("\n", Environment.NewLine);
			Assert.AreEqual(expected, sw.ToString());
		}

		[Test()]
		public void WriteLineFormatTestCase()
		{
			prompter.WriteLine("{0} {1} {2}", "Hey", "Hello", "World");
			var expected = @"Hey Hello World
";
			expected = expected.Replace("\n", Environment.NewLine);
			Assert.AreEqual(expected, sw.ToString());
		}
	}
}
