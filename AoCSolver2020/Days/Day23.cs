using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day23 : DayBase
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
		LinkedList<int> cups = PlayGameWithLinkedList(input, 100, 10, true);

		Console.WriteLine($"-- Final --\ncups: {cups.GetPrintStr()}");

		var score = "";
		var scoreCup = cups.Find(1);
		while (cups.Count != 0)
		{
			var lastCup = scoreCup;
			scoreCup = scoreCup.NextOrFirst();
			cups.Remove(lastCup);
			if (scoreCup != null)
				score += scoreCup.Value.ToString();
		}
		score = score.Remove(score.Length - 1);

		return long.Parse(score);
	}

	private static LinkedList<int> PlayGameWithLinkedList(string input, int moves, int nbCups, bool print)
	{
		var cups = new LinkedList<int>(input.Select(x => int.Parse(x.ToString())));

		for (int i = cups.Max(x => x) + 1; i < nbCups; i++)
			cups.AddLast(i);

		var currentCup = cups.First;
		for (int move = 1; move <= moves; move++)
		{
			if (move % (moves / 100) == 0)
				Console.WriteLine($"Moved {move}");

			var cupStr = cups.GetPrintStr();

			var pickUp1 = currentCup.NextOrFirst();
			cups.Remove(pickUp1);
			var pickUp2 = currentCup.NextOrFirst();
			cups.Remove(pickUp2);
			var pickUp3 = currentCup.NextOrFirst();
			cups.Remove(pickUp3);

			var destination = currentCup.Value;
			while (!cups.Contains((--destination >= 0) ? destination : (destination = 9))) ;

			if (print)
				Console.WriteLine($"-- move {move} --\ncups: {cupStr}"
					+ $"\nPick up: {pickUp1!.Value}, {pickUp2!.Value}, {pickUp3!.Value}"
					+ $"\nDestination: {destination}\n");

			cups.AddAfter(cups.Find(destination), pickUp1);
			cups.AddAfter(pickUp1, pickUp2);
			cups.AddAfter(pickUp2, pickUp3);

			currentCup = currentCup.NextOrFirst();
		}

		return cups;
	}

	//-----------------------------------------------------------------

	//934001 too low
	//
	public override long Part2(string input)
	{
		//LinkedList<int> cups = PlayGameWithLinkedList(input, 10, 20, true);
		//LinkedList<int> cups = PlayGameWithLinkedList(input, 10000000, 1000000, false);
		//LinkedList<int> cups = PlayGameWithLinkedList(input, 10000, 10000, false);
		//LinkedList<int> cups = PlayGameWithArray(input, 10000, 10000, false);
		//LinkedList<int> cups = PlayGameWithArray(input, 3, 9, true);
		//LinkedList<int> cups = PlayGameWithDictionnaryLinkedList(input, 10, 9, true);
		return PlayGameWithDictionnaryLinkedList(input, 10000000, 1000000);
	}

	private static long PlayGameWithDictionnaryLinkedList(string input, int moves, int nbCups)
	{
		var remove = new System.Diagnostics.Stopwatch();
		var find = new System.Diagnostics.Stopwatch();
		var AddAfter = new System.Diagnostics.Stopwatch();

		var cups = new LinkedList<int>(input.Select(x => int.Parse(x.ToString())));
		var dict = new Dictionary<int, LinkedListNode<int>>();
		for (int i = 1; i <= 9; i++)
			dict.Add(i, cups.Find(i));

		for (int i = cups.Max(x => x) + 1; i <= nbCups; i++)
		{
			cups.AddLast(i);
			dict.Add(i, cups.Last);
		}
		Console.WriteLine($"Init done!");

		var all = new System.Diagnostics.Stopwatch();
		all.Start();

		var currentCup = cups.First;
		for (int move = 1; move <= moves; move++)
		{
			if (moves > 100 && move % (moves / 100) == 0)
				Console.WriteLine($"Moved {move}");

			remove.Start();
			var pickUp1 = currentCup.NextOrFirst();
			cups.Remove(pickUp1);
			var pickUp2 = currentCup.NextOrFirst();
			cups.Remove(pickUp2);
			var pickUp3 = currentCup.NextOrFirst();
			cups.Remove(pickUp3);
			remove.Stop();

			find.Start();
			var destinationValue = currentCup.Value;
			while (destinationValue == pickUp1.Value || destinationValue == pickUp2.Value || destinationValue == pickUp3.Value || destinationValue == currentCup.Value)
				destinationValue = --destinationValue >= 1 ? destinationValue : nbCups;
			find.Stop();

			AddAfter.Start();
			cups.AddAfter(dict[destinationValue], pickUp1);
			cups.AddAfter(pickUp1, pickUp2);
			cups.AddAfter(pickUp2, pickUp3);
			AddAfter.Stop();

			currentCup = currentCup.NextOrFirst();
		}

		all.Stop();

		Console.WriteLine($"remove : {remove.Elapsed.ToString()}");
		Console.WriteLine($"AddAfter : {AddAfter.Elapsed.ToString()}");
		Console.WriteLine($"find : {find.Elapsed.ToString()}");
		Console.WriteLine($"all : {all.Elapsed.ToString()}");
		Console.WriteLine($"Cups immediately clockwise of cup 1: {dict[1].Next.Value}, {dict[1].Next.Next.Value}");
		return (long)dict[1].Next.Value * (long)dict[1].Next.Next.Value;
	}

	private static long PlayPart2GameWithLinkedList(string input, int moves, int nbCups)
	{
		var remove = new System.Diagnostics.Stopwatch();
		var find = new System.Diagnostics.Stopwatch();
		var AddAfter = new System.Diagnostics.Stopwatch();

		var cups = new LinkedList<int>(input.Select(x => int.Parse(x.ToString())));
		for (int i = cups.Max(x => x) + 1; i <= nbCups; i++)
		{
			cups.AddLast(i);
		}
		Console.WriteLine($"Init done!");

		var all = new System.Diagnostics.Stopwatch();
		all.Start();

		var currentCup = cups.First;
		for (int move = 1; move <= moves; move++)
		{
			remove.Start();
			var pickUp1 = currentCup.NextOrFirst();
			cups.Remove(pickUp1);
			var pickUp2 = currentCup.NextOrFirst();
			cups.Remove(pickUp2);
			var pickUp3 = currentCup.NextOrFirst();
			cups.Remove(pickUp3);
			remove.Stop();

			find.Start();
			var destinationValue = currentCup.Value;
			while (destinationValue == pickUp1.Value || destinationValue == pickUp2.Value || destinationValue == pickUp3.Value || destinationValue == currentCup.Value)
				destinationValue = --destinationValue >= 1 ? destinationValue : nbCups;
			find.Stop();

			AddAfter.Start();
			cups.AddAfter(cups.Find(destinationValue), pickUp1);
			cups.AddAfter(pickUp1, pickUp2);
			cups.AddAfter(pickUp2, pickUp3);
			AddAfter.Stop();

			currentCup = currentCup.NextOrFirst();
		}

		all.Stop();

		Console.WriteLine($"remove : {remove.Elapsed.ToString()}");
		Console.WriteLine($"AddAfter : {AddAfter.Elapsed.ToString()}");
		Console.WriteLine($"find : {find.Elapsed.ToString()}");
		Console.WriteLine($"all : {all.Elapsed.ToString()}");
		var node1 = cups.Find(1);
		Console.WriteLine($"Cups immediately clockwise of cup 1: {node1.Next.Value}, {node1.Next.Next.Value}");
		return (long)node1.Next.Value * (long)node1.Next.Next.Value;
	}

	private static LinkedList<int> PlayGameWithArray(string input, int moves, int nbCups, bool print)
	{
		var cups = new List<int>(input.Select(x => int.Parse(x.ToString())));
		for (int i = cups.Max(x => x) + 1; i <= nbCups; i++)
			cups.Add(i);

		var cupsNextValue = new int[nbCups + 1];
		var cupsPosition = new int[nbCups + 1];

		cupsNextValue[0] = -1;

		for (int i = 0; i < nbCups; i++)
			cupsPosition[cups[i]] = i;

		for (int i = 1; i < nbCups + 1; i++)
			cupsNextValue[i] = 1 + cupsPosition.Skip(1).FirstBy(x => x == ((cupsPosition[i] + 1 == nbCups) ? 0 : cupsPosition[i] + 1)).Item1;

		var currentCupValue = cups[0];
		var cupStr = "";

		for (int move = 1; move <= moves; move++)
		{
			if (move > 100 && (move % (moves / 100) == 0))
				Console.WriteLine($"Moved {move}");

			if (print && nbCups < 10)
			{
				/*var valuesInOrder = new char[nbCups];
				var strBuilding = new List<int>();
				var searchingValue = cupsNextValue[cupsNextValue[1]];
				while (searchingValue != cupsNextValue[1])
				{
					strBuilding.Add(searchingValue);
					searchingValue = cupsNextValue[searchingValue];
				}
				cupStr = new String(strBuilding.Select(x => x == -1 ? '(' : x == -2 ? ')' : x.ToString()[0]).ToArray());*/
				/*for (int cupValue = 1; cupValue <= nbCups; cupValue++)
					valuesInOrder[cupsPosition[cupValue]] = cupValue.ToString()[0];

				var strbuilding = valuesInOrder.ToList();
				strbuilding.Insert(cupsPosition[currentCupValue], '(');
				strbuilding.Insert(cupsPosition[currentCupValue] + 2, ')');
				cupStr = new string(strbuilding.ToArray());*/
			}

			var pickUp1Value = cupsNextValue[currentCupValue];
			var pickUp2Value = cupsNextValue[pickUp1Value];
			var pickUp3Value = cupsNextValue[pickUp2Value];
			cupsNextValue[currentCupValue] = cupsNextValue[pickUp3Value];

			var destinationValue = currentCupValue;

			while (destinationValue == pickUp1Value || destinationValue == pickUp2Value || destinationValue == pickUp3Value || destinationValue == currentCupValue)
				destinationValue = --destinationValue >= 1 ? destinationValue : nbCups;

			if (print && nbCups < 100)
				Console.WriteLine($"-- move {move} --\ncups: {cupStr}"
					+ $"\nPick up: {pickUp1Value}, {pickUp2Value}, {pickUp3Value}"
					+ $"\nDestination: {destinationValue}\n");

			cupsNextValue[pickUp3Value] = cupsNextValue[destinationValue];
			cupsNextValue[destinationValue] = pickUp1Value;

			currentCupValue = destinationValue;
		}

		return null;
	}

}
