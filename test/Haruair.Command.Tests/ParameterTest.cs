using System;
using System.IO;
using Haruair.Command.Tests.Fixtures;
using NUnit.Framework;

namespace Haruair.Command.Tests
{
	[TestFixture]
	public class ParameterTest
	{
		Commander commander;
		TextWriter originalSw;
		StringWriter sw;

		[SetUp]
		public void Init()
		{
			commander = new Commander();

			commander.Add<CountryCommand>();

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
		public void NoParamTestCase()
		{
			var mockArgs = new string[] { "country", "where" };
			commander.Parse(mockArgs);

			var expected = @"Example of country:
  where <here>	Country check now. No GPS Search.
  add <country> [lat] [lng]	Add new country.
";
			expected = expected.Replace("\n", Environment.NewLine).TrimEnd();
			Assert.AreEqual(expected, sw.ToString().TrimEnd());
		}

		[Test]
		public void OneParamTestCase()
		{
			var mockArgs = new string[] { "country", "where", "Australia" };
			commander.Parse(mockArgs);

			var expected = @"Where are you? Australia?
";
			expected = expected.Replace("\n", Environment.NewLine).TrimEnd();
			Assert.AreEqual(expected, sw.ToString().TrimEnd());
		}

		[Test]
		public void OptionalParamTestCase()
		{
			var mockArgs = new string[] { "country", "add", "Australia" };
			commander.Parse(mockArgs);

			var expected = @"Add new country Australia
lat is null
lng is null
";
			expected = expected.Replace("\n", Environment.NewLine).TrimEnd();
			Assert.AreEqual(expected, sw.ToString().TrimEnd());
		}

		[Test]
		public void OptionalParamsTestCase()
		{
			var mockArgs = new string[] { "country", "add", "Australia", "10.000", "20.000" };
			commander.Parse(mockArgs);

			var expected = @"Add new country Australia
lat is 10.000
lng is 20.000
";
			expected = expected.Replace("\n", Environment.NewLine).TrimEnd();
			Assert.AreEqual(expected, sw.ToString().TrimEnd());
		}
	}
}
