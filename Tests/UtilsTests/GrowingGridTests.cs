using System;
using System.Collections;
using Xunit;

namespace Test
{
	public class GrowingGridTests
	{
		public class GrowthsTests
		{
			[Fact]
			public void FullSizeIsInclusiveOfRange()
			{
				var grid = new GrowingGrid<bool>(false, new Point(0, 2), new Point(0, 5), 3, true, true);
				Assert.Equal(3, grid.FullWidth);
				Assert.Equal(6, grid.FullHeight);
			}

			[Fact]
			public void LookingAtAllCorner_ShouldNotMakeAnyResize()
			{
				var grid = new GrowingGrid<bool>(false, new Point(0, 2), new Point(0, 5), 3, true, true);
				grid[0, 0] = true;
				grid[0, 5] = true;
				grid[2, 5] = true;
				grid[2, 5] = true;
				Assert.Equal(0, grid.GrowthTimesUp);
				Assert.Equal(0, grid.GrowthTimesRight);
				Assert.Equal(0, grid.GrowthTimesDown);
				Assert.Equal(0, grid.GrowthTimesLeft);
			}

			[Fact]
			public void LookingAtAllCorner_UsedSizeShouldBeLikeFull()
			{
				var grid = new GrowingGrid<bool>(false, new Point(0, 2), new Point(0, 5), 3, true, true);
				grid[0, 0] = true;
				grid[0, 2] = true;
				grid[2, 5] = true;
				grid[2, 5] = true;
				Assert.Equal(grid.FullWidth, grid.UsedWidth);
				Assert.Equal(grid.FullHeight, grid.UsedHeight);
			}

			[Fact]
			public void LookingAtAllCorner_HaveTheRightMinMaxXY()
			{
				var grid = new GrowingGrid<bool>(false, new Point(0, 2), new Point(0, 5), 3, true, true);
				grid[0, 0] = true;
				grid[0, 2] = true;
				grid[2, 5] = true;
				grid[2, 5] = true;

				Assert.Equal(0, grid.MinX);
				Assert.Equal(2, grid.MaxX);
				Assert.Equal(0, grid.MinY);
				Assert.Equal(5, grid.MaxY);
			}

			[Fact]
			public void GrowRight_IsBiggerToTheRight()
			{
				var grid = new GrowingGrid<bool>(false, new Point(0, 2), new Point(0, 5), 10, true, true);
				grid[3, 0] = true;

				Assert.Equal(0, grid.GrowthTimesUp);
				Assert.Equal(1, grid.GrowthTimesRight);
				Assert.Equal(0, grid.GrowthTimesDown);
				Assert.Equal(0, grid.GrowthTimesLeft);

				Assert.Equal(0, grid.MinX);
				Assert.Equal(12, grid.MaxX);
				Assert.Equal(13, grid.FullWidth);
				Assert.Equal(6, grid.FullHeight);

				Assert.Equal(0, grid.MinY);
				Assert.Equal(5, grid.MaxY);
			}

			[Fact]
			public void GrowTwiceRight_IsTwiceBiggerToTheRight()
			{
				var grid = new GrowingGrid<bool>(false, new Point(0, 2), new Point(0, 5), 10, true, true);
				grid[3, 0] = true;
				grid[20, 0] = true;

				Assert.Equal(0, grid.GrowthTimesUp);
				Assert.Equal(2, grid.GrowthTimesRight);
				Assert.Equal(0, grid.GrowthTimesDown);
				Assert.Equal(0, grid.GrowthTimesLeft);

				Assert.Equal(23, grid.FullWidth);
				Assert.Equal(6, grid.FullHeight);

			}

			[Fact]
			public void GrowRightOnce_With2TimeGrowthSize()
			{
				var grid = new GrowingGrid<bool>(false, new Point(0, 2), new Point(0, 5), 10, true, true);
				grid[23, 0] = true;

				Assert.Equal(0, grid.GrowthTimesUp);
				Assert.Equal(1, grid.GrowthTimesRight);
				Assert.Equal(0, grid.GrowthTimesDown);
				Assert.Equal(0, grid.GrowthTimesLeft);

				Assert.Equal(0, grid.MinX);
				Assert.Equal(32, grid.MaxX);
				Assert.Equal(33, grid.FullWidth);
			}

			[Fact]
			public void GrowLeftOnce_With2TimeGrowthSize()
			{
				var grid = new GrowingGrid<bool>(false, new Point(0, 2), new Point(0, 5), 10, true, true);
				grid[-16, 0] = true;

				Assert.Equal(0, grid.GrowthTimesUp);
				Assert.Equal(0, grid.GrowthTimesRight);
				Assert.Equal(0, grid.GrowthTimesDown);
				Assert.Equal(1, grid.GrowthTimesLeft);

				Assert.Equal(-20, grid.MinX);
				Assert.Equal(2, grid.MaxX);
				Assert.Equal(23, grid.FullWidth);
			}

			[Fact]
			public void GrowUpOnce_With2TimeGrowthSize()
			{
				var grid = new GrowingGrid<bool>(false, new Point(0, 2), new Point(0, 5), 10, true, true);
				grid[0, 16] = true;

				Assert.Equal(1, grid.GrowthTimesUp);
				Assert.Equal(0, grid.GrowthTimesRight);
				Assert.Equal(0, grid.GrowthTimesDown);
				Assert.Equal(0, grid.GrowthTimesLeft);

				Assert.Equal(0, grid.MinY);
				Assert.Equal(25, grid.MaxY);
				Assert.Equal(26, grid.FullHeight);
			}

			[Fact]
			public void GrowDownOnce_With2TimeGrowthSize()
			{
				var grid = new GrowingGrid<bool>(false, new Point(0, 2), new Point(0, 5), 10, true, true);
				grid[0, -16] = true;

				Assert.Equal(0, grid.GrowthTimesUp);
				Assert.Equal(0, grid.GrowthTimesRight);
				Assert.Equal(1, grid.GrowthTimesDown);
				Assert.Equal(0, grid.GrowthTimesLeft);

				Assert.Equal(-20, grid.MinY);
				Assert.Equal(5, grid.MaxY);
				Assert.Equal(26, grid.FullHeight);
			}
		}

		public class GrowingOffsetedTests
		{
			[Fact]
			public void OffsetedHasRightMinMax()
			{
				var grid = new GrowingGrid<bool>(false, new Point(-2, 2), new Point(-5, 5), 3, true, true);
				Assert.Equal(-2, grid.MinX);
				Assert.Equal(2, grid.MaxX);
				Assert.Equal(-5, grid.MinY);
				Assert.Equal(5, grid.MaxY);
			}
			[Fact]
			public void CheckLeft_DoesntGrow()
			{
				var grid = new GrowingGrid<bool>(false, new Point(-2, 2), new Point(-5, 5), 3, true, true);
				grid[-2, 0] = true;
				Assert.Equal(true, grid[-2, 0]);
				Assert.Equal(0, grid.GrowthTimesLeft);
				Assert.Equal(0, grid.GrowthTimes);
			}
			[Fact]
			public void CheckRight_DoesntGrow()
			{
				var grid = new GrowingGrid<bool>(false, new Point(-2, 2), new Point(-5, 5), 3, true, true);
				grid[2, 0] = true;
				Assert.Equal(true, grid[2, 0]);
				Assert.Equal(0, grid.GrowthTimesRight);
				Assert.Equal(0, grid.GrowthTimes);
			}
			[Fact]
			public void CheckUp_DoesntGrow()
			{
				var grid = new GrowingGrid<bool>(false, new Point(-2, 2), new Point(-5, 5), 3, true, true);
				grid[0, 5] = true;
				Assert.Equal(true, grid[0, 5]);
				Assert.Equal(0, grid.GrowthTimesUp);
				Assert.Equal(0, grid.GrowthTimes);
			}
			[Fact]
			public void CheckDown_DoesntGrow()
			{
				var grid = new GrowingGrid<bool>(false, new Point(-2, 2), new Point(-5, 5), 3, true, true);
				grid[0, -5] = true;
				Assert.Equal(true, grid[0, -5]);
				Assert.Equal(0, grid.GrowthTimesDown);
				Assert.Equal(0, grid.GrowthTimes);
			}
		}

		public class AddGrid
		{
			int[,] SmallSquare = new int[,] { { 1, 2 }, { 4, 3 } };

			[Fact]
			public void SimpleAddGrid()
			{
				var grid = new GrowingGrid<int>(0, new Point(0, 2), new Point(0, 5), 10, true, true);
				var gridToAdd = new int[,] {
					{ 1, 0, 2 }, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, { 4, 0, 3 }
				};

				grid.AddGrid(0, 0, gridToAdd, GridAxes.YX);

				Assert.Equal(1, grid[0, 0]);
				Assert.Equal(2, grid[2, 0]);
				Assert.Equal(3, grid[2, 5]);
				Assert.Equal(4, grid[0, 5]);

			}

			[Fact]
			public void OffsetedAddGrid()
			{
				var grid = new GrowingGrid<int>(0, new Point(0, 2), new Point(0, 5), 10, true, true);

				grid.AddGrid(1, 1, SmallSquare, GridAxes.YX);

				Assert.Equal(1, grid[1, 1]);
				Assert.Equal(2, grid[2, 1]);
				Assert.Equal(3, grid[2, 2]);
				Assert.Equal(4, grid[1, 2]);

			}

			[Fact]
			public void AddGridOnGrowth()
			{
				var grid = new GrowingGrid<int>(0, new Point(0, 2), new Point(0, 5), 10, true, true);
				grid.OnGridGrown = DoAddGridOnGrowth;

				Assert.Equal(1, grid[3, 0]);
				Assert.Equal(2, grid[4, 0]);
				Assert.Equal(3, grid[4, 1]);
				Assert.Equal(4, grid[3, 1]);

			}

			private void DoAddGridOnGrowth(GrowingGrid<int>.GrowingGridEvent obj)
			{
				obj.Grid.AddGrid(obj.Grid.FullWidth - obj.Right, 0, SmallSquare, GridAxes.YX);
			}
		}
	}
}
