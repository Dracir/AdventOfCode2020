using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public class Grid<T> : IGrid<T>
{
	public T[,] Values;

	private int _offsetX;
	private int _offsetY;
	private int minX;
	private int minY;
	private int maxX;
	private int maxY;

	public int MinX => minX;
	public int MinY => minY;
	public int MaxX => maxX;
	public int MaxY => maxY;
	public int Width => maxX - minX + 1;
	public int Height => maxY - minY + 1;
	public int OffsetX => _offsetX;
	public int OffsetY => _offsetY;

	protected T DefaultValue;

	public Grid(T defaultValue, Point xRange, Point yRange)
	{
		this.DefaultValue = defaultValue;
		_offsetX = -xRange.X;
		_offsetY = -yRange.X;
		minX = _offsetX;
		maxX = _offsetX;
		minY = _offsetY;
		maxY = _offsetY;
		Values = new T[xRange.Y - xRange.X, yRange.Y - yRange.X];
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
			minX = Math.Min(minX, x);
			minY = Math.Min(minY, y);
			maxX = Math.Max(maxX, x);
			maxY = Math.Max(maxY, y);
		}
	}


	public Point TopLeft => new Point(minX, maxY);
	public Point TopRight => new Point(maxX, maxY);
	public Point BottomLeft => new Point(minX, minY);
	public Point BottomRight => new Point(maxX, minY);
	public Point Center => new Point(_offsetX, _offsetY);

	public IEnumerable<Point> Points()
	{
		for (int x = minX; x <= maxX; x++)
			for (int y = minY; y <= maxY; y++)
				yield return new Point(x, y);
	}


	public IEnumerable<Point> AreaSquareAround(Point pt, int radiusDistance)
	{

		int x1 = Math.Max(minX, pt.X - radiusDistance);
		int y1 = Math.Max(minY, pt.Y - radiusDistance);
		int x2 = Math.Max(maxX, pt.X + radiusDistance);
		int y2 = Math.Max(maxY, pt.Y + radiusDistance);

		for (int x = x1; x <= x2; x++)
			for (int y = y1; y <= y2; y++)
				yield return new Point(x, y);
	}
	public IEnumerable<Point> AreaAround(Point pt, int manhattanDistance)
	{

		int x1 = Math.Max(minX, pt.X - manhattanDistance);
		int y1 = Math.Max(minY, pt.Y - manhattanDistance);
		int x2 = Math.Max(maxX, pt.X + manhattanDistance);
		int y2 = Math.Max(maxY, pt.Y + manhattanDistance);

		for (int x = x1; x <= x2; x++)
			for (int y = y1; y <= y2; y++)
				if (x + y <= manhattanDistance)
					yield return new Point(x, y);
	}

	public IEnumerable<int> ColumnIndexs()
	{
		for (int y = minY; y <= maxY; y++)
			yield return y;
	}

	public IEnumerable<int> RowIndexs()
	{
		for (int x = minX; x <= maxX; x++)
			yield return x;
	}

	public T[,] ToArray()
	{
		var arr = new T[Width, Height];
		for (int x = minX; x <= maxX; x++)
			for (int y = minY; y <= maxY; y++)
				arr[x - minX, y - minY] = Values[x, y];
		return arr;
	}

}
