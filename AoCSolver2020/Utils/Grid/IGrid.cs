
using System.Collections.Generic;

public interface IGrid<T>
{
	int MinX { get; }
	int MinY { get; }
	int MaxX { get; }
	int MaxY { get; }
	int Width { get; }
	int Height { get; }

	Point TopLeft { get; }
	Point TopRight { get; }
	Point BottomLeft { get; }
	Point BottomRight { get; }
	Point Center { get; }

	T this[Point key] { get; set; }
	T this[int x, int y] { get; set; }


	IEnumerable<Point> Points();
	IEnumerable<Point> AreaSquareAround(Point pt, int radiusDistance);
	IEnumerable<Point> AreaAround(Point pt, int manhattanDistance);
	IEnumerable<int> ColumnIndexs();
	IEnumerable<int> RowIndexs();
	T[,] ToArray();
}