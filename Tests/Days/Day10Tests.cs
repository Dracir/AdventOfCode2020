using System.Collections;
using Xunit;

namespace Test
{
	public class Day10Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day10Part1Answer, new Day10().Part1(DaysInputs.D10));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day10Part2Answer, new Day10().Part2(DaysInputs.D10));
		}
	}
}
