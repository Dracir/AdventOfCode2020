using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day13 : DayBase
{

	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
		Console.Header.ReserveLines(0);
	}

	//-----------------------------------------------------------------

	public override void SetUpConsolePart2()
	{
		Console.Header.ReserveLines(0);
	}

	//-----------------------------------------------------------------

	public override void CleanUp()
	{
	}

	//-----------------------------------------------------------------

	public override bool Equals(object? obj) => base.Equals(obj);
	public override int GetHashCode() => base.GetHashCode();

	//-----------------------------------------------------------------

	public override long Part1(string input)
	{
		var inputLines = input.Split("\n");
		var timestep = int.Parse(inputLines[0]);
		var busFleet = new List<int>();
		foreach (var busEntry in inputLines[1].Split(","))
		{
			if (int.TryParse(busEntry, out var number))
				busFleet.Add(number);
		}
		busFleet.Sort();
		var busTime = new List<int>();
		int smalestTime = int.MaxValue;
		int smalestBus = 0;
		foreach (var bus in busFleet)
		{
			var t = (int)Math.Ceiling((double)timestep / bus) * bus;
			Console.WriteLine($"{bus} - {t}");
			if (smalestTime > t)
			{
				smalestTime = t;
				smalestBus = bus;
			}
		}
		return (smalestTime - timestep) * smalestBus;
	}


	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		var inputLines = input.Split("\n");
		var timestep = int.Parse(inputLines[0]);
		var busFleet = new List<int>();
		foreach (var busEntry in inputLines[1].Split(","))
		{
			if (int.TryParse(busEntry, out var number))
				busFleet.Add(number);
		}
		busFleet.Sort();


		return 0;
	}

}
