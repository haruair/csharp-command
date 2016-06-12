using System;

namespace Haruair.Command
{
	[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method)]
	public class Usage : System.Attribute
	{
		public string Description {
			get; private set;
		}

		public Usage(string description) {
			this.Description = description;
		}
	}
}

