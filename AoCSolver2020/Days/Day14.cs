using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Console = ConsoleManager;


public class Day14 : DayBase
{

	private static ulong MAX_VALUE = (2L << 35) - 1;
	private HeaderValue? _progress;
	private HeaderValue? _currentValue;
	private HeaderValue? _currentMask;
	private HeaderValue? _currentResult;
	private HeaderValue? _currentMemSize;
	private HeaderValue? _currentSum;


	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
		Console.Header.ReserveLines(5);
		_progress = Console.Header.CreateBlockValue(61, "Progress: ", ValueToUTFBars.Styles.Vertical);
		Console.Header.ForceNewLine();
		_currentValue = Console.Header.CreateFormatedValue(60, "Value   :  ");
		Console.Header.ForceNewLine();
		_currentMask = Console.Header.CreateFormatedValue(60, "Mask    :  ");
		Console.Header.ForceNewLine();
		_currentResult = Console.Header.CreateFormatedValue(60, "Result  :  ");
		Console.Header.ForceNewLine();
		_currentSum = Console.Header.CreateFormatedValue(60, "Sum     :  ");
	}

	//-----------------------------------------------------------------

	public override void SetUpConsolePart2()
	{
		Console.Header.ReserveLines(6);
		_progress = Console.Header.CreateBlockValue(60, "Progress :  ", ValueToUTFBars.Styles.Vertical);
		Console.Header.ForceNewLine();
		_currentValue = Console.Header.CreateFormatedValue(60, "Adress   :  ");
		Console.Header.ForceNewLine();
		_currentMask = Console.Header.CreateFormatedValue(60, "Mask     :  ");
		Console.Header.ForceNewLine();
		_currentResult = Console.Header.CreateFormatedValue(60, "Adresses :  ");
		Console.Header.ForceNewLine();
		_currentMemSize = Console.Header.CreateFormatedValue(60, "Mem Size :  ");
		Console.Header.ForceNewLine();
		_currentSum = Console.Header.CreateFormatedValue(60, "Sum      :  ");


	}

	//-----------------------------------------------------------------

	public override void CleanUp()
	{
	}

	//-----------------------------------------------------------------

	public override bool Equals(object? obj) => base.Equals(obj);
	public override int GetHashCode() => base.GetHashCode();

	//-----------------------------------------------------------------
	//2600183168364  too low
	public override long Part1(string input)
	{
		var mem = new Dictionary<int, ulong>();
		var mask = new List<(int Index, int Override)>();
		var keepGoing = false;

		var lines = input.Split("\n");
		for (int i = 0; i < lines.Length; i++)
		{
			var line = lines[i];
			_progress?.SetValue(1f * i / lines.Length);
			if (line.Contains("mask"))
			{
				mask = ParseMask(line);
				ShowMask(mask, line);
			}
			else
			{
				var write = ParseWrite(line);
				var valueMasked = MaskValue(write.Value, mask);
				mem[write.Key] = valueMasked;
				ShowValue(write.Value, valueMasked, mask);
				//Console.WriteLine($"Write at {write.Key} value {write.Value}, masked : {valueMasked}");
				if (_currentSum != null)
					_currentSum.SetValue(((long)mem.Select(x => (long)x.Value).Sum()).ToString());
			}
			if (!keepGoing)
			{
				if (Console.ReadKey().Key == ConsoleKey.Enter)
					keepGoing = true;
			}

		}
		_progress?.SetValue(1f);
		return (long)mem.Select(x => (long)x.Value).Sum();
	}

	private void ShowValue(ulong value, ulong valueMasked, List<(int Index, int Override)> mask)
	{
		if (_currentValue != null && _currentResult != null)
		{
			var strValue = Convert.ToString((long)value, 2).PadLeft(36, '0');
			var strValueMasked = Convert.ToString((long)valueMasked, 2).PadLeft(36, '0');
			_currentValue.SetValue(strValue + $"  (decimal {(long)value})");
			_currentResult.SetValue(strValueMasked + $"  (decimal {(long)valueMasked})");

			var c = System.Console.ForegroundColor;
			System.Console.ForegroundColor = ConsoleColor.White;
			foreach (var item in mask)
			{
				var index = 35 - item.Index;
				if (strValue[index] == '0' + item.Override)
					continue;
				if (strValueMasked[index] != '0' + item.Override)
					System.Console.ForegroundColor = ConsoleColor.Red;
				else
					System.Console.ForegroundColor = ConsoleColor.White;

				var x = _currentResult.Position.X + index;
				Console.WriteAt(strValueMasked[index], x, _currentResult.Position.Y);
			}
			System.Console.ForegroundColor = c;
		}
	}

	private void ShowMask(List<(int Index, int Override)> mask, string line)
	{
		if (_currentMask != null)
		{
			_currentMask.SetValue(line.Substring(7));
		}
	}

	private ulong MaskValue(ulong value, List<(int Index, int Override)> mask)
	{
		foreach (var maskValue in mask)
		{
			if (maskValue.Override == 1)
			{
				var v = (ulong)((long)maskValue.Override << maskValue.Index);
				value = value | v;
			}
			else if (maskValue.Override == 0)
			{
				var v = MAX_VALUE ^ (ulong)(1L << maskValue.Index);
				value = value & v;
			}
		}
		return value;
	}

	private KeyValuePair<int, ulong> ParseWrite(string line)
	{
		var key = Regex.Match(line, @"\[(\d+)\]").Groups.IntValue(1);
		var value = (ulong)Regex.Match(line, @"= (\d+)").Groups.LongValue(1);
		return new KeyValuePair<int, ulong>(key, value);
	}

	private static List<(int Index, int Override)> ParseMask(string line)
	{
		var newMask = new List<(int Index, int Override)>();
		for (int i = 0; i < 36; i++)
		{
			var c = line[7 + i];
			if (c == '1')
				newMask.Add((35 - i, 1));
			else if (c == '0')
				newMask.Add((35 - i, 0));
		}
		return newMask;
	}

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		var mem = new Dictionary<long, ulong>();
		var maskStr = "";
		var keepGoing = false;

		var lines = input.Split("\n");
		for (int i = 0; i < lines.Length; i++)
		{
			var line = lines[i];
			_progress?.SetValue(1f * i / lines.Length);
			if (line.Contains("mask"))
			{
				maskStr = line.Substring(7);
				_currentMask?.SetValue(maskStr);
			}
			else
			{
				var write = ParseWrite(line);
				var adressMask = MaskValueVersion2((ulong)write.Key, maskStr);
				var adresses = GetAdressesFromMask(adressMask);
				ShowValueVersion2((ulong)write.Key, adressMask, maskStr, adresses);

				foreach (var adress in adresses)
					mem[Convert.ToInt64(adress, 2)] = write.Value;

				_currentMemSize?.SetValue(mem.Count);

				if (_currentSum != null)
					_currentSum.SetValue(((long)mem.Select(x => (long)x.Value).Sum()).ToString());
			}
			if (!keepGoing)
			{
				if (Console.ReadKey().Key == ConsoleKey.Enter)
					keepGoing = true;
			}

		}
		_progress?.SetValue(1f);
		return (long)mem.Select(x => (long)x.Value).Sum();
	}

	private List<string> GetAdressesFromMask(string maskStr)
	{
		var adresses = new List<string>();

		var firstX = maskStr.IndexOf('X');
		if (firstX == -1)
		{
			adresses.Add(maskStr);
			return adresses;
		}
		else
		{
			var c = maskStr.ToCharArray();
			c[firstX] = '0';
			adresses.AddRange(GetAdressesFromMask(new string(c)));
			c = maskStr.ToCharArray();
			c[firstX] = '1';
			adresses.AddRange(GetAdressesFromMask(new string(c)));
		}

		return adresses;
	}

	private string MaskValueVersion2(ulong value, string mask)
	{
		var adresses = Convert.ToString((long)value, 2).PadLeft(36, '0').ToArray();
		for (int i = 0; i < 36; i++)
		{
			if (mask[i] == '1' && adresses[i] == '0')
				adresses[i] = '1';
			else if (mask[i] == 'X')
				adresses[i] = 'X';
		}
		return new string(adresses);
	}


	private void ShowValueVersion2(ulong value, string valueMasked, string mask, List<string> adresses)
	{
		if (_currentValue != null && _currentResult != null)
		{
			var strValue = Convert.ToString((long)value, 2).PadLeft(36, '0');
			_currentValue.SetValue(strValue + $"  (decimal {(long)value})");
			_currentResult.SetValue(valueMasked + $"  ({adresses.Count} posibilities)");

			var c = System.Console.ForegroundColor;
			System.Console.ForegroundColor = ConsoleColor.White;
			for (int i = 0; i < 36; i++)
			{
				var x = _currentResult.Position.X + i;
				if (mask[i] == '1' && strValue[i] == '0')
				{
					if (valueMasked[i] != '1')
						System.Console.ForegroundColor = ConsoleColor.Red;
					else
						System.Console.ForegroundColor = ConsoleColor.White;
					Console.WriteAt(valueMasked[i], x, _currentResult.Position.Y);
				}
				else if (mask[i] == 'X')
				{
					if (valueMasked[i] != 'X')
						System.Console.ForegroundColor = ConsoleColor.Red;
					else
						System.Console.ForegroundColor = ConsoleColor.White;
					Console.WriteAt('X', x, _currentResult.Position.Y);
				}
			}
			System.Console.ForegroundColor = c;
		}
	}
}
