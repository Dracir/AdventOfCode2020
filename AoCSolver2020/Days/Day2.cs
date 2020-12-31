using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class Day2 : DayBase
{

	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
	}

	//-----------------------------------------------------------------

	public override void SetUpConsolePart2()
	{
	}

	//-----------------------------------------------------------------

	public override void CleanUp()
	{
	}

	//-----------------------------------------------------------------

	public override bool Equals(object obj)
	{
		return base.Equals(obj);
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}


	//-----------------------------------------------------------------

	//5-6 m: mmmmgm
	private Day2PasswordEntry ParseInputLine(string input)
	{
		var splitedInput = input.Split(" ");
		var entry = new Day2PasswordEntry();
		var splitedRange = splitedInput[0].Split("-");

		entry.Range = new RangeInt(int.Parse(splitedRange[0]), int.Parse(splitedRange[1]));
		entry.CharNeeded = splitedInput[1][0];
		entry.Password = splitedInput[2];

		return entry;
	}

	private struct Day2PasswordEntry
	{
		public RangeInt Range;
		public char CharNeeded;
		public string Password;
	}

	//-----------------------------------------------------------------

	public override long Part1(string input)
	{
		return input
			.Split("\n")
			.Select(x => ParseInputLine(x))
			.Where(x => x.Range.Contains(x.Password.Count(letter => letter == x.CharNeeded)))
			.Count();
	}

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		return input
			.Split("\n")
			.Select(x => ParseInputLine(x))
			.Where(x => x.Password[x.Range.Min - 1] == x.CharNeeded ^ x.Password[x.Range.Max - 1] == x.CharNeeded)
			.Count();
	}

}
