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
	public int MinX => _grid.MaxX;
	public int MinY => _grid.MinY;
	public int MaxX => _grid.MaxX;
	public int MaxY => _grid.MaxY;
	public int Width => _grid.Width;
	public int Height => _grid.Height;
	public T[,] Values => _grid.Values;


	private Grid<T> _grid;
	private int _growthIncrement;
	private bool _growsOnRead;
	private bool _growsOnWrite;


	int OffsetX => _grid.OffsetX;
	int OffsetY => _grid.OffsetY;

	public Point TopLeft => _grid.TopLeft;
	public Point TopRight => _grid.TopRight;
	public Point BottomLeft => _grid.BottomLeft;
	public Point BottomRight => _grid.BottomRight;
	public Point Center => _grid.Center;

	public GrowingGrid(T defaultValue, Point xRange, Point yRange, int growthIncrement, bool growsOnRead, bool growsOnWrite)
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
			return Values[targetX, targetY];
		}
		set
		{
			var targetX = x + OffsetX;
			var targetY = y + OffsetY;
			if (_growsOnWrite)
				GrowIfNeeded(targetX, targetY);
			Values[targetX, targetY] = value;
		}
	}

	private void GrowIfNeeded(int targetX, int targetY)
	{
		if (targetX < 0)
			if (targetY < 0)
				GrowGrid(0, 0, _growthIncrement, _growthIncrement);
			else if (targetY > Height)
				GrowGrid(_growthIncrement, 0, 0, _growthIncrement);
			else
				GrowGrid(0, 0, 0, _growthIncrement);
		else if (targetX > 0)
			if (targetY < 0)
				GrowGrid(0, _growthIncrement, _growthIncrement, 0);
			else if (targetY > Height)
				GrowGrid(_growthIncrement, _growthIncrement, 0, 0);
			else
				GrowGrid(0, _growthIncrement, 0, 0);
		else if (targetY < 0)
			GrowGrid(0, 0, _growthIncrement, 0);
		else if (targetY > Height)
			GrowGrid(_growthIncrement, 0, 0, 0);
	}

	private void GrowGrid(int up, int right, int down, int left)
	{
		var newGrid = new Grid<T>(_defaultValue, new Point(_grid.MinX - left, _grid.MaxX + right), new Point(_grid.MinY - down, _grid.MaxY + up));

		for (int x = _grid.MinX; x <= _grid.MaxX; x++)
			for (int y = _grid.MinY; y <= _grid.MaxY; y++)
				newGrid[x + left, y + down] = _grid[x, y];

		_grid = newGrid;
	}

	public IEnumerable<Point> Points() => _grid.Points();
	public IEnumerable<Point> AreaSquareAround(Point pt, int radiusDistance) => _grid.AreaSquareAround(pt, radiusDistance);
	public IEnumerable<Point> AreaAround(Point pt, int manhattanDistance) => _grid.AreaAround(pt, manhattanDistance);
	public IEnumerable<int> ColumnIndexs() => _grid.ColumnIndexs();
	public IEnumerable<int> RowIndexs() => _grid.RowIndexs();
	public T[,] ToArray() => _grid.ToArray();
}

