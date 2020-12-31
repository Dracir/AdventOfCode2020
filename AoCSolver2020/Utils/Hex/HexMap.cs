
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;

public class HexMap<T>
{

	public Dictionary<Point, Hex<T>> _map = new Dictionary<Point, Hex<T>>();

	public int UsedWidth => UsedMaxX - UsedMinX + 1;
	public int UsedHeight => UsedMaxY - UsedMinY + 1;

	public int UsedMinX;
	public int UsedMinY;
	public int UsedMaxX;
	public int UsedMaxY;

	public Hex<T> this[int col, int row] => this[new Point(col, row)];
	public Hex<T> this[Point point]
	{
		get
		{
			if (_map.TryGetValue(point, out var hex))
				return hex;
			else
			{
				var newHex = new Hex<T>(point.X, point.Y, this);
				_map.Add(point, newHex);
				UsedMinX = Math.Min(point.X, UsedMinX);
				UsedMinY = Math.Min(point.Y, UsedMinY);
				UsedMaxX = Math.Max(point.X, UsedMaxX);
				UsedMaxY = Math.Max(point.Y, UsedMaxY);
				return newHex;
			}
		}
	}

	public HexMap()
	{

	}

	public bool XInBound(int x) => x.Between(UsedMinX, UsedMaxX);
	public bool YInBound(int y) => y.Between(UsedMinY, UsedMaxY);

}