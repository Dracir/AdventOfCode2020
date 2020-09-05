using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public class Point
{
	public int X;
	public int Y;

	public Point Up { get { return new Point(X, Y + 1); } }
	public Point Down { get { return new Point(X, Y - 1); } }
	public Point Left { get { return new Point(X - 1, Y); } }
	public Point Right { get { return new Point(X + 1, Y); } }

	public Point(int x, int y)
	{
		this.X = x;
		this.Y = y;
	}

	public int DistanceManhattan(Point p2)
	{
		return Math.Abs(X - p2.X + Y - p2.Y);
	}


	public override bool Equals(object obj)
	{
		if (obj == null || GetType() != obj.GetType())
		{
			return false;
		}

		var other = (Point)obj;
		return other.X == X && other.Y == Y;
	}

	// override object.GetHashCode
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
}