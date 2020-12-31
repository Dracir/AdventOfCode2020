using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day1 : DayBase
{

	private HeaderValue _itteration;
	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
		Console.Header.ReserveLines(1);
		_itteration = Console.Header.CreateFormatedValue(5, "Itterations: ");
	}

	//-----------------------------------------------------------------

	public override void SetUpConsolePart2()
	{
		Console.Header.ReserveLines(1);
		_itteration = Console.Header.CreateFormatedValue(10, "Itterations: ");
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

	//-----------------------------------------------------------------

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	//-----------------------------------------------------------------

	public override long Part1(string input)
	{
		var inputValues = InputParser.ListOfInts(input).ToList();
		inputValues.Sort();
		int itteration = 0;
		var random = new Random();
		while (true)
		{
			int a = random.Next(inputValues.Count);
			int b = random.Next(inputValues.Count);
			if (inputValues[a] + inputValues[b] == 2020)
				return inputValues[a] * inputValues[b];
			itteration++;
			if (itteration % 10 == 0)
				_itteration?.SetValue(itteration);
		}
	}

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		var inputValues = InputParser.ListOfInts(input).ToList();
		inputValues.Sort();
		int itteration = 0;
		var random = new Random();
		while (true)
		{
			int a = random.Next(inputValues.Count);
			int b = random.Next(inputValues.Count);
			int c = random.Next(inputValues.Count);
			if (inputValues[a] + inputValues[b] + inputValues[c] == 2020)
				return inputValues[a] * inputValues[b] * inputValues[c];
			itteration++;
			if (itteration % 100 == 0)
				_itteration?.SetValue(itteration);
		}
	}

}
