using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace Test
{
	public class Day20Tests
	{
		public class TileFits
		{
			[Fact]
			public void TileFits_Empty()
			{
				var tile = new Day20.GridSides(10, 20, 30, 40);
				var actual = Day20.TileFits(tile, 0, 0, new Day20.GridTile[2, 2]);
				Assert.True(actual);
			}

			[Fact]
			public void TileFits_Right_Matching()
			{
				var tile = new Day20.GridSides(10, 20, 30, 40);
				var grid = new Day20.GridTile[2, 2];
				grid[0, 0] = new Day20.GridTile(0, new Day20.GridSides(0, 40, 0, 0), Day20.RotationValue.Identity);
				var actual = Day20.TileFits(tile, 1, 0, grid);
				Assert.True(actual);
			}

			[Fact]
			public void TileFits_Right_NotMatching()
			{
				var tile = new Day20.GridSides(10, 20, 30, 40);
				var grid = new Day20.GridTile[2, 2];
				grid[0, 0] = new Day20.GridTile(0, new Day20.GridSides(20, 30, 10, 0), Day20.RotationValue.Identity);
				var actual = Day20.TileFits(tile, 1, 0, grid);
				Assert.False(actual);
			}

			[Fact]
			public void TileFits_Top_Matching()
			{
				var tile = new Day20.GridSides(10, 20, 30, 40);
				var grid = new Day20.GridTile[2, 2];
				grid[0, 0] = new Day20.GridTile(0, new Day20.GridSides(30, 40, 10, 0), Day20.RotationValue.Identity);
				var actual = Day20.TileFits(tile, 0, 1, grid);
				Assert.True(actual);
			}

			[Fact]
			public void TileFits_Top_NotMatching()
			{
				var tile = new Day20.GridSides(10, 20, 30, 40);
				var grid = new Day20.GridTile[2, 2];
				grid[0, 0] = new Day20.GridTile(0, new Day20.GridSides(20, 40, 30, 10), Day20.RotationValue.Identity);
				var actual = Day20.TileFits(tile, 0, 1, grid);
				Assert.False(actual);
			}

			[Fact]
			public void TileFits_TopRight_Matching()
			{
				var tile = new Day20.GridSides(10, 20, 30, 40);
				var grid = new Day20.GridTile[2, 2];
				grid[1, 0] = new Day20.GridTile(0, new Day20.GridSides(20, 40, 10, 0), Day20.RotationValue.Identity);
				grid[0, 1] = new Day20.GridTile(0, new Day20.GridSides(0, 40, 0, 0), Day20.RotationValue.Identity);
				var actual = Day20.TileFits(tile, 1, 1, grid);
				Assert.True(actual);
			}
		}

		public class Run
		{
			[Fact]
			public void Need_XFlip()
			{
				var tile = new Day20.GridSides(10, 20, 30, 40);
				var tileSides = new List<(int, Day20.GridSides)>();
				var ExpectedId = 1;
				tileSides.Add((ExpectedId, tile));

				var grid = new Day20.GridTile[2, 1];
				grid[0, 0] = new Day20.GridTile(0, new Day20.GridSides(0, 20, 0, 0), Day20.RotationValue.Identity);

				var (succes, answer) = Day20.Run(tileSides, 1, grid);

				Assert.True(succes);
				Assert.Equal(ExpectedId, answer[1, 0].TileId);
				Assert.Equal(Day20.RotationValue.X, answer[1, 0].Rotation);
			}

			[Theory()]
			[InlineData(Day20.RotationValue.Identity)]
			[InlineData(Day20.RotationValue.X)]
			[InlineData(Day20.RotationValue.Y)]
			[InlineData(Day20.RotationValue.X_Y)]
			[InlineData(Day20.RotationValue.R90)]
			[InlineData(Day20.RotationValue.R90_X)]
			[InlineData(Day20.RotationValue.R90_Y)]
			[InlineData(Day20.RotationValue.R90_X_Y)]
			[InlineData(Day20.RotationValue.R180)]
			[InlineData(Day20.RotationValue.R180_X)]
			[InlineData(Day20.RotationValue.R180_Y)]
			[InlineData(Day20.RotationValue.R180_X_Y)]
			[InlineData(Day20.RotationValue.R270)]
			[InlineData(Day20.RotationValue.R270_X)]
			[InlineData(Day20.RotationValue.R270_Y)]
			[InlineData(Day20.RotationValue.R270_X_Y)]
			public void RotationMatching_WithTopAndLeft(Day20.RotationValue expected)
			{
				var fliped = Day20.RotationToSides(new Day20.GridSides(10, 20, 30, 40), expected);
				var tileSides = new List<(int, Day20.GridSides)>();
				tileSides.Add((1, new Day20.GridSides(10, 20, 30, 40)));

				var grid = new Day20.GridTile[2, 2];
				grid[1, 0] = new Day20.GridTile(0, new Day20.GridSides(0, 0, fliped.Top, 0), Day20.RotationValue.Identity);
				grid[0, 1] = new Day20.GridTile(0, new Day20.GridSides(0, fliped.Left, 0, 0), Day20.RotationValue.Identity);
				var (succes, answer) = Day20.Run(tileSides, 3, grid);

				Assert.True(succes);
				Assert.Equal(expected, answer[1, 1].Rotation);
			}
		}


	}
}
