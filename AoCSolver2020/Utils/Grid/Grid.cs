using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public class Grid<T> : IGrid<T>
{
	private T[,] Values;

	private int _offsetX;
	private int _offsetY;
	private int usedMinX;
	private int usedMinY;
	private int usedMaxX;
	private int usedMaxY;


	public int UsedMinX => usedMinX;
	public int UsedMinY => usedMinY;
	public int UsedMaxX => usedMaxX;
	public int UsedMaxY => usedMaxY;

	public int MinX => OffsetX;
	public int MinY => OffsetY;
	public int MaxX => OffsetX + FullWidth - 1;
	public int MaxY => OffsetY + FullHeight - 1;

	public int FullWidth => Values.GetLength(0);
	public int FullHeight => Values.GetLength(1);
	public int UsedWidth => usedMaxX - usedMinX + 1;
	public int UsedHeight => usedMaxY - usedMinY + 1;

	public int OffsetX => _offsetX;
	public int OffsetY => _offsetY;

	protected T DefaultValue;

	public Grid(T defaultValue, Point xRange, Point yRange)
	{
		this.DefaultValue = defaultValue;
		_offsetX = -xRange.X;
		_offsetY = -yRange.X;
		usedMinX = _offsetX;
		usedMaxX = _offsetX;
		usedMinY = _offsetY;
		usedMaxY = _offsetY;
		Values = new T[xRange.Y - xRange.X + 1, yRange.Y - yRange.X + 1];
	}


	public virtual T this[Point key]
	{
		get { return this[key.X, key.Y]; }
		set { this[key.X, key.Y] = value; }
	}

	public virtual T this[int x, int y]
	{
		get { return Values[x + _offsetX, y + _offsetY]; }
		set
		{
			Values[x + _offsetX, y + _offsetY] = value;
			usedMinX = Math.Min(usedMinX, x);
			usedMinY = Math.Min(usedMinY, y);
			usedMaxX = Math.Max(usedMaxX, x);
			usedMaxY = Math.Max(usedMaxY, y);
		}
	}


	public Point TopLeft => new Point(usedMinX, usedMaxY);
	public Point TopRight => new Point(usedMaxX, usedMaxY);
	public Point BottomLeft => new Point(usedMinX, usedMinY);
	public Point BottomRight => new Point(usedMaxX, usedMinY);
	public Point Center => new Point(_offsetX, _offsetY);

	public IEnumerable<Point> Points()
	{
		for (int x = usedMinX; x <= usedMaxX; x++)
			for (int y = usedMinY; y <= usedMaxY; y++)
				yield return new Point(x, y);
	}


	public IEnumerable<Point> AreaSquareAround(Point pt, int radiusDistance)
	{

		int x1 = Math.Max(usedMinX, pt.X - radiusDistance);
		int y1 = Math.Max(usedMinY, pt.Y - radiusDistance);
		int x2 = Math.Max(usedMaxX, pt.X + radiusDistance);
		int y2 = Math.Max(usedMaxY, pt.Y + radiusDistance);

		for (int x = x1; x <= x2; x++)
			for (int y = y1; y <= y2; y++)
				yield return new Point(x, y);
	}
	public IEnumerable<Point> AreaAround(Point pt, int manhattanDistance)
	{

		int x1 = Math.Max(usedMinX, pt.X - manhattanDistance);
		int y1 = Math.Max(usedMinY, pt.Y - manhattanDistance);
		int x2 = Math.Max(usedMaxX, pt.X + manhattanDistance);
		int y2 = Math.Max(usedMaxY, pt.Y + manhattanDistance);

		for (int x = x1; x <= x2; x++)
			for (int y = y1; y <= y2; y++)
				if (x + y <= manhattanDistance)
					yield return new Point(x, y);
	}

	public IEnumerable<int> ColumnIndexs()
	{
		for (int y = usedMinY; y <= usedMaxY; y++)
			yield return y;
	}

	public IEnumerable<int> RowIndexs()
	{
		for (int x = usedMinX; x <= usedMaxX; x++)
			yield return x;
	}

	public T[,] ToArray()
	{
		var arr = new T[UsedWidth, UsedHeight];
		for (int x = usedMinX; x <= usedMaxX; x++)
			for (int y = usedMinY; y <= usedMaxY; y++)
				arr[x - usedMinX, y - usedMinY] = Values[x, y];
		return arr;
	}

	public void AddGrid(int leftX, int bottomY, T[,] grid)
	{
		for (int x = 0; x < grid.GetLength(0); x++)
		{
			for (int y = 0; y < grid.GetLength(1); y++)
			{
				this[x + leftX, y + bottomY] = grid[x, y];
			}
		}
	}

}
