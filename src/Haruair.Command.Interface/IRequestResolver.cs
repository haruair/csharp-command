using System;

namespace Haruair.Command.Interface
{
	/// <summary>
	/// RequestResolver Interface
	/// </summary>
	public interface IRequestResolver
	{
		/// <summary>
		/// Resolve string array for input of the consoles
		/// </summary>
		/// <param name="args">Arguments.</param>
		IRequest Resolve(string[] args);
	}
}

