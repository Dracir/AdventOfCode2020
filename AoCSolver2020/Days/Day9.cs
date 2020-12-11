using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day9 : DayBase
{
	private HeaderValue? _progress;
	private HeaderValue? _currentIndex;
	private HeaderValue? _preamble;
	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
		Console.Header.ReserveLines(2);
		_progress = Console.Header.CreateBlockValue(40, "Progress: ", ValueToUTFBars.Styles.Shades);
		_currentIndex = Console.Header.CreateFormatedValue(4, "Index: ");
		Console.Header.ForceNewLine();
		_preamble = Console.Header.CreateFormatedValue(Console.Width - 15, "Preamble: ");
	}

	//-----------------------------------------------------------------

	public override void SetUpConsolePart2()
	{
		Console.Header.ReserveLines(1);
		_progress = Console.Header.CreateBlockValue(40, "Progress: ", ValueToUTFBars.Styles.Shades);
		_currentIndex = Console.Header.CreateFormatedValue(4, "Index: ");
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
		var numbers = InputParser.ListOfLong(input);
		var preambleSize = 25;
		var index = FindInvalidIndexe(numbers, preambleSize);
		return numbers[index];
	}

	private int FindInvalidIndexe(long[] numbers, int preambleSize)
	{
		var toParse = numbers.Length - preambleSize;
		_preamble?.SetValue(string.Join(",", numbers.Take(preambleSize)));
		var invalidIndex = new List<int>();

		for (int i = preambleSize; i < numbers.Length; i++)
		{
			_progress?.SetValue(i / (float)toParse);
			_currentIndex?.SetValue(i);
			var previous = numbers.Skip(i - preambleSize).Take(preambleSize).ToList();
			var validSum = GetValidSum(numbers[i], previous);
			var indexStr = i.ToString("000");
			if (!validSum.HasValue)
			{
				Console.WriteLine($"{indexStr}: Found nothing for number {numbers[i]}", ConsoleColor.Red);
				invalidIndex.Add(i);
				_progress?.SetValue(1f);
				return i;
			}
			else
			{
				var value = validSum.Value;
				Console.WriteLine($"{indexStr}: {previous[value.i]} + {previous[value.j]} = {numbers[i]}");
			}
		}
		_progress?.SetValue(1f);
		return 0;
	}

	private (int i, int j)? GetValidSum(long targetNumber, List<long> previousNumbers)
	{
		for (int i = 0; i < previousNumbers.Count; i++)
		{
			for (int j = 0; j < previousNumbers.Count; j++)
			{
				if (i == j) continue;
				if (previousNumbers[i] + previousNumbers[j] == targetNumber)
					return (i, j);
			}
		}
		return null;
	}

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		var numbers = InputParser.ListOfLong(input);
		var targetSum = 21806024;
		//var targetSum = 127;
		var firstConsecutive = FindContiguousSum(numbers.ToList(), targetSum);

		_progress?.SetValue(1f);
		if (firstConsecutive.HasValue)
		{
			var range = numbers.Skip(firstConsecutive.Value.indexStart)
				.Take(firstConsecutive.Value.indexEnd - firstConsecutive.Value.indexStart)
				.ToList();
			return range.Min() + range.Max();
		}
		return 0;
	}

	private (int indexStart, int indexEnd)? FindContiguousSum(List<long> numbers, long target)
	{
		for (int left = 0; left < numbers.Count - 1; left++)
		{
			var sum = numbers[left];
			var right = left;

			_progress?.SetValue(left / (float)numbers.Count);
			_currentIndex?.SetValue(left);
			do
			{
				right++;
				sum += numbers[right];
				if (sum == target)
					return (left, right);
			} while (sum < target && right < numbers.Count);

			Console.WriteLine($"{left.ToString("000")} to {right.ToString("000")} = {sum}");
		}
		return null;
	}
}
