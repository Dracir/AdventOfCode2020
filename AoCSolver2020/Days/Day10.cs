using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day10 : DayBase
{
	HeaderValue[] Frequency = new HeaderValue[5];

	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
		Console.Header.ReserveLines(0);
	}

	//-----------------------------------------------------------------

	public override void SetUpConsolePart2()
	{
		Console.Header.ReserveLines(5);
		for (int i = 0; i < 5; i++)
		{
			Frequency[i] = Console.Header.CreateBlockValue(50, $"Freqency of {i + 1}s:  ", ValueToUTFBars.Styles.Vertical);
			Console.Header.ForceNewLine();
		}
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
		var adapters = InputParser.ListOfInts(input).ToList();
		adapters.Add(0);
		adapters.Add(adapters.Max() + 3);
		adapters.Sort();

		var joltDiff = new int[4];
		for (int i = 0; i < adapters.Count - 1; i++)
			joltDiff[adapters[i + 1] - adapters[i]]++;
		for (int i = 1; i < 4; i++)
			Console.WriteLine($"There is {joltDiff[i]} differences of {i} jolt.");
		return joltDiff[1] * joltDiff[3];
	}

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		var adapters = InputParser.ListOfInts(input).ToList();
		adapters.Add(0);
		adapters.Add(adapters.Max() + 3);
		adapters.Sort();

		var jumps = new int[adapters.Count - 1];
		for (int i = 0; i < adapters.Count - 1; i++)
			jumps[i] = adapters[i + 1] - adapters[i];

		//Console.WriteLine(string.Join(',', jumps.Select(x => x.ToString())));

		var continuousGroups = GetContinuousGroups(jumps);
		var groupsLenghts = continuousGroups.Select(x => x.Count);
		var groupsFrequency = new int[groupsLenghts.Max() + 1];

		foreach (var length in groupsLenghts)
		{
			groupsFrequency[length]++;
			Frequency[length - 1].SetValue(groupsFrequency[length] / 20f);
		}

		//foreach (var group in continuousGroups)
		//	Console.WriteLine("Group : " + string.Join(",", group.Select(x => x.ToString())));

		var ways = 1L;
		var index = 0;
		foreach (var group in continuousGroups)
		{
			var frequency = group.Count;
			if (frequency == 1)
				ways *= 1;
			if (frequency == 2)
				ways *= 1;
			if (frequency == 3)
				ways *= 2;
			if (frequency == 4)
				ways *= 4;
			if (frequency == 5)
				ways *= 7;

			var groupStr = string.Join(",", group.Select(x => x.ToString()));
			var istr = index++.ToString("00");
			Console.WriteLine($"{istr} : [{groupStr}] -> {ways}");
		}

		return ways; //19208
	}

	private List<List<int>> GetContinuousGroups(int[] jumps)
	{
		var continousGroups = new List<List<int>>();
		var group = new List<int>();

		for (int i = 0; i < jumps.Length; i++)
		{
			group.Add(jumps[i]);
			if (jumps[i] == 3)
			{
				if (group.Count != 0)
				{
					continousGroups.Add(group);
					group = new List<int>();
				}
			}
		}

		return continousGroups;
	}
}
