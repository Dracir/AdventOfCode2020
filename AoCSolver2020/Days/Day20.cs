using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day20 : DayBase
{

	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
		Console.Header.ReserveLines(0);
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

	public override bool Equals(object obj) => base.Equals(obj);
	public override int GetHashCode() => base.GetHashCode();

	public Dictionary<int, Tile> ParseInput(string input)
	{
		var tiles = new Dictionary<int, Tile>();
		foreach (var tileStr in input.Split("\n\n"))
		{
			var splited = tileStr.Split("\n", 2);
			var id = int.Parse(splited[0].Substring(4, 5));
			var gridBool = InputParser.ParseBoolGrid(splited[1], '\n', '#');
			var grid = Grid<bool>.FromArray(false, gridBool, GridAxes.YX);
			tiles.Add(id, new Tile(id, grid));
		}
		return tiles;
	}

	//-----------------------------------------------------------------

	public override long Part1(string input)
	{
		var tiles = ParseInput(input);
		var result = Solve(tiles);

		return 0;
	}

	private object Solve(Dictionary<int, Tile> tiles)
	{
		var tileSides = tiles.Select(x => (x.Key, x.Value.Sides));
		var size = (int)Math.Sqrt(tiles.Count);

		var (succes, answer) = Run(tileSides, 0, new GridTile[size, size]);
		if (succes)
		{
			for (int y = 0; y < size; y++)
			{
				var line = "";
				for (int x = 0; x < size; x++)
					line += answer[x, y].TileId + " ";
				Console.WriteLine(line);
			}
		}


		Console.WriteLine("Succes : " + succes);

		return null;
	}

	public static (bool succes, GridTile[,] newGrid) Run(IEnumerable<(int Key, GridSides Sides)> remainingTiles, int index, GridTile[,] currentGrid)
	{
		var sideSize = currentGrid.GetLength(0);
		var x = index % sideSize;
		var y = index / sideSize;
		var testingGrid = (GridTile[,])currentGrid.Clone();

		//Console.WriteLine(x + " , " + y);

		foreach (var tile in remainingTiles)
		{
			/*var a = GetVariations(tile.Sides).ToList();
			var va = a
				.Select(x => x.Sides)
				.Select(x => $"{x.Top},{x.Right},{x.Bottom},{x.Left},")
				.ToList();
			foreach (var v in va)
				Console.WriteLine(v);*/
			foreach (var variation in GetVariations(tile.Sides))
			{
				if (TileFits(variation.Sides, x, y, currentGrid))
				{
					testingGrid[x, y] = new GridTile(tile.Key, variation.Sides, variation.RotationValue);
					if (index == currentGrid.Length - 1)
						return (true, testingGrid);

					var subTestTiles = remainingTiles.Where(x => x.Key != tile.Key);
					var (sucess, subGrid) = Run(subTestTiles, index + 1, testingGrid);
					if (sucess)
						return (true, subGrid);
				}
			}
		}



		return (false, null);
	}

	public static bool TileFits(GridSides tile, int x, int y, GridTile[,] fullImage)
	{
		if (x > 0 && fullImage[x - 1, y].Sides.Right != tile.Left)
			return false;
		else if (y > 0 && fullImage[x, y - 1].Sides.Bottom != tile.Top)
			return false;
		/*else if (x < fullImage.GetLength(0) - 1 && fullImage[x + 1, y] != null && fullImage[x + 1, y].Sides.Right == tile.Left)
			return false;
		else if (y < fullImage.GetLength(0) - 1 && fullImage[x, y + 1] != null && fullImage[x, y + 1].Sides.Bottom == tile.Top)
			return false;*/
		else
			return true;
	}

	public static IEnumerable<(RotationValue RotationValue, GridSides Sides)> GetVariations(GridSides sides)
	{
		yield return (RotationValue.Identity, sides);
		yield return (RotationValue.X, sides.XFliped);
		yield return (RotationValue.Y, sides.YFliped);
		yield return (RotationValue.X_Y, sides.XFliped.YFliped);

		yield return (RotationValue.R90, sides.Rotated);
		yield return (RotationValue.R90_X, sides.Rotated.XFliped);
		yield return (RotationValue.R90_Y, sides.Rotated.YFliped.XFliped);
		yield return (RotationValue.R90_X_Y, sides.Rotated.YFliped);

		yield return (RotationValue.R180, sides.Rotated.Rotated);
		yield return (RotationValue.R180_X, sides.Rotated.Rotated.XFliped);
		yield return (RotationValue.R180_Y, sides.Rotated.Rotated.YFliped.XFliped);
		yield return (RotationValue.R180_X_Y, sides.Rotated.Rotated.YFliped);

		yield return (RotationValue.R270, sides.Rotated.Rotated.Rotated);
		yield return (RotationValue.R270_X, sides.Rotated.Rotated.Rotated.XFliped);
		yield return (RotationValue.R270_Y, sides.Rotated.Rotated.Rotated.YFliped.XFliped);
		yield return (RotationValue.R270_X_Y, sides.Rotated.Rotated.Rotated.YFliped);
		/*
		710,498,564,841,
313,841,459,498,
564,525,710,182,
459,182,313,525,
841,710,498,564,
182,564,525,710,
525,459,182,313,
498,313,841,459,
564,841,710,498,
459,498,313,841,
313,525,459,182,
710,182,564,525,
498,564,841,710,
525,710,182,564,
182,313,525,459,
841,459,498,313,
*/
	}

	public static GridSides RotationToSides(GridSides sides, RotationValue rotation) => rotation switch
	{
		RotationValue.Identity => sides,
		RotationValue.X => sides.XFliped,
		RotationValue.Y => sides.YFliped,
		RotationValue.X_Y => sides.XFliped.YFliped,
		RotationValue.R90 => sides.Rotated,
		RotationValue.R90_X => sides.Rotated.XFliped,
		RotationValue.R90_Y => sides.Rotated.YFliped.XFliped,
		RotationValue.R90_X_Y => sides.Rotated.YFliped,
		RotationValue.R180 => sides.Rotated.Rotated,
		RotationValue.R180_X => sides.Rotated.Rotated.XFliped,
		RotationValue.R180_Y => sides.Rotated.Rotated.YFliped.XFliped,
		RotationValue.R180_X_Y => sides.Rotated.Rotated.YFliped,
		RotationValue.R270 => sides.Rotated.Rotated.Rotated,
		RotationValue.R270_X => sides.Rotated.Rotated.Rotated.XFliped,
		RotationValue.R270_Y => sides.Rotated.Rotated.Rotated.YFliped.XFliped,
		RotationValue.R270_X_Y => sides.Rotated.Rotated.Rotated.YFliped,
		_ => sides,
	};

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		var tile = new Day20.GridSides(10, 20, 30, 40);
		var tileSides = new List<(int, Day20.GridSides)>();
		var ExpectedId = 1;
		tileSides.Add((ExpectedId, tile));

		var grid = new Day20.GridTile[2, 1];
		grid[0, 0] = new Day20.GridTile(0, new Day20.GridSides(0, 20, 0, 0), Day20.RotationValue.X);

		var (succes, answer) = Day20.Run(tileSides, 1, grid);

		return 0;

		/*	yield return new bool[,] { { tl, tr }, { bl, br } }; // identity

			yield return new bool[,] { { tr, tl }, { br, bl } }; // X flip
			yield return new bool[,] { { bl, br }, { tl, tr } }; // Y flip
			yield return new bool[,] { { br, bl }, { tr, tl } }; // X & Y flip

			yield return new bool[,] { { br, tl }, { tr, bl } }; // 90 rotation
			yield return new bool[,] { { tl, br }, { bl, tr } }; // 90 rotation & X flip
			yield return new bool[,] { { tr, bl }, { br, tl } }; // 90 rotation & Y flip
			yield return new bool[,] { { bl, tr }, { tl, br } }; // 90 rotation & Y flip & X flip
			yield return new bool[,] { { bl, br }, { tl, tr } }; // 180 rotation
			yield return new bool[,] { { br, bl }, { tr, tl } }; // 180 rotation & X flip
			yield return new bool[,] { { tl, tr }, { bl, br } }; // 180 rotation & Y flip
			yield return new bool[,] { { tr, tl }, { br, bl } }; // 180 rotation & Y flip & X flip
			yield return new bool[,] { { tr, bl }, { br, tl } }; // 270 rotation
			yield return new bool[,] { { bl, tr }, { tl, br } }; // 270 rotation & X Flip
			yield return new bool[,] { { br, tl }, { tr, bl } }; // 270 rotation & Y flip
			yield return new bool[,] { { tl, br }, { bl, tr } }; // 270 rotation & Y flip & X flip*/
	}


	public class Tile
	{
		public readonly int Id;
		public readonly Grid<bool> Grid;

		public Tile(int id, Grid<bool> grid)
		{
			this.Id = id;
			this.Grid = grid;
		}

		public string TopValues => new String(Enumerable.Range(0, 10).Select(x => Grid[x, 0] ? '1' : '0').ToArray());
		public string BottomValues => new String(Enumerable.Range(0, 10).Select(x => Grid[x, 9] ? '1' : '0').ToArray());
		public string LeftValues => new String(Enumerable.Range(0, 10).Select(y => Grid[0, y] ? '1' : '0').ToArray());
		public string RightValues => new String(Enumerable.Range(0, 10).Select(y => Grid[9, y] ? '1' : '0').ToArray());

		public int Top => Convert.ToInt32(TopValues, 2);
		public int Bottom => Convert.ToInt32(BottomValues, 2);
		public int Left => Convert.ToInt32(LeftValues, 2);
		public int Right => Convert.ToInt32(RightValues, 2);

		public bool TopLeft => Grid[0, 0];
		public bool TopRight => Grid[9, 0];
		public bool BottomLeft => Grid[0, 9];
		public bool BottomRight => Grid[9, 9];

		public bool[,] Corners => new bool[,] { { TopLeft, TopRight }, { BottomLeft, BottomRight } };
		public GridSides Sides => new GridSides(Top, Right, Bottom, Left);
	}
	public struct GridSides
	{
		public GridSides(int top, int right, int bottom, int Left)
		{
			Top = top;
			Right = right;
			Bottom = bottom;
			this.Left = Left;
		}

		public int Top { get; }
		public int Right { get; }
		public int Bottom { get; }
		public int Left { get; }

		public GridSides XFliped => new GridSides(1023 - Top, Left, 1023 - Bottom, Right);
		public GridSides YFliped => new GridSides(Bottom, 1023 - Right, Top, 1023 - Left);
		public GridSides Rotated => new GridSides(Left, Top, Right, Bottom);
	}

	public class GridTile
	{
		public GridTile(int tileId, GridSides sides, RotationValue rotation)
		{
			TileId = tileId;
			Sides = sides;
			Rotation = rotation;
		}

		public int TileId { get; }
		public GridSides Sides { get; }
		public RotationValue Rotation { get; }

	}

	public enum RotationValue
	{
		Identity, X, Y, X_Y, R90, R90_X, R90_Y, R90_X_Y, R180, R180_X, R180_Y, R180_X_Y, R270, R270_X, R270_Y, R270_X_Y
	}
}
