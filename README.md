CSharp-Command
==============

Attributes based Command-line interface for C#.

[![Build status](https://ci.appveyor.com/api/projects/status/github/haruair/csharp-command?branch=master&svg=true)](https://ci.appveyor.com/project/haruair/csharp-command/branch/master)

## Example

```csharp
using System;
using Haruair.Command;

[Command("hello", "h")]
[Usage("Hello Command. Nothing Special.")]
public class HelloCommand
{
	[Command("say", "s")]
	[Usage("When you want to say something, you can use it.")]
	public void Say() {
		Console.WriteLine ("Yo. You called me.");
	}
}

[Command("time", "t")]
[Usage("Check the system time.")]
public class TimeCommand
{
	[Command("now")]
	public void Now() {
		Console.WriteLine ("{0}", DateTime.Now);
	}
}


class ConsoleApp 
{
	public static void Main (string[] args)
	{
		var commander = new Commander();
		commander.Add (typeof(HelloCommand));
		commander.Add (typeof(TimeCommand));
		commander.Run (args);
	}
}
```

## Usage

```bash
$ ./ConsoleApp.exe

Example: 
  hello, h	Hello Command. Nothing Special.
  time, t	Check the system time.

$ ./ConsoleApp.exe hello

Example of hello:
  say, s	When you want to say something, you can use it.

$ ./ConsoleApp.exe hello say

Yo. You called me.
```

## License

MIT License

