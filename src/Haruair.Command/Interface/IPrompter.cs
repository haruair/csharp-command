using System;

namespace Haruair.Command.Interface
{
	public interface IPrompter
	{
		void Write(string value);
		void Write(string value, params object[] arg);

		void WriteLine();
		void WriteLine(string value);
		void WriteLine(string value, params object[] arg);
	}
}
