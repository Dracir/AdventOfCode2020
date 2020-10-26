using System.Collections;
using Xunit;

namespace Test
{
	public class Day11Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day11Part1Answer, new Day11().Part1(DaysInputs.D11));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day11Part2Answer, new Day11().Part2(DaysInputs.D11));
		}
	}
}
