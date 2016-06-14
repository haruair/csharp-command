using System;
using System.Collections.Generic;
using Haruair.Command.Interface;

namespace Haruair.Command
{
	public class Request : IRequest
	{
		public string Command {
			get;
			set;
		}
		public string Method {
			get;
			set;
		}
		public IList<string> Params {
			get;
			set;
		}
		public IDictionary<string, string> Options {
			get;
			set;
		}
	}
}
