using System;
using Haruair.Command.Interface;
using System.Collections.Generic;

namespace Haruair.Command
{
	public class BasicCommandResolver : ICommandResolver
	{
		public IList<Type> Commands {
			get;
			set;
		}

		public CommandMeta Match (IRequest request) {
			return null;
		}

		public IList<CommandMeta> Find (IRequest request) {
			return null;
		}
	}
}

