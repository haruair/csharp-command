using Haruair.Command.Interface;
using NUnit.Framework;

namespace Haruair.Command.Tests
{
	[TestFixture]
	public class BasicRequestParserTest
	{
		IRequestParser parser;

		[SetUp]
		public void Init()
		{
			parser = new BasicRequestParser();
		}

		[TearDown]
		public void Dispose()
		{
			parser = null;
		}

		[Test]
		public void EmptyArgsTestCase()
		{
			var args = new string[] { };
			var request = parser.Parse(args);

			Assert.AreEqual(null, request.Command);
			Assert.AreEqual(null, request.Method);
		}

		[Test]
		public void CommandTestCase()
		{
			var args = new string[] { "hello" };
			var request = parser.Parse(args);

			Assert.AreEqual("hello", request.Command);
			Assert.AreEqual(null, request.Method);
		}

		[Test]
		public void CommandAndMethodTestCase()
		{
			var args = new string[] { "hello", "world" };
			var request = parser.Parse(args);

			Assert.AreEqual("hello", request.Command);
			Assert.AreEqual("world", request.Method);
		}

		[Test]
		public void EssentialExpressionTestCase()
		{
			var args = new string[] { "hello", "world", "--url", "http://haruair.com", "edward" };
			var request = parser.Parse(args);

			Assert.AreEqual("hello", request.Command);
			Assert.AreEqual("world", request.Method);
			Assert.IsTrue(request.Options.ContainsKey("url"));

			string url;

			Assert.IsTrue(request.Options.TryGetValue("url", out url));
			Assert.AreEqual("http://haruair.com", url);

			Assert.IsTrue(request.Params.Contains("edward"));
		}

		[Test]
		public void FullExpressionTestCase()
		{
			var args = new string[] {
				"hello", "world", "--url", "http://haruair.com", "edward", "--nick-name", "eddy", "SecondParam" };
			var request = parser.Parse(args);

			Assert.AreEqual("hello", request.Command);
			Assert.AreEqual("world", request.Method);
			Assert.IsTrue(request.Options.ContainsKey("url"));

			string url, name;

			Assert.IsTrue(request.Options.TryGetValue("url", out url));
			Assert.AreEqual("http://haruair.com", url);

			Assert.IsTrue(request.Options.TryGetValue("nick-name", out name));
			Assert.AreEqual("eddy", name);

			Assert.IsTrue(request.Params.Contains("edward"));
			Assert.IsTrue(request.Params.Contains("SecondParam"));
		}
	}
}
