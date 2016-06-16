using System;

namespace Haruair.Command
{
	[System.AttributeUsage(System.AttributeTargets.Parameter | System.AttributeTargets.Method, AllowMultiple = true)]
	public class Parameter : System.Attribute
	{
		public enum Required { Must, Optional };

		public string Attribute {
			get;
			set;
		}

		public Required Require {
			get;
			set;
		}

		public Parameter (string attribute, Required required = Required.Must)
		{
			this.Attribute = attribute;
			this.Require = required;
		}
	}
}

