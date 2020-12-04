using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day3 : DayBase
{
	char[,]? _inputGrid;
	private GridPreview<char>[]? _gridRenderer;
	private HeaderValue[]? _treeHit;
	private HeaderValue[]? _openSquares;

	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
		CreateRenders(1);
	}

	//-----------------------------------------------------------------

	public override void SetUpConsolePart2()
	{
		CreateRenders(5);
	}

	private void CreateRenders(int amount)
	{
		Console.Header.ReserveLines(21);

		_treeHit = new HeaderValue[amount];
		//_openSquares = new HeaderValue[amount];
		_gridRenderer = new GridPreview<char>[amount];

		for (int i = 0; i < amount; i++)
		{
			_treeHit[i] = Console.Header.CreateFormatedValue(13, "Tree Hits: ");
			//_openSquares[i] = Console.Header.CreateFormatedValue(5, "Open Squares: ");

			_gridRenderer[i] = new GridPreview<char>((c) => c, new RectInt(i * 25, 4, 20, 20));
			_gridRenderer[i].GetTileColor = (c) => c switch
			{
				'.' => ConsoleColor.Gray,
				'#' => ConsoleColor.DarkGreen,
				'@' => ConsoleColor.Yellow,
				'X' => ConsoleColor.Red,
				'O' => ConsoleColor.Cyan,
				_ => ConsoleColor.Gray
			};
		}
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
			_gridRenderer[0].Grid = grid;

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
				_gridRenderer[0].Offset = new Point(x - 10, y - 10);

			var gridValue = grid[x, y];
			grid[x, y] = '@';

			if (gridValue == '#')
			{
				lastTile = 'X';
				hitTrees++;
				if (_treeHit != null)
					_treeHit[0].SetValue(hitTrees);
			}
			else
			{
				lastTile = 'O';
				openSquares++;
				if (_openSquares != null)
					_openSquares[0].SetValue(openSquares);
			}

			if (_gridRenderer != null)
				_gridRenderer[0].Update();

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
		_inputGrid = InputParser.ParseCharGrid(input, '\n');

		var directions = new Point[] {
			new Point(1,1), new Point(3,1), new Point(5,1), new Point(7,1), new Point(1,2)
		};
		var solvers = new List<Solver>();

		for (int i = 0; i < directions.Length; i++)
		{
			var grid = CreateGrowingGrid(_inputGrid);
			grid.OnGridGrown = OnGridGrow;

			if (_gridRenderer != null)
				_gridRenderer[i].Grid = grid;
			if (_gridRenderer != null && _treeHit != null)
				solvers.Add(new Solver(grid, _gridRenderer[i], _treeHit[i], directions[i]));
			else
				solvers.Add(new Solver(grid, null, null, directions[i]));
		}



		var y = 0;
		while (y < _inputGrid.GetLength(1))
		{
			foreach (var solver in solvers)
			{
				solver.Step();
			}

		}
		return 0;
	}

	//-----------------------------------------------------------------

	public class Solver
	{
		GrowingGrid<char> Grid;
		GridPreview<char>? Renderer;
		HeaderValue? TreeHitHeader;

		int x = 0;
		int y = 0;
		public int HitTrees = 0;
		public char LastTile = '.';
		private int _totalHeight;

		Point _direction;

		public Solver(GrowingGrid<char> grid, GridPreview<char>? renderer, HeaderValue? treeHitHeader, Point direction)
		{
			_totalHeight = grid.FullHeight;
			Grid = grid;
			Renderer = renderer;
			TreeHitHeader = treeHitHeader;
			_direction = direction;
		}

		public void Step()
		{
			if (y >= _totalHeight) return;

			Grid[x, y] = LastTile;
			x += _direction.X;
			y += _direction.Y;

			if (Renderer != null && y > 10)
				Renderer.Offset = new Point(x - 10, y - 10);

			var gridValue = Grid[x, y];
			Grid[x, y] = '@';

			if (gridValue == '#')
			{
				LastTile = 'X';
				HitTrees++;
				if (TreeHitHeader != null)
					TreeHitHeader.SetValue(HitTrees);
			}
			else
				LastTile = 'O';
			if (Renderer != null)
				Renderer.Update();
		}
	}
}