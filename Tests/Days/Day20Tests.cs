using System.Collections;
using Xunit;

namespace Test
{
	public class Day20Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day20Part1Answer, new Day20().Part1(DaysInputs.D20));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day20Part2Answer, new Day20().Part2(DaysInputs.D20));
		}
	}
}
