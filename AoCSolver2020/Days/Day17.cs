using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day17 : DayBase
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

	public override bool Equals(object? obj) => base.Equals(obj);
	public override int GetHashCode() => base.GetHashCode();

	//-----------------------------------------------------------------

	public override long Part1(string input)
	{
		var inputGrid = InputParser.ParseBoolGrid(input, '\n', '#');
		ConwayGrid3D grid = GenerateGrid(inputGrid);

		for (int i = 0; i < 6; i++)
		{
			DoCycle(grid);
			foreach (var point in grid.PointsAndValues(grid.MinKeys, grid.MaxKeys))
				grid[point.Key] = (grid[point.Key].after, false);
		}

		var nbActives = grid.PointsAndValues().Where(x => x.Value.before).Count();
		Console.WriteLine($"Actives : {nbActives}");
		return nbActives;
	}

	private static ConwayGrid3D GenerateGrid(bool[,] inputGrid)
	{
		var grid = new ConwayGrid3D();

		for (int y = 0; y < inputGrid.GetLength(0); y++)
			for (int x = 0; x < inputGrid.GetLength(0); x++)
				grid[new int[] { y, x, 0 }] = (inputGrid[x, y], inputGrid[x, y]);
		return grid;
	}

	private static void DoCycle(ConwayGrid3D grid)
	{
		var comparer = new MultiDimentionalArray<bool>.MultiDimentionalArrayEqualityComparer();
		int nbActives = grid.PointsAndValues().Where(x => x.Value.before).Count();
		Console.WriteLine($"Actives : {nbActives}, Size: [{string.Join(",", grid.Size)}]");

		var minIndex = grid.MinKeys.Select(x => x - 1).ToArray();
		var maxIndex = grid.MaxKeys.Select(x => x + 1).ToArray();

		foreach (var point in grid.PointsAndValues(minIndex, maxIndex))
		{
			var nbActiveNeihbors = 0;
			foreach (var neighbor in grid.AreaSquareAround(point.Key, 1))
				if (neighbor.Value.before && !comparer.Equals(point.Key, neighbor.Key)) nbActiveNeihbors++;

			if (point.Value.before)
				grid[point.Key] = (point.Value.before, nbActiveNeihbors.Between(2, 3));
			else
				grid[point.Key] = (point.Value.before, nbActiveNeihbors == 3);
		}
	}

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		var inputGrid = InputParser.ParseBoolGrid(input, '\n', '#');
		var grid = GenerateGrid4D(inputGrid);

		for (int i = 0; i < 6; i++)
		{
			DoCycle(grid);
			foreach (var point in grid.PointsAndValues(grid.MinKeys, grid.MaxKeys))
				grid[point.Key] = (grid[point.Key].after, false);
		}

		var nbActives = grid.PointsAndValues().Where(x => x.Value.before).Count();
		Console.WriteLine($"Actives : {nbActives}");
		return nbActives;
	}

	private static ConwayGrid4D GenerateGrid4D(bool[,] inputGrid)
	{
		var grid = new ConwayGrid4D();

		for (int y = 0; y < inputGrid.GetLength(0); y++)
			for (int x = 0; x < inputGrid.GetLength(0); x++)
				grid[new int[] { y, x, 0, 0 }] = (inputGrid[x, y], inputGrid[x, y]);
		return grid;
	}

	private static void DoCycle(ConwayGrid4D grid)
	{
		var comparer = new MultiDimentionalArray<bool>.MultiDimentionalArrayEqualityComparer();
		int nbActives = grid.PointsAndValues().Where(x => x.Value.before).Count();
		Console.WriteLine($"Actives : {nbActives}, Size: [{string.Join(",", grid.Size)}]");

		var minIndex = grid.MinKeys.Select(x => x - 1).ToArray();
		var maxIndex = grid.MaxKeys.Select(x => x + 1).ToArray();

		foreach (var point in grid.PointsAndValues(minIndex, maxIndex))
		{
			var nbActiveNeihbors = 0;
			foreach (var neighbor in grid.AreaSquareAround(point.Key, 1))
				if (neighbor.Value.before && !comparer.Equals(point.Key, neighbor.Key)) nbActiveNeihbors++;

			if (point.Value.before)
				grid[point.Key] = (point.Value.before, nbActiveNeihbors.Between(2, 3));
			else
				grid[point.Key] = (point.Value.before, nbActiveNeihbors == 3);
		}
	}

	// -------------------------------------------

	public class ConwayGrid3D : MultiDimentionalArray<(bool before, bool after)>
	{
		public ConwayGrid3D() : base((false, false), 3)
		{
		}

	}
	public class ConwayGrid4D : MultiDimentionalArray<(bool before, bool after)>
	{
		public ConwayGrid4D() : base((false, false), 4)
		{
		}

	}
}
