using System;
using System.Collections.Generic;

namespace Haruair.Command.Interface
{
	public interface IRequest
	{
		string Command { get; set; }
		string Method { get; set; }
		IList<string> Params { get; set; }
		IDictionary<string, string> Options { get; set; }
	}
}

