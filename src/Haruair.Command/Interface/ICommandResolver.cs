using System;
using System.Collections.Generic;

namespace Haruair.Command.Interface
{
	public interface ICommandResolver
	{
		IList<Type> Commands {
			get;
			set;
		}

		CommandMeta Match (IRequest request);
		IList<CommandMeta> Find (IRequest request);
	}
}

