
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Console = ConsoleManager;

public class TestDay : DayBase
{

	static Random RandomGen = new Random();
	private HeaderValue? _itteration;
	private HeaderValue? _aFloat;
	private HeaderValue? _aFormatedFloat;
	private HeaderValue? _percent1;
	private HeaderValue? _percent2;
	private HeaderValue? _percent3;
	private HeaderValue? _percent4;
	private HeaderValue? _percent5;
	private HeaderValue? _percent6;

	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
		Console.Header.ReserveLines(10);
		_itteration = Console.Header.CreateFormatedValue(5, "Itteration: ");

		_itteration.SetValue(10);
		_aFloat = Console.Header.CreateFormatedValue(6, "Float: ");
		_aFormatedFloat = Console.Header.CreateFormatedValue(6, "Formated Float: ", "F3");

		_percent1 = Console.Header.CreateFormatedValue(6, "Percent: ");
		_percent2 = Console.Header.CreateBlockValue(2, "Percent: ", ValueToUTFBars.Styles.Horizontal);
		_percent3 = Console.Header.CreateBlockValue(5, "Percent: ", ValueToUTFBars.Styles.Vertical);
		_percent4 = Console.Header.CreateBlockValue(10, "Percent: ", ValueToUTFBars.Styles.Shades);
		_percent5 = Console.Header.CreateBlockValue(15, "Percent: ", ValueToUTFBars.Styles.Circle);
		_percent6 = Console.Header.CreateBlockValue(1, "Percent: ", ValueToUTFBars.Styles.CenteredVerticalBar);
	}
	//-----------------------------------------------------------------

	public override void SetUpConsolePart2()
	{
		Console.Header.ReserveLines(1);
		_itteration = Console.Header.CreateFormatedValue(5, "Itteration: ");

		_itteration.SetValue(10);
		_aFloat = Console.Header.CreateFormatedValue(6, "Float: ");
		_percent1 = Console.Header.CreateFormatedValue(6, "Percent: ");
		_percent2 = Console.Header.CreateBlockValue(2, "Percent: ", ValueToUTFBars.Styles.Horizontal);
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
		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(100);
			_itteration?.SetValue(i);
			var f = (float)RandomGen.NextDouble();
			_aFloat?.SetValue(f);
			_aFormatedFloat?.SetValue(f);
			Console.WriteLine("Itteration " + i);


			var percent = i * 1f / 9f;
			_percent1?.SetValue(percent);
			_percent2?.SetValue(percent);
			_percent3?.SetValue(percent);
			_percent4?.SetValue(percent);
			_percent5?.SetValue(percent);
			_percent6?.SetValue(percent);
		}

		return (long)(RandomGen.NextDouble() * 1000);
	}

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(100);
			_itteration?.SetValue(i);
			var f = (float)RandomGen.NextDouble();
			_aFloat?.SetValue(f);
			Console.WriteLine("Generation " + i);


			var percent = i * 1f / 9f;
			_percent1?.SetValue(percent);
		}

		return (long)(RandomGen.NextDouble() * 1000);
	}

}
