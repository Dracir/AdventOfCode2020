using System;

public class Hex<T>
{
	public T Value;
	private readonly HexMap<T> Map;


	public readonly Point Point;
	private int Q => Point.X;
	private int R => Point.Y;

	public Hex(int q, int r, HexMap<T> map)
	{
		Map = map;
		Point = new Point(q, r);
	}

	public Hex<T> East => Map[Q + 1, R];
	public Hex<T> SouthEast => Map[Q + ((Math.Abs(R) + 1) % 2), R - 1];
	public Hex<T> SouthWest => Map[Q - (Math.Abs(R) % 2), R - 1];
	public Hex<T> West => Map[Q - 1, R];
	public Hex<T> NorthEast => Map[Q + ((Math.Abs(R) + 1) % 2), R + 1];
	public Hex<T> NorthWest => Map[Q - (Math.Abs(R) % 2), R + 1];
}