using System;
using System.Reflection;

namespace Haruair.Command
{
	public class CommandMeta {
		public string Method {
			get;
			set;
		}
		public string Alias {
			get;
			set;
		}
		public string Description {
			get;
			set;
		}
		public Type Command {
			get;
			set;
		}
		public MethodInfo CallMethod {
			get;
			set;
		}
	}
}

