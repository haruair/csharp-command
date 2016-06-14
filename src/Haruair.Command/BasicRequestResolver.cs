using System;
using Haruair.Command.Interface;

namespace Haruair.Command
{
	public class BasicRequestResolver : IRequestResolver
	{
		public Request Resolve(string[] args)
		{
			var request = new Request ();

			if (args.Length >= 1) {
				request.Command = args [0];
			}

			if (args.Length >= 2) {
				request.Method = args [1];
			}

			return request;
		}
	}
}

