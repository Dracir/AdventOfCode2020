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
		Console.Header.ReserveLines(30);
		_hexPreview = new HexPreview<int>(HexPreviewChar, new RectInt(0, 3, 60, 30));
		_hexPreview.GetTileColor = c => c % 2 == 1 ? ConsoleColor.DarkGray : ConsoleColor.White;
		_hexPreview.ReverseY = true;
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
		var HexMap = new HexMap<int>(0);
		if (_hexPreview != null)
			_hexPreview.HexMap = HexMap;

		HexMap[0, 0] = 2;

		if (_hexPreview != null)
		{
			_hexPreview.Offset = new Point(-15, -15);
			_hexPreview.Update();
		}
		var flipOnce = 0;
		var flipBack = 0;

		foreach (var line in input.Split("\n"))
		{
			Console.WriteLine($"{line}");
			var tile = HexMap.GetHex(0, 0);

			tile = MoveTile(line, tile);

			var before = tile.Value;
			if (before == 0) flipOnce++; else flipBack++;
			tile.Value += (tile.Value % 2 == 0) ? 1 : -1;

			if (_hexPreview != null)
				_hexPreview.Update();
		}
		Console.WriteLine($"flip Once: {flipOnce}");
		Console.WriteLine($"flip Back: {flipBack}");

		foreach (var tile in HexMap.PointsAndValues().Where(x => (x.Value % 2) == 1))
		{
			Console.WriteLine($"{tile.Key} : {tile.Value}");
		}

		if (_hexPreview != null)
			_hexPreview.Update();

		return HexMap.PointsAndValues().Select(x => x.Value).Where(tile => tile % 2 == 1).Count();
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
		var HexMap = new HexMap<int>(0);
		if (_hexPreview != null)
			_hexPreview.HexMap = HexMap;

		HexMap[0, 0] = 2;

		if (_hexPreview != null)
		{
			_hexPreview.Offset = new Point(-15, -15);
			_hexPreview.Update();
		}

		foreach (var line in input.Split("\n"))
		{
			var tile = HexMap.GetHex(0, 0);
			tile = MoveTile(line, tile);
			var before = tile.Value;
			tile.Value += (tile.Value % 2 == 0) ? 1 : -1;
		}

		var stepOfData = new GameOfLife.StepOfLifeData<int>(true, TileActive, GetNeighbors, GetNextState);

		for (int i = 1; i <= 100; i++)
		{
			foreach (var changes in GameOfLife.DoStepOfLife(HexMap, stepOfData))
				HexMap[changes.Key[0], changes.Key[1]] = changes.Value;

			if (_hexPreview != null)
				Console.WriteLine($"Day {i.ToString("000")}: {HexMap.PointsAndValues().Where(x => TileActive(x.Value)).Count()}");

			if (_hexPreview != null)
				_hexPreview.Update();
		}

		return HexMap.PointsAndValues().Where(x => TileActive(x.Value)).Count();
	}

	private int FlipValue(int value) => value += (value % 2 == 0) ? 1 : -1;
	
	private bool TileActive(int value) => value % 2 == 1;

	private KeyValuePair<int[], int>[] GetNeighbors(MultiDimentionalArray<int> map, int[] key)
	{
		var hexMap = (HexMap<int>)map;
		return hexMap.GetHex(key[0], key[1]).GetNeighbors()
			.Select(hex => hex.GetKeyValuePair())
			.ToArray();
	}

	private int GetNextState(int value, int neighbors)
	{
		var active = TileActive(value);
		var shouldFlip = (active && !neighbors.Between(1, 2)) || (!active && neighbors == 2);
		return shouldFlip ? FlipValue(value) : value;
	}

}
