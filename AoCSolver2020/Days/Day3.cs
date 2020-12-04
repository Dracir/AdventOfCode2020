using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day3 : DayBase
{
	char[,]? _inputGrid;
	private GridPreview<char>? _gridRenderer;
	private HeaderValue? _treeHit;
	private HeaderValue? _openSquares;

	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
		Console.Header.ReserveLines(21);

		_treeHit = Console.Header.CreateFormatedValue(5, "Tree Hits: ");
		_openSquares = Console.Header.CreateFormatedValue(5, "Open Squares: ");

		_gridRenderer = new GridPreview<char>((c) => c, new RectInt(0, 4, 20, 20));
		_gridRenderer.GetTileColor = (c) => c switch
		{
			'.' => ConsoleColor.Gray,
			'#' => ConsoleColor.DarkGreen,
			'@' => ConsoleColor.Yellow,
			'X' => ConsoleColor.Red,
			'O' => ConsoleColor.Cyan,
			_ => ConsoleColor.Gray
		};
	}

	//-----------------------------------------------------------------

	public override void SetUpConsolePart2()
	{
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


	public static GrowingGrid<char> CreateGrowingGrid(char[,] grid)
	{
		var xRange = new Point(0, grid.GetLength(0) - 1);
		var yRange = new Point(0, grid.GetLength(1) - 1);
		var growingGrid = new GrowingGrid<char>(' ', xRange, yRange, grid.GetLength(0) - 1, true, true);
		growingGrid.AddGrid(0, 0, grid);
		return growingGrid;
	}

	//-----------------------------------------------------------------

	public override long Part1(string input)
	{
		_inputGrid = InputParser.ParseCharGrid(input, '\n');
		var grid = CreateGrowingGrid(_inputGrid);
		grid.OnGridGrown = OnGridGrow;
		if (_gridRenderer != null)
			_gridRenderer.Grid = grid;

		var hitTrees = 0;
		var openSquares = 0;
		var x = 0;
		var y = 0;
		var lastTile = '.';

		while (y < _inputGrid.GetLength(1))
		{
			grid[x, y] = lastTile;
			x += 3;
			y += 1;


			if (_gridRenderer != null && y > 10)
				_gridRenderer.Offset = new Point(x - 10, y - 10);

			var gridValue = grid[x, y];
			grid[x, y] = '@';

			if (gridValue == '#')
			{
				lastTile = 'X';
				hitTrees++;
				_treeHit?.SetValue(hitTrees);
			}
			else
			{
				lastTile = 'O';
				openSquares++;
				_openSquares?.SetValue(openSquares);
			}

			_gridRenderer?.Update();

		}
		return hitTrees;
	}

	private void OnGridGrow(GrowingGrid<char>.GrowingGridEvent growingEvent)
	{
		var grid = growingEvent.Grid;
		if (growingEvent.Right != 0 && _inputGrid != null)
		{
			grid.AddGrid(grid.MaxX - growingEvent.Right, 0, _inputGrid);
		}
	}

	public override long Part2(string input)
	{
		return 0;
	}

	//-----------------------------------------------------------------

}