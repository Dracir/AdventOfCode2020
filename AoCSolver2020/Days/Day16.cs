using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day16 : DayBase
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
		var data = Parse(input);
		var errorRate = data.Tickets
			.Skip(1)
			.SelectMany(x => x)
			.Where(x => !data.Fields.Any(
				YearFileCreator => YearFileCreator.Range1.Contains(x) ||
				YearFileCreator.Range2.Contains(x)
			));

		Console.WriteLine(string.Join(",", errorRate));
		return errorRate.Sum();
	}

	private Day16Data Parse(string input)
	{
		var splitted = input.Replace("\r", "").Split("\n\n");

		var fields = splitted[0].Split("\n");
		var tickets = splitted[2].Split("\n");

		var data = new Day16Data(fields.Length, tickets.Length + 1);

		data.Tickets.Add(InputParser.ListOfInts(splitted[1].Split("\n")[1]));
		for (int i = 1; i < tickets.Length; i++)
			data.Tickets.Add(InputParser.ListOfInts(tickets[i]));

		foreach (var field in fields)
		{
			var lineSplited = field.Split(":");
			var name = lineSplited[0];
			lineSplited = lineSplited[1].Split("or");
			var rangeA = ParseRange(lineSplited[0].Trim());
			var rangeB = ParseRange(lineSplited[1].Trim());
			data.Fields.Add(new Day16Fields(name, rangeA, rangeB));
		}

		return data;
	}

	private RangeInt ParseRange(string rangeString)
	{
		var splited = rangeString.Split("-");
		return new RangeInt(
			Int32.Parse(splited[0]),
			Int32.Parse(splited[1])
		);
	}

	struct Day16Data
	{
		public List<Day16Fields> Fields;
		public List<int[]> Tickets;

		public Day16Data(int fields, int tickets)
		{
			Fields = new List<Day16Fields>(fields);
			Tickets = new List<int[]>(tickets);
		}
	}

	struct Day16Fields
	{
		public string Name;
		public RangeInt Range1;
		public RangeInt Range2;

		public Day16Fields(string name, RangeInt range1, RangeInt range2)
		{
			Name = name;
			Range1 = range1;
			Range2 = range2;
		}
	}
	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		var data = Parse(input);
		var countBefore = data.Tickets.Count - 1;
		for (int i = data.Tickets.Count - 1; i > 0; i--)
		{
			var error = data.Tickets[i]
				.Any(x => !data.Fields.Any(
					YearFileCreator => YearFileCreator.Range1.Contains(x) ||
					YearFileCreator.Range2.Contains(x)
				));
			if (error)
				data.Tickets.RemoveAt(i);
		}

		Console.WriteLine($"From {countBefore} to {(data.Tickets.Count - 1)}");


		var columCandidates = new Dictionary<int, List<Day16Fields>>();
		for (int i = 0; i < data.Tickets[0].Length; i++)
		{
			var candidates = new List<Day16Fields>();
			var col = data.Tickets.Select(x => x[i]).ToList();

			foreach (var field in data.Fields)
				if (col.All(x => field.Range1.Contains(x) || field.Range2.Contains(x)))
					candidates.Add(field);

			columCandidates.Add(i, candidates);
		}

		var lastOne = new List<string>();
		foreach (var col in columCandidates.OrderBy(x => x.Value.Count))
		{
			var newOnes = col.Value.Select(x => x.Name).Where(x => !lastOne.Contains(x)).ToList();

			Console.WriteLine($"Col {col.Key} - new :[{string.Join(",", newOnes)}], with {col.Value.Count} candiates.");

			lastOne = col.Value.Select(x => x.Name).ToList();
			//Console.WriteLine($"Col {col.Key} - {col.Value.Count} candidates.");
		}

		var takenFields = new List<Day16Fields>();
		var answer = 1L;
		foreach (var col in columCandidates.OrderBy(x => x.Value.Count))
		{
			col.Value.RemoveAll(x => takenFields.Contains(x));
			var onlyRemainer = col.Value[0];
			takenFields.Add(onlyRemainer);
			if (onlyRemainer.Name.StartsWith("departure"))
				answer *= data.Tickets[0][col.Key];
		}

		return answer;
	}

}
