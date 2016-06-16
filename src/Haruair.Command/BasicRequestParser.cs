using System;
using System.Linq;
using Haruair.Command.Interface;
using System.Collections.Generic;

namespace Haruair.Command
{
	public class BasicRequestParser : IRequestParser
	{
		public string OptionFlag = "--";

		public IRequest Parse(string[] args)
		{
			var request = new Request();
			var arguments = new List<string>(args);

			request.Command = arguments.ElementAtOrDefault(0);
			request.Method = arguments.ElementAtOrDefault(1);

			var rest = arguments.Skip(2);

			var isOption = false;
			string optionName = null;

			foreach (var item in rest)
			{
				if (item.IndexOf(OptionFlag, 0, OptionFlag.Length, StringComparison.Ordinal) > -1)
				{
					if (isOption == true)
						request.Options.Add(optionName, "true");
					isOption = true;
					optionName = item.Substring(OptionFlag.Length);
				}
				else if (isOption == true)
				{
					request.Options.Add(optionName, item);
					isOption = false;
					optionName = null;
				}
				else {
					request.Params.Add(item);
				}
			}

			if (isOption == true)
			{
				request.Options.Add(optionName, "true");
			}

			return request;
		}
	}
}
