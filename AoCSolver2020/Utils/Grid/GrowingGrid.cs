using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public class GrowingGrid<T> : IGrid<T>
{
	private T _defaultValue;

	public int UsedMinX => _grid.UsedMinX;
	public int UsedMinY => _grid.UsedMinY;
	public int UsedMaxX => _grid.UsedMaxX;
	public int UsedMaxY => _grid.UsedMaxY;
	public int MinX => _grid.MinX;
	public int MinY => _grid.MinY;
	public int MaxX => _grid.MaxX;
	public int MaxY => _grid.MaxY;
	public int FullWidth => _grid.FullWidth;
	public int FullHeight => _grid.FullHeight;
	public int UsedWidth => _grid.UsedWidth;
	public int UsedHeight => _grid.UsedHeight;

	private Grid<T> _grid;
	private int _growthIncrement;
	private bool _growsOnRead;
	private bool _growsOnWrite;

	public int GrowthTimesRight = 0;
	public int GrowthTimesLeft = 0;
	public int GrowthTimesUp = 0;
	public int GrowthTimesDown = 0;


	int OffsetX => _grid.OffsetX;
	int OffsetY => _grid.OffsetY;

	public Point TopLeft => _grid.TopLeft;
	public Point TopRight => _grid.TopRight;
	public Point BottomLeft => _grid.BottomLeft;
	public Point BottomRight => _grid.BottomRight;
	public Point Center => _grid.Center;

	public Action<GrowingGridEvent>? OnGridGrown;

	public GrowingGrid(T defaultValue, Point xRange, Point yRange, int growthIncrement, bool growsOnRead = true, bool growsOnWrite = true)
	{
		_grid = new Grid<T>(defaultValue, xRange, yRange);
		_defaultValue = defaultValue;
		_growthIncrement = growthIncrement;
		_growsOnRead = growsOnRead;
		_growsOnWrite = growsOnWrite;
	}

	public T this[Point key]
	{
		get { return this[key.X, key.Y]; }
		set { this[key.X, key.Y] = value; }
	}

	public T this[int x, int y]
	{
		get
		{
			var targetX = x + OffsetX;
			var targetY = y + OffsetY;
			if (_growsOnWrite)
				GrowIfNeeded(targetX, targetY);
			return _grid[targetX, targetY];
		}
		set
		{
			var targetX = x + OffsetX;
			var targetY = y + OffsetY;
			if (_growsOnWrite)
				GrowIfNeeded(targetX, targetY);
			_grid[targetX, targetY] = value;
		}
	}

	private void GrowIfNeeded(int targetX, int targetY)
	{
		var growthNeeded = false;
		int left = 0, right = 0, top = 0, bottom = 0;
		if (targetX < 0)
		{
			growthNeeded = true;
			GrowthTimesLeft++;
			left = _growthIncrement;
		}
		if (targetX >= FullWidth)
		{
			growthNeeded = true;
			GrowthTimesRight++;
			right = _growthIncrement;
		}
		if (targetY < 0)
		{
			growthNeeded = true;
			GrowthTimesDown++;
			bottom = _growthIncrement;
		}
		if (targetY >= FullHeight)
		{
			growthNeeded = true;
			GrowthTimesUp++;
			top = _growthIncrement;
		}
		if (growthNeeded)
			GrowGrid(top, right, bottom, left);
	}

	public void AddGrid(int leftX, int bottomY, T[,] grid) => _grid.AddGrid(leftX, bottomY, grid);

	private void GrowGrid(int up, int right, int down, int left)
	{
		var newGrid = new Grid<T>(_defaultValue, new Point(_grid.MinX - left, _grid.MaxX + right), new Point(_grid.MinY - down, _grid.MaxY + up));

		for (int x = _grid.MinX; x <= _grid.MaxX; x++)
			for (int y = _grid.MinY; y <= _grid.MaxY; y++)
				newGrid[x + left, y + down] = _grid[x, y];

		_grid = newGrid;
		OnGridGrown?.Invoke(new GrowingGridEvent(this, up, right, down, left));
	}

	public IEnumerable<Point> Points() => _grid.Points();
	public IEnumerable<Point> AreaSquareAround(Point pt, int radiusDistance) => _grid.AreaSquareAround(pt, radiusDistance);
	public IEnumerable<Point> AreaAround(Point pt, int manhattanDistance) => _grid.AreaAround(pt, manhattanDistance);
	public IEnumerable<int> ColumnIndexs() => _grid.ColumnIndexs();
	public IEnumerable<int> RowIndexs() => _grid.RowIndexs();
	public T[,] ToArray() => _grid.ToArray();

	public struct GrowingGridEvent
	{
		public GrowingGrid<T> Grid;
		public int Up;
		public int Right;
		public int Down;
		public int Left;

		public GrowingGridEvent(GrowingGrid<T> growingGrid, int up, int right, int down, int left)
		{
			Grid = growingGrid;
			Up = up;
			Right = right;
			Down = down;
			Left = left;
		}
	}
}

