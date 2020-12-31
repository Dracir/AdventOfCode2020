using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day3 : DayBase
{
	char[,] _inputGrid;
	private GridPreview<char>[] _gridRenderer;
	private HeaderValue[] _treeHit;
	private HeaderValue _progress;
	private int _rowToDo;

	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
		CreateRenders(1, 30);
	}

	//-----------------------------------------------------------------

	public override void SetUpConsolePart2()
	{
		CreateRenders(5, 20);
	}

	private void CreateRenders(int amount, int gridPreviewWidth)
	{
		var headerSize = gridPreviewWidth + 5;
		Console.Header.ReserveLines(23);
		_progress = Console.Header.CreateBlockValue(amount * headerSize - 10, "Progress: ", ValueToUTFBars.Styles.Horizontal);
		Console.Header.ForceNewLine();

		_treeHit = new HeaderValue[amount];
		_gridRenderer = new GridPreview<char>[amount];

		for (int i = 0; i < amount; i++)
		{
			_treeHit[i] = Console.Header.CreateFormatedValue(13, "Tree Hits: ");

			_gridRenderer[i] = new GridPreview<char>((c) => c, new RectInt(i * headerSize, 6, gridPreviewWidth, 20));
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
		_gridRenderer = null;
		_treeHit = null;
		_progress = null;
	}

	//-----------------------------------------------------------------

	public override bool Equals(object obj)
	{
		return base.Equals(obj);
	}

	//-----------------------------------------------------------------

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}


	public static GrowingGrid<char> CreateGrowingGrid(char[,] grid) => new GrowingGrid<char>(' ', grid, GridAxes.XY, grid.GetLength(0), true, true);

	//-----------------------------------------------------------------

	public override long Part1(string input)
	{
		_inputGrid = InputParser.ParseCharGrid(input, '\n');
		_rowToDo = _inputGrid.GetLength(1);

		var grid = CreateGrowingGrid(_inputGrid);
		grid.OnGridGrown = OnGridGrow;

		Solver solver;
		if (_gridRenderer != null && _gridRenderer[0] != null && _treeHit != null)
		{
			_gridRenderer[0].Grid = grid;
			solver = new Solver(grid, _gridRenderer[0], _treeHit[0], new Point(3, 1));
		}
		else
			solver = new Solver(grid, null, null, new Point(3, 1));


		for (var y = 0; y < _rowToDo; y++)
		{
			solver.Step();

			if (_progress != null)
				_progress.SetValue(y * 1f / _rowToDo);
		}

		if (_progress != null)
			_progress.SetValue(1f);

		return solver.HitTrees;
	}
	//-----------------------------------------------------------------

	private void OnGridGrow(GrowingGrid<char>.GrowingGridEvent growingEvent)
	{
		var grid = growingEvent.Grid;
		if (growingEvent.Right != 0 && _inputGrid != null)
		{
			grid.AddGrid(grid.FullWidth - growingEvent.Right, 0, _inputGrid, GridAxes.XY);
		}
	}

	public override long Part2(string input)
	{
		_inputGrid = InputParser.ParseCharGrid(input, '\n');
		_rowToDo = _inputGrid.GetLength(1);

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
		while (y < _rowToDo)
		{
			foreach (var solver in solvers)
			{
				solver.Step();
			}
			y++;

			if (_progress != null)
				_progress.SetValue(y * 1f / _rowToDo);
		}

		if (_progress != null)
			_progress.SetValue(1f);

		var value = 1L;
		foreach (var solver in solvers)
			value *= (long)solver.HitTrees;
		return value;
	}

	//-----------------------------------------------------------------

	public class Solver
	{
		GrowingGrid<char> Grid;
		GridPreview<char> Renderer;
		HeaderValue TreeHitHeader;

		int x = 0;
		int y = 0;
		public int HitTrees = 0;
		public int OpenSquare = 0;
		public char LastTile = '.';
		private int _totalHeight;

		Point _direction;

		public Solver(GrowingGrid<char> grid, GridPreview<char> renderer, HeaderValue treeHitHeader, Point direction)
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

			if (Renderer != null)
				Renderer.Offset = new Point(x - Renderer.Viewport.Width / 2, y - Renderer.Viewport.Height / 2);

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
			{
				LastTile = 'O';
				OpenSquare++;
			}

			if (Renderer != null)
				Renderer.Update();
		}
	}
}