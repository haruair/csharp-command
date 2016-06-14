using System;

namespace Haruair.Command.Interface
{
	public interface IRequest
	{
		string Command { get; set; }
		string Method { get; set; }
	}
}

