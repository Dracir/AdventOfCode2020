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

	public override bool Equals(object obj) => base.Equals(obj);
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
		//Part2With(inputLines[1]);
		AssertTest(3417, "x,7,x,5,x,x,8");
		AssertTest(3417, "17,x,13,19");
		AssertTest(754018, "67,7,59,61");
		AssertTest(779210, "67,x,7,59,61");
		AssertTest(1261476, "67,7,x,59,61");
		AssertTest(1202161486, "1789,37,47,1889");

		var inputLines = input.Split("\n");
		return Part2With(inputLines[1]);
	}

	private void AssertTest(long expected, string busesLine)
	{
		var res = Part2With(busesLine);
		var col = expected != res ? ConsoleColor.Red : ConsoleColor.Green;
		var error = "";
		if (expected < res) error = " - Actual is too BIG";
		else if (expected > res) error = " - Actual is too SMALL";
		Console.WriteLine($"{busesLine} - Expected : {expected} - Actual : {res} {error}", col);
	}

	//1925524193  too low
	//836024966345345
	private static long Part2With(string busesLine)
	{
		var busFleet = new List<(int Index, int Time)>();
		var index = 0;
		foreach (var busEntry in busesLine.Split(","))
		{
			if (int.TryParse(busEntry, out var number))
				busFleet.Add((index, number));
			index++;
		}
		//busFleet = busFleet.OrderBy(x => x.Time).ToList();

		var t = 0L;
		var step = 1L;

		foreach (var busEntry in busFleet)
		{
			while (((t += step) + busEntry.Index) % busEntry.Time != 0) ;
			step *= busEntry.Time;
			Console.WriteLine($"Bus {busEntry.Time} ({busEntry.Index}) sync at {t}");
		}

		return t;
	}
}
