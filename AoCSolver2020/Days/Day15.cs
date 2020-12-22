using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day15 : DayBase
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

		AssertTest(0, "0,3,6", 10);
		AssertTest(1, "1,3,2", 2020);
		AssertTest(10, "2,1,3", 2020);
		AssertTest(27, "1,2,3", 2020);
		AssertTest(78, "2,3,1", 2020);
		AssertTest(438, "3,2,1", 2020);
		AssertTest(1836, "3,1,2", 2020);

		return PlayGame(InputParser.ListOfInts(input), 2020, false);
	}


	private void AssertTest(long expected, string numbers, int turns)
	{
		var res = PlayGame(InputParser.ListOfInts(numbers), turns);
		var col = expected != res ? ConsoleColor.Red : ConsoleColor.Green;
		var error = "";
		if (expected < res) error = " - Actual is too BIG";
		else if (expected > res) error = " - Actual is too SMALL";
		Console.WriteLine($"{numbers} - Expected : {expected} - Actual : {res} {error}", col);
	}

	private long PlayGame(int[] numbers, int turns, bool print = false)
	{
		var memory = new Dictionary<int, (int Recent, int Old)>();
		for (int i = 1; i <= numbers.Length; i++)
		{
			if (print)
				Console.WriteLine($"Turn {i} - {numbers[i - 1]}");
			memory.Add(numbers[i - 1], (i, -1));
		}

		var last = numbers[^1];
		for (int i = numbers.Length + 1; i <= turns; i++)
		{
			if (print)
				Console.WriteLine($"Turn {i} - Last {last} at turn {memory?[last]}");
			if (memory.ContainsKey(last) && memory[last].Old != -1)
				last = memory[last].Recent - memory[last].Old;
			else
				last = 0;

			if (!memory.ContainsKey(last))
				memory.Add(last, (i, -1));
			else
				memory[last] = (i, memory[last].Recent);


			if (print)
				Console.WriteLine($"{last}");
		}

		return last;
	}

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		return PlayGame(InputParser.ListOfInts(input), 30000000, false);
	}

}
