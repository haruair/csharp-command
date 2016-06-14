using NUnit.Framework;
using System;
using Haruair.Command.Interface;

namespace Haruair.Command.Tests
{
	[TestFixture ()]
	public class BasicRequestResolverTest
	{
		IRequestResolver resolver;

		[SetUp ()]
		public void Init()
		{
			resolver = new BasicRequestResolver ();
		}

		[TearDown ()]
		public void Dispose()
		{
			resolver = null;
		}

		[Test ()]
		public void EmptyArgsTestCase ()
		{
			var args = new string[] { };
			var request = resolver.Resolve (args);

			Assert.AreEqual (null, request.Command);
			Assert.AreEqual (null, request.Method);
		}

		[Test ()]
		public void CommandTestCase ()
		{
			var args = new string[] { "hello" };
			var request = resolver.Resolve (args);

			Assert.AreEqual ("hello", request.Command);
			Assert.AreEqual (null, request.Method);
		}

		[Test ()]
		public void CommandAndMethodTestCase ()
		{
			var args = new string[] { "hello", "world" };
			var request = resolver.Resolve (args);

			Assert.AreEqual ("hello", request.Command);
			Assert.AreEqual ("world", request.Method);
		}
	}
}

