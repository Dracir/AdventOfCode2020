using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day12 : DayBase
{
	GridPreview<char>? _gridPreview;

	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
		Console.Header.ReserveLines(0);
		/*Console.Header.ReserveLines(30);
		_gridPreview = new GridPreview<char>(x => x, new RectInt(0, 3, 30, 30));
		_gridPreview.GetTileColor = c => c switch
		{
			'@' => ConsoleColor.Yellow,
			'~' => ConsoleColor.Blue,
			_ => ConsoleColor.Gray
		};*/
	}

	//-----------------------------------------------------------------

	public override void SetUpConsolePart2()
	{
		Console.Header.ReserveLines(0);
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
		var worldmap = new GrowingGrid<char>('~', new Point(-20, 20), new Point(-20, 20), 20, true, true);
		if (_gridPreview != null)
			_gridPreview.Grid = worldmap;
		worldmap[0, 0] = '@';
		_gridPreview?.Update();

		var instructions = ParseInput(input);
		int shipX = 0;
		int shipY = 0;
		var direction = Direction.Est;

		foreach (var instruction in instructions)
		{
			Console.WriteLine(instruction.Direction + " " + instruction.Amount);
			worldmap[shipX, shipY] = '!';
			switch (instruction.Direction)
			{
				case 'N':
					shipY -= instruction.Amount;
					break;
				case 'S':
					shipY += instruction.Amount;
					break;
				case 'W':
					shipX -= instruction.Amount;
					break;
				case 'E':
					shipX += instruction.Amount;
					break;
				case 'L':
					for (int i = 0; i < instruction.Amount / 90; i++)
						direction = direction.Left();
					break;
				case 'R':
					for (int i = 0; i < instruction.Amount / 90; i++)
						direction = direction.Right();
					break;
				case 'F':
					var movement = direction.ToPoint() * instruction.Amount;
					shipX += movement.X;
					shipY += movement.Y;
					break;
			}
			worldmap[shipX, shipY] = '@';
			Console.WriteLine("-> Ship : " + new Point(shipX, shipY) + " - " + direction);
			if (_gridPreview != null)
				_gridPreview.Offset = new Point(shipX - _gridPreview.Viewport.Width / 2, shipY - _gridPreview.Viewport.Height / 2);
			//_gridPreview?.Update();
		}
		return new Point(shipX, shipY).DistanceManhattan(Point.ZERO);
	}

	private List<(char Direction, int Amount)> ParseInput(string input)
	{
		var instructions = new List<(char, int)>();

		foreach (var line in input.Replace("\r", "").Split("\n"))
		{
			var value = int.Parse(line.Substring(1));
			instructions.Add((line[0], value));
		}

		return instructions;
	}

	//-----------------------------------------------------------------
	//78648 Too high
	public override long Part2(string input)
	{
		var worldmap = new GrowingGrid<char>('~', new Point(-20, 20), new Point(-20, 20), 20, true, true);
		if (_gridPreview != null)
			_gridPreview.Grid = worldmap;
		worldmap[0, 0] = '@';
		_gridPreview?.Update();

		var instructions = ParseInput(input);
		int shipX = 0;
		int shipY = 0;
		var waypoint = new Point(10, -1);

		foreach (var instruction in instructions)
		{
			Console.WriteLine(instruction.Direction + " " + instruction.Amount);
			if (_gridPreview != null)
				worldmap[shipX, shipY] = '!';
			switch (instruction.Direction)
			{
				case 'N':
					waypoint.Y -= instruction.Amount;
					break;
				case 'S':
					waypoint.Y += instruction.Amount;
					break;
				case 'W':
					waypoint.X -= instruction.Amount;
					break;
				case 'E':
					waypoint.X += instruction.Amount;
					break;
				case 'L':
					for (int i = 0; i < instruction.Amount / 90; i++)
						waypoint = waypoint.RotateRight();
					break;
				case 'R':
					for (int i = 0; i < instruction.Amount / 90; i++)
						waypoint = waypoint.RotateLeft();
					break;
				case 'F':
					shipX += waypoint.X * instruction.Amount;
					shipY += waypoint.Y * instruction.Amount;
					break;
			}
			if (_gridPreview != null)
				worldmap[shipX, shipY] = '@';
			Console.WriteLine("-> Ship : " + new Point(shipX, shipY) + " - WP: " + waypoint);
			if (_gridPreview != null)
				_gridPreview.Offset = new Point(shipX - _gridPreview.Viewport.Width / 2, shipY - _gridPreview.Viewport.Height / 2);
			//_gridPreview?.Update();
		}
		return new Point(shipX, shipY).DistanceManhattan(Point.ZERO);
	}

}
