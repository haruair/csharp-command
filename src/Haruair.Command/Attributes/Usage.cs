using System;

namespace Haruair.Command
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class Usage : Attribute
	{
		public string Description
		{
			get; private set;
		}

		public Usage(string description)
		{
			Description = description;
		}
	}
}
