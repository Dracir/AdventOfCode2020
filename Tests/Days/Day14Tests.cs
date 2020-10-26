using System.Collections;
using Xunit;

namespace Test
{
	public class Day14Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day14Part1Answer, new Day14().Part1(DaysInputs.D14));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day14Part2Answer, new Day14().Part2(DaysInputs.D14));
		}
	}
}
