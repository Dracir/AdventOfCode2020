using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day6 : DayBase
{

	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
		Console.Header.ReserveLines(1);
	}

	//-----------------------------------------------------------------

	public override void SetUpConsolePart2()
	{
		Console.Header.ReserveLines(1);
	}

	//-----------------------------------------------------------------

	public override void CleanUp()
	{
	}

	//-----------------------------------------------------------------

	public override bool Equals(object? obj)
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
		var groups = input.Split("\n\n").ToList();
		Console.WriteLine($"{groups.Count} groups to handle.");
		int totalQuestionAnswered = 0;

		foreach (var group in groups)
		{
			var yesAnswers = new int[26];
			var persons = group.Split("\n").ToList();
			Console.WriteLine($"With {persons.Count} persons to handle.");
			foreach (var person in persons)
			{
				foreach (var question in person)
				{
					yesAnswers[question - 'a']++;
				}
			}
			var nbQuestionAnswered = yesAnswers.Where(x => x != 0).Count();
			Console.WriteLine($"This group answered yes to {nbQuestionAnswered} different questions.");
			totalQuestionAnswered += nbQuestionAnswered;
			Console.WriteLine("\n");
		}

		return totalQuestionAnswered;
	}

	//-----------------------------------------------------------------
	public override long Part2(string input)
	{
		var groups = input.Split("\n\n").ToList();
		Console.WriteLine($"{groups.Count} groups to handle.");
		int totalQuestionAnswered = 0;

		foreach (var group in groups)
		{
			var yesAnswers = new int[26];
			var persons = group.Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToList();
			Console.WriteLine($"With {persons.Count} persons to handle :\n{string.Join('\n', persons.Select(x => " - " + x).ToList())}");

			foreach (var person in persons)
				foreach (var question in person)
					yesAnswers[question - 'a']++;

			var nbQuestionAnswered = yesAnswers.Where(x => x == persons.Count && x != 0).Count();
			Console.WriteLine($"This group answered yes to {nbQuestionAnswered} different questions.\n");
			totalQuestionAnswered += nbQuestionAnswered;
		}

		return totalQuestionAnswered;
	}

}
