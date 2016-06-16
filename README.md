CSharp-Command
==============

[![Build status](https://ci.appveyor.com/api/projects/status/github/haruair/csharp-command?branch=master&svg=true)](https://ci.appveyor.com/project/haruair/csharp-command/branch/master) [![NuGet](https://img.shields.io/nuget/v/Haruair.Command.svg)](https://www.nuget.org/packages/Haruair.Command/)

Attributes based Command-line interface for C#.

## Example

**MIND THE VERSION: It can be broken everything during the version 0.0.x.**

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
	public void Say()
	{
		Console.WriteLine("Yo. You called me.");
	}
}

[Command("time", "t")]
[Usage("Check the system time.")]
public class TimeCommand
{
	[Command("now")]
	public void Now()
	{
		Console.WriteLine("{0}", DateTime.Now);
	}
}


class ConsoleApp
{
	public static void Main(string[] args)
	{
		var commander = new Commander();
		commander.Add<HelloCommand>();
		commander.Add<TimeCommand>();
		commander.Parse(args);
	}
}
```

### Intermediate

```csharp
using System;
using Haruair.Command;

[Command("feed", "f")]
[Usage("Feeding related commands.")]
public class FeedCommand
{
	[Command("me", "m")]
	[Usage("Feeding yourself.")]
	public void FeedMe()
	{
		Console.WriteLine("I'm already full.");
	}
	[Command("monkey")]
	[Usage("Feeding the monkey.")]
	[Parameter("food", Required = false)]
	public void FeedMonkey(string food)
	{
		Console.WriteLine("You gave {0} to the monkey.", food ?? "banana");
	}
}

[Command("time", "t")]
[Usage("Time related commands.")]
public class OpeningHoursCommand
{
	string opening = "09:00 AM";
	string closing = "05:00 PM";

	[Command("open")]
	[Usage("Display or set opening time.")]
	[Parameter("newTime", Required = false)]
	public void OpeningTime(string newTime)
	{
		if (newTime != null)
		{
			// may you can store some permanent place..
			opening = newTime;
		}
		Console.WriteLine("Opening Hour: {0}", opening);
	}

	[Command("close")]
	[Usage("Display or set closing time.")]
	[Parameter("newTime", Required = false)]
	public void ClosingTime(string newTime)
	{
		if (newTime != null)
		{
			// may you can store some permanent place..
			closing = newTime;
		}
		Console.WriteLine("Closing Hour: {0}", closing);
	}
}

class ZooApp
{
	public static void Main(string[] args)
	{
		var commander = new Commander();
		commander.Add<FeedCommand>();
		commander.Add<OpeningHoursCommand>();
		commander.Parse(args);
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

public class CustomRequestParser : IRequestParser
{
	public IRequest Parse(string[] args)
	{
		var request = new CustomRequest();
		request.Command = "start";
		// Some custom code
		return request;
	}
}

class ConsoleApp
{
	public static void Main(string[] args)
	{
		var commander = new Commander()
		{
			RequestParser = new CustomRequestParser()
		};
		commander.Add(typeof(HelloCommand));
		// same as commander.Add<HelloCommand> ();
		commander.Add(typeof(TimeCommand));
		commander.Parse(args);
	}
}
```

## Usage

```bash
$ ./ZooApp.exe

Example:
  feed, f	Feeding related commands.
  time, t	Time related commands.

```

```bash
$ ./ZooApp.exe time

Example of time:
  open [newTime]	Display or set opening time.
  close [newTime]	Display or set closing time.

```

```bash
$ ./ZooApp.exe time open

Opening Hour: 09:00 AM

```

```bash
$ ./ZooApp.exe time open "09:30 AM"

Opening Hour: 09:30 AM

```

## License

MIT License
