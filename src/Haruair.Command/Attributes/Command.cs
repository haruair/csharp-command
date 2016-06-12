using System;

namespace Haruair.Command
{
	[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method)]
	public class Command : System.Attribute
	{
		public string Alias {
			get;
			private set;
		}
		public string Method {
			get;
			private set;
		}
		public Command(string method) {
			this.Method = method;
			this.Alias = null;
		}
		public Command(string method, string alias) {
			this.Method = method;
			this.Alias = alias;
		}
	}
}

