using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day25 : DayBase
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
		var doorPK = 12092626;
		var cardPK = 4707356;

		var cardLoopSize = FindLoopSize(7, cardPK);
		var doorLoopSize = FindLoopSize(7, doorPK);
		Console.WriteLine($"cardLoopSize : {cardLoopSize}");
		Console.WriteLine($"doorLoopSize : {doorLoopSize}");

		var encryptionKeyCard = TransformValue(cardPK, doorLoopSize);
		var encryptionKeyDoor = TransformValue(doorPK, cardLoopSize);
		Console.WriteLine($"encryptionKeyCard : {encryptionKeyCard}");
		Console.WriteLine($"encryptionKeyDoor : {encryptionKeyDoor}");

		return encryptionKeyDoor;
	}


	public long TransformValue(int subjectNumber, long loopSize)
	{
		var value = 1L;
		for (int i = 0; i < loopSize; i++)
		{
			value *= subjectNumber;
			value = value % 20201227;
		}

		return value;
	}

	public long FindLoopSize(int subjectNumber, int targetValue)
	{
		long loopSize = 0;
		var value = 1L;
		while (++loopSize < 10000000000L)
		{
			value *= subjectNumber;
			value = value % 20201227;
			if (targetValue == value)
				return loopSize;
		}

		return -loopSize;
	}

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		return 0;
	}

}
