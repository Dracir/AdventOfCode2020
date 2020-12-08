using System.IO;

public static class YearFileCreator
{

	private static string MainFileText = @"using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day{DayX} : DayBase
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
	public override int GetHashCode()=> base.GetHashCode();

	//-----------------------------------------------------------------

	public override long Part1(string input)
	{
		return 0;
	}

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		return 0;
	}

}
";


	private static string TestFileText = @"using System.Collections;
using Xunit;

namespace Test
{
	public class Day{DayX}Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day{DayX}Part1Answer, new Day{DayX}().Part1(DaysInputs.D{DayX}));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day{DayX}Part2Answer, new Day{DayX}().Part2(DaysInputs.D{DayX}));
		}
	}
}
";

	public static void CreateYear()
	{
		for (int day = 8; day <= 25; day++)
		{
			var path = Path.Combine("DaysInput", $"Day{day}.txt");
			File.WriteAllText(path, "");

			path = Path.Combine("AoCSolver2020/Days", $"Day{day}.cs");
			var txt = MainFileText.Replace("{DayX}", $"{day}");
			File.WriteAllText(path, txt);

			/*path = Path.Combine("Tests/Days", $"Day{day}Tests.cs");
			txt = TestFileText.Replace("{DayX}", $"{day}");
			File.WriteAllText(path, txt);*/
		}
	}
}