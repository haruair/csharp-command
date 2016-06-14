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
	}
}
