using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day5 : DayBase
{

	private HeaderValue? _progress;
	private Point PlaneOffset = new Point(3, 4);

	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
		Console.Header.ReserveLines(11);
		_progress = Console.Header.CreateBlockValue(40, "Progress: ", ValueToUTFBars.Styles.Circle);
	}

	//-----------------------------------------------------------------

	public override void SetUpConsolePart2()
	{
		Console.Header.ReserveLines(11);
		_progress = Console.Header.CreateBlockValue(40, "Progress: ", ValueToUTFBars.Styles.Circle);
	}

	//-----------------------------------------------------------------

	public override void CleanUp()
	{
	}

	//-----------------------------------------------------------------

	public override bool Equals(object? obj)
	{
		return base.Equals(obj);
	}

	//-----------------------------------------------------------------

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	//-----------------------------------------------------------------
	public override long Part1(string input)
	{
		DrawPlane();
		var plane = GetPlane(input);
		return plane.Seats.Max(x => GetSeatId(x));
	}

	private Plane GetPlane(string input)
	{
		var seats = new List<Point>();
		var plane = new Plane(seats);

		var index = 0;
		var tickets = input.Split("\n");
		foreach (var item in tickets)
		{
			if (_progress != null)
				_progress.SetValue(++index / (float)tickets.Count());
			var seat = ParseSeat(item);
			plane.TakenSeats[seat.X, seat.Y] = true;
			var rowFirst = plane.FirstSeat[seat.Y];
			var rowLast = plane.LastSeat[seat.Y];

			var displayY = seat.Y;
			if (seat.Y >= 4)
				displayY += 2;

			var wasFirstOrLast = false;
			if (rowFirst > seat.X)
			{
				wasFirstOrLast = true;
				if (rowFirst <= 128 && rowLast != rowFirst)
				{
					SetColorForCol(seat.Y);
					BetterConsole.WriteAt('X', PlaneOffset.X + rowFirst, PlaneOffset.Y + displayY);
				}
				plane.FirstSeat[seat.Y] = seat.X;
				System.Console.ForegroundColor = ConsoleColor.Red;
				BetterConsole.WriteAt('X', PlaneOffset.X + seat.X, PlaneOffset.Y + displayY);
			}

			if (rowLast < seat.X)
			{
				wasFirstOrLast = true;
				if (rowLast >= 0 && rowLast != rowFirst)
				{
					SetColorForCol(seat.Y);
					BetterConsole.WriteAt('X', PlaneOffset.X + rowLast, PlaneOffset.Y + displayY);
				}
				plane.LastSeat[seat.Y] = seat.X;

				System.Console.ForegroundColor = ConsoleColor.Red;
				BetterConsole.WriteAt('X', PlaneOffset.X + seat.X, PlaneOffset.Y + displayY);
			}

			if (!wasFirstOrLast)
			{
				SetColorForCol(seat.Y);
				BetterConsole.WriteAt('X', PlaneOffset.X + seat.X, PlaneOffset.Y + displayY);

			}

			seats.Add(seat);
		}

		return plane;
	}

	private void SetColorForCol(int col)
	{
		if (col >= 4)
			System.Console.ForegroundColor = ConsoleColor.Green;
		else
			System.Console.ForegroundColor = ConsoleColor.Blue;
	}

	private void DrawPlane()
	{
		System.Console.ForegroundColor = ConsoleColor.Gray;
		string row = new String('O', 128);
		for (var y = 0; y < 4; y++)
			BetterConsole.WriteAt(row, PlaneOffset.X, PlaneOffset.Y + y);

		for (var y = 4; y < 8; y++)
			BetterConsole.WriteAt(row, PlaneOffset.X, PlaneOffset.Y + y + 2);
	}

	private int GetSeatId(Point seat) => seat.X * 8 + seat.Y;

	private Point ParseSeat(string x)
	{
		var binary = x.Select(x => ParseChar(x));
		var binStr = binary.Select(x => x ? '1' : '0').ToList();
		binStr.Insert(7, '-');

		var row = FromBinary(binary.SkipLast(3));
		var col = FromBinary(binary.Skip(7));
		var seat = new Point(row, col);

		if (col >= 4)
			System.Console.ForegroundColor = ConsoleColor.Green;
		else
			System.Console.ForegroundColor = ConsoleColor.Blue;
		Console.WriteLine($"Seat {x} → {new string(binStr.ToArray())} → row {row.ToString("000")}, col {col.ToString("00")} → ID {GetSeatId(seat)}");

		return seat;
	}

	private int FromBinary(IEnumerable<bool> bools)
	{
		int value = 0;
		int index = 0;
		foreach (var bit in bools.Reverse())
		{
			if (bit)
				value += (int)Math.Pow(2, index);
			index++;
		}
		return value;
	}

	private bool ParseChar(char c) => c switch
	{
		'B' => true,
		'R' => true,
		_ => false
	};

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		DrawPlane();
		var plane = GetPlane(input);


		for (var y = 0; y < 8; y++)
		{
			System.Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine($"Searching col {y}                                                           ");
			for (var x = plane.FirstSeat[y] + 1; x < plane.LastSeat[y]; x++)
			{
				if (!plane.TakenSeats[x, y])
				{
					System.Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine($"Found the empty seat at row {x} , col {y}                                           ");
					BetterConsole.WriteAt('X', PlaneOffset.X + x, PlaneOffset.Y + (y >= 4 ? y + 2 : y));

					return x * 8 + y;
				}
			}
		}

		return 0L;
	}

	struct Plane
	{
		public List<Point> Seats;
		public bool[,] TakenSeats;
		public int[] FirstSeat;
		public int[] LastSeat;

		public Plane(List<Point> seats)
		{
			Seats = seats;
			TakenSeats = new bool[128, 8];
			FirstSeat = new int[] { 129, 129, 129, 129, 129, 129, 129, 129 };
			LastSeat = new int[] { -1, -1, -1, -1, -1, -1, -1, -1 };
		}
	}

}
