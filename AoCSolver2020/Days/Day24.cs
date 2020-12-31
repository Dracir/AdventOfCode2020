using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day24 : DayBase
{

	HexPreview<int> _hexPreview;

	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
		Console.Header.ReserveLines(30);
		_hexPreview = new HexPreview<int>(HexPreviewChar, new RectInt(0, 3, 60, 30));
		_hexPreview.GetTileColor = c => c % 2 == 1 ? ConsoleColor.DarkGray : ConsoleColor.White;
		_hexPreview.ReverseY = true;
	}

	private char HexPreviewChar(int value) => value switch
	{
		0 => '#',
		1 => '#',
		2 => '*',
		3 => '*',
		_ => ' ',
	};

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

	public override bool Equals(object obj) => base.Equals(obj);
	public override int GetHashCode() => base.GetHashCode();

	//-----------------------------------------------------------------

	public override long Part1(string input)
	{
		var HexMap = new HexMap<int>();
		if (_hexPreview != null)
		{
			_hexPreview.HexMap = HexMap;
			//_hexPreview.Offset = new Point(10, 10);

		}

		HexMap[0, 0].Value = 2;
		/*HexMap[0, 1].Value = 1;
		HexMap[0, 2].Value = 1;
		HexMap[0, 3].Value = 1;
		HexMap[1, 0].Value = 1;
		HexMap[2, 0].Value = 1;
		HexMap[1, 1].Value = 1;
		HexMap[2, 2].Value = 1;*/


		if (_hexPreview != null)
		{
			_hexPreview.Offset = new Point(-15, -15);
			_hexPreview.Update();
		}
		var flipOnce = 0;
		var flipBack = 0;

		foreach (var line in input.Split("\n"))
		{
			//Console.WriteLine($"{line}");
			var tile = HexMap[0, 0];

			tile = MoveTile(line, tile);

			var before = tile.Value;
			if (before == 0) flipOnce++; else flipBack++;
			tile.Value += (tile.Value % 2 == 0) ? 1 : -1;
			//Console.WriteLine($"{before} -> {tile.Value}");

			if (_hexPreview != null)
			{
				//var offsetX = HexMap.UsedWidth / 2 - HexMap.UsedMinX - _hexPreview.Viewport.Width / 2;
				//var offsetY = HexMap.UsedHeight / 2 - HexMap.UsedMinY - _hexPreview.Viewport.Height / 2;
				//_hexPreview.Offset = new Point(HexMap.UsedMinX, HexMap.UsedMinY);
				_hexPreview.Update();
			}
		}
		Console.WriteLine($"flip Once: {flipOnce}");
		Console.WriteLine($"flip Back: {flipBack}");

		foreach (var tile in HexMap._map.Where(x => (x.Value.Value % 2) == 1))
		{
			Console.WriteLine($"{tile.Key} : {tile.Value.Value}");
		}

		if (_hexPreview != null)
			_hexPreview.Update();
		return HexMap._map.Select(x => x.Value).Where(tile => tile.Value % 2 == 1).Count();
	}

	public static Hex<int> MoveTile(string movementLine, Hex<int> tile)
	{
		var acccumulator = "";
		foreach (var direction in movementLine)
		{
			acccumulator += direction;
			if (direction == 'e' || direction == 'w')
			{
				tile = MoveTile(tile, acccumulator);
				//	Console.WriteLine($"({tile.Q},{tile.R}) : {acccumulator}");
				acccumulator = "";
			}
		}
		return tile;
	}

	private static Hex<int> MoveTile(Hex<int> tile, string acccumulator) => acccumulator switch
	{
		"e" => tile.East,
		"se" => tile.SouthEast,
		"sw" => tile.SouthWest,
		"w" => tile.West,
		"nw" => tile.NorthWest,
		"ne" => tile.NorthEast,
		_ => tile
	};

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		return 0;
	}

}
