using System.Collections;
using Xunit;

namespace Test
{
	public class Day18Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day18Part1Answer, new Day18().Part1(DaysInputs.D18));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day18Part2Answer, new Day18().Part2(DaysInputs.D18));
		}
	}
}
