using System;

namespace Haruair.Command
{
	[System.AttributeUsage(System.AttributeTargets.Parameter | System.AttributeTargets.Method, AllowMultiple = true)]
	public class Parameter : System.Attribute
	{
		public string Attribute {
			get;
			set;
		}

		public bool Required {
			get;
			set;
		}

		public Parameter (string attribute, bool required = true)
		{
			this.Attribute = attribute;
			this.Required = required;
		}
	}
}

