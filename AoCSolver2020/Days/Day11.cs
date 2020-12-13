using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Console = ConsoleManager;


public class Day11 : DayBase
{
	GridPreview<TileType>? _gridPreview;
	GridPreview<int>? _occupiedPreview;

	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
		Console.Header.ReserveLines(40);
		_gridPreview = new GridPreview<TileType>(GetDisplayChar, new RectInt(0, 3, 60, 40));
		_occupiedPreview = new GridPreview<int>((x => x == 9 ? '.' : x.ToString()[0]), new RectInt(35, 3, 30, 30));
	}

	//-----------------------------------------------------------------

	public override void SetUpConsolePart2()
	{
		Console.Header.ReserveLines(91);
		_gridPreview = new GridPreview<TileType>(GetDisplayChar, new RectInt(0, 3, 91, 91));
		_gridPreview.GetTileColor = (tileType) => tileType switch
		{
			TileType.Occupied => ConsoleColor.Green,
			TileType.Empty => ConsoleColor.Gray,
			_ => ConsoleColor.DarkGray,
		};
		//_occupiedPreview = new GridPreview<int>((x => x == 9 ? '.' : x.ToString()[0]), new RectInt(35, 3, 30, 30));
	}

	//-----------------------------------------------------------------

	public override void CleanUp()
	{
	}

	//-----------------------------------------------------------------

	public override bool Equals(object? obj) => base.Equals(obj);
	public override int GetHashCode() => base.GetHashCode();

	//-----------------------------------------------------------------

	public override long Part1(string input)
	{
		var area = PartArea(input);
		if (_gridPreview != null)
		{
			_gridPreview.Grid = area;
			_gridPreview.Update();
		}


		while (Step(area, GetImmediatelyAdjacentSeats, 4))
		{
			Console.WriteLine("Step");
			_gridPreview?.Update();
		}

		return area.PointsAndValues().Count(x => x.Value == TileType.Occupied);
	}

	private TileType[] GetImmediatelyAdjacentSeats(Grid<TileType> area, Point pt)
	{
		return area.PointAndValuesSquareAround(pt, 1)
		.Where(x => (x.Point.X != pt.X || x.Point.Y != pt.Y))
		.Select(x => x.Value)
		.ToArray();
	}

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		var area = PartArea(input);
		if (_gridPreview != null)
		{
			_gridPreview.Grid = area;
			_gridPreview.Update();
		}
		if (_occupiedPreview != null)
		{
			_occupiedPreview.Grid = new Grid<int>(0, new Point(0, area.UsedMaxX), new Point(0, area.UsedMaxY));
			_occupiedPreview.Update();
		}



		///return 0;

		while (Step(area, GetDirectionsAdjacentSeats, 5))
		{
			_occupiedPreview?.Update();
			//System.Console.ReadLine();
			//Console.WriteLine("Step");
			//Console.WriteLine(string.Join(",", GetDirectionsAdjacentSeats(area, new Point(4, 1))));
			_gridPreview?.Update();
		}

		return area.PointsAndValues().Count(x => x.Value == TileType.Occupied);
	}

	private TileType[] GetDirectionsAdjacentSeats(Grid<TileType> area, Point pt)
	{
		var seats = new List<TileType>();

		foreach (var dir in PointHelper._8DirectionPoints)
		{
			foreach (var item in area.PointAndValuesInDirection(pt, dir.X, dir.Y, false))
			{
				if (item.Value != TileType.Floor)
				{
					seats.Add(item.Value);
					break;
				}
			}
		}

		return seats.ToArray();
	}


	// -------------------------------------------
	private bool Step(Grid<TileType> area, Func<Grid<TileType>, Point, TileType[]> getAdjacent, int seatTolerence)
	{
		var update = new TileType[area.FullWidth, area.FullHeight];
		var change = false;
		for (int x = 0; x < area.FullWidth; x++)
		{
			for (int y = 0; y < area.FullHeight; y++)
			{
				var seat = area[x, y];
				update[x, y] = seat;

				var pts = getAdjacent(area, new Point(x, y));
				var nbOccupied = pts.Count(pt => pt == TileType.Occupied);

				if (seat == TileType.Empty)
				{
					if (nbOccupied == 0)
					{
						update[x, y] = TileType.Occupied;
						change = true;
					}
				}
				else if (seat == TileType.Occupied)
				{
					if (nbOccupied >= seatTolerence)
					{
						update[x, y] = TileType.Empty;
						change = true;
					}
				}
				if (_occupiedPreview != null && _occupiedPreview.Grid != null)
					_occupiedPreview.Grid[x, y] = (seat == TileType.Floor) ? 9 : nbOccupied;
			}
		}

		area.AddGrid(0, 0, update, GridAxes.XY);
		return change;
	}


	private Grid<TileType> PartArea(string input)
	{
		var rows = InputParser.ListOfStrings(input);
		var grid = new Grid<TileType>(TileType.Empty, new Point(0, rows[0].Length - 1), new Point(0, rows.Length - 1));
		for (int y = 0; y < rows.Length; y++)
		{
			for (int x = 0; x < rows[y].Length; x++)
			{
				grid[x, y] = ParseSeat(rows[y][x]);
			}
		}
		return grid;
	}

	private TileType ParseSeat(char c) => c switch
	{
		'L' => TileType.Empty,
		'#' => TileType.Occupied,
		'.' => TileType.Floor,
		_ => TileType.Floor,
	};


	private Char GetDisplayChar(TileType tiletype) => tiletype switch
	{
		TileType.Empty => 'L',
		TileType.Occupied => '#',
		TileType.Floor => '.',
		_ => '.',
	};

	enum TileType { Floor, Empty, Occupied };

}
