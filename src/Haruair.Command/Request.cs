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
		} = new List<string> ();

		public IDictionary<string, string> Options {
			get;
			set;
		} = new Dictionary<string, string>();

	}
}
