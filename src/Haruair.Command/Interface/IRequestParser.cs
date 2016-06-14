using System;

namespace Haruair.Command.Interface
{
	/// <summary>
	/// RequestParser Interface
	/// </summary>
	public interface IRequestParser
	{
		/// <summary>
		/// Parse string array for input of the consoles
		/// </summary>
		/// <param name="args">Arguments.</param>
		IRequest Parse(string[] args);
	}
}

