using System;

namespace Haruair.Command
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class Command : Attribute
	{
		public string Alias
		{
			get;
			private set;
		}
		public string Method
		{
			get;
			private set;
		}
		public Command(string method)
		{
			Method = method;
			Alias = null;
		}
		public Command(string method, string alias)
		{
			Method = method;
			Alias = alias;
		}
	}
}
