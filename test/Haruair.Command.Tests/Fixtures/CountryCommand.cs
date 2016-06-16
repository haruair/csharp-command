using System;

namespace Haruair.Command.Tests.Fixtures
{
	[Command("country")]
	[Usage("Country Related Command")]
	public class CountryCommand
	{
		[Command("where")]
		[Usage("Country check now. No GPS Search.")]
		[Parameter("here")]
		public void WhereIAm(string here)
		{
			Console.WriteLine("Where are you? {0}?", here);
		}

		[Command("add")]
		[Usage("Add new country.")]
		[Parameter("country")]
		[Parameter("lat", Required = false)]
		[Parameter("lng", Required = false)]
		public void AddCountry(string country, string lat, string lng)
		{
			Console.WriteLine("Add new country {0}", country);
			Console.WriteLine("lat is {0}", lat ?? "null");
			Console.WriteLine("lng is {0}", lng ?? "null");
		}

		[Command("insert", "i")]
		[Usage("Add new country, in a very unusual way.")]
		[Parameter("lat")]
		[Parameter("lng")]
		[Parameter("country")]
		public void InsertCountry(string country, string lat, string lng)
		{
			Console.WriteLine("Add new country {0}", country);
			Console.WriteLine("lat is {0}", lat ?? "null");
			Console.WriteLine("lng is {0}", lng ?? "null");
		}
	}
}
