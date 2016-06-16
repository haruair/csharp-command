using NUnit.Framework;
using System;
using System.IO;
using Haruair.Command.Tests.Fixtures;

namespace Haruair.Command.Tests
{
	[TestFixture ()]
	public class ParameterTest
	{
		Commander commander;
		TextWriter originalSw;
		StringWriter sw;

		[SetUp()]
		public void Init ()
		{
			this.commander = new Commander ();

			this.commander.Add<CountryCommand> ();

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
		public void NoParamTestCase ()
		{
			var mockArgs = new string[] { "country", "where" };
			this.commander.Parse (mockArgs);

			var expected = @"Example of country:
  where <here>	Country check now. No GPS Search.
";
			expected = expected.Replace ("\n", Environment.NewLine).TrimEnd();
			Assert.AreEqual (expected, sw.ToString ().TrimEnd());
		}

		[Test ()]
		public void OneParamTestCase ()
		{
			var mockArgs = new string[] { "country", "where", "Australia" };
			this.commander.Parse (mockArgs);

			var expected = @"Where are you? Australia?
";
			expected = expected.Replace ("\n", Environment.NewLine).TrimEnd();
			Assert.AreEqual (expected, sw.ToString ().TrimEnd());
		}

		[Test ()]
		public void OptionalParamTestCase ()
		{
			var mockArgs = new string[] { "country", "add", "Australia" };
			this.commander.Parse (mockArgs);

			var expected = @"Add new country Australia
lat is null
lng is null
";
			expected = expected.Replace ("\n", Environment.NewLine).TrimEnd();
			Assert.AreEqual (expected, sw.ToString ().TrimEnd());
		}

		[Test ()]
		public void OptionalParamsTestCase ()
		{
			var mockArgs = new string[] { "country", "add", "Australia", "10.000", "20.000" };
			this.commander.Parse (mockArgs);

			var expected = @"Add new country Australia
lat is 10.000
lng is 20.000
";
			expected = expected.Replace ("\n", Environment.NewLine).TrimEnd();
			Assert.AreEqual (expected, sw.ToString ().TrimEnd());
		}
	}
}

