using System.Collections;
using Xunit;

namespace Test
{
	public class CodeTests
	{
		public class HexTests
		{
			public class SimpleDirections
			{
				[Theory()]
				[InlineData(0, 0, 0, 1)]
				[InlineData(1, 0, 1, 1)]
				[InlineData(0, 1, -1, 2)]
				public void NorthWest(int x0, int y0, int x1, int y1)
				{
					var hexMap = new HexMap<bool>(false);
					var hex = hexMap.GetHex(x0, y0);
					var northWest = hex.NorthWest;
					Assert.Equal(new Point(x1, y1), northWest.Point);
				}

				[Theory()]
				[InlineData(0, 0, 0, -1)]
				[InlineData(1, 0, 1, -1)]
				[InlineData(0, 1, -1, 0)]
				[InlineData(-1, -1, -2, -2)]
				public void SouthWest(int x0, int y0, int x1, int y1)
				{
					var hexMap = new HexMap<bool>(false);
					var hex = hexMap.GetHex(x0, y0);
					var southWest = hex.SouthWest;
					Assert.Equal(new Point(x1, y1), southWest.Point);
				}

				[Theory()]
				[InlineData(0, 0, 1, 1)]
				[InlineData(1, 0, 2, 1)]
				[InlineData(0, 1, 0, 2)]
				public void NorthEast(int x0, int y0, int x1, int y1)
				{
					var hexMap = new HexMap<bool>(false);
					var hex = hexMap.GetHex(x0, y0);
					var northEast = hex.NorthEast;
					Assert.Equal(new Point(x1, y1), northEast.Point);
				}

				[Theory()]
				[InlineData(0, 0, 1, -1)]
				[InlineData(1, 0, 2, -1)]
				[InlineData(0, 1, 0, 0)]
				public void SouthEast(int x0, int y0, int x1, int y1)
				{
					var hexMap = new HexMap<bool>(false);
					var hex = hexMap.GetHex(x0, y0);
					var southEast = hex.SouthEast;
					Assert.Equal(new Point(x1, y1), southEast.Point);
				}
			}

			public class MoveHexTests
			{

				[Theory()]
				[InlineData(0, 0, -1, -3, "seswneswswsenwwnwse")]

				[InlineData(0, 0, 1, 2, "neeenesenwnwwsw")]
				[InlineData(0, 0, 0, 3, "neeenesenwnwwswnenewnwwsewnenwseswesw")]

				[InlineData(0, 0, 0, 0, "sesenwnenenewseeswwswswwnene")]
				[InlineData(0, 0, -2, -2, "sesenwnenenewseeswwswswwnenewsewsw")]
				[InlineData(0, 0, -2, -2, "wsewsw")]
				[InlineData(0, 0, 0, -1, "wse")]
				[InlineData(0, 0, -1, -1, "wsew")]
				public void MoveTile(int x0, int y0, int x1, int y1, string movement)
				{
					var hexMap = new HexMap<int>(0);
					var hex = hexMap.GetHex(x0, y0);
					var moved = Day24.MoveTile(movement, hex);
					Assert.Equal(new Point(x1, y1), moved.Point);
				}
			}
		}
	}
}
