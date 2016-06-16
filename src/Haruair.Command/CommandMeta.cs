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
		public Type CommandType {
			get;
			set;
		}
		public MethodInfo MethodInfo {
			get;
			set;
		}
	}
}

