CSharp-Command
==============

[![Build status](https://ci.appveyor.com/api/projects/status/github/haruair/csharp-command?branch=master&svg=true)](https://ci.appveyor.com/project/haruair/csharp-command/branch/master) [![NuGet](https://img.shields.io/nuget/v/Haruair.Command.svg)](https://www.nuget.org/packages/Haruair.Command/)

Attributes based Command-line interface for C#.

## Example

### Simple

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

### Advanced

```csharp
using Haruair.Command.Interface;

public class CustomRequest : IRequest
{
	public string Command { get; set; }
	public string Method { get; set; }
	public string SomethingCustom { get; set; }
}

public class CustomRequestResolver : IRequestResolver
{
	public IRequest Resolve(string[] args)
	{
		var request = new CustomRequest();
		request.Command = "start";
		// Some custom code
		return request;
	}
}

class ConsoleApp 
{
	public static void Main (string[] args)
	{
		var commander = new Commander() {
			resolver = new CustomRequestResolver()
		};
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

```

```bash
$ ./ConsoleApp.exe hello

Example of hello:
  say, s	When you want to say something, you can use it.

```

```bash
$ ./ConsoleApp.exe hello say

Yo. You called me.

```

## License

MIT License

