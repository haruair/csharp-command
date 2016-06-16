using System;

namespace Haruair.Command
{
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method, AllowMultiple = true)]
	public class Parameter : Attribute
	{
		public string Attribute
		{
			get;
			set;
		}

		public bool Required
		{
			get;
			set;
		}

		public Parameter(string attribute, bool required = true)
		{
			Attribute = attribute;
			Required = required;
		}
	}
}
