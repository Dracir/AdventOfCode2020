using System.Collections;
using Xunit;

namespace Test
{
	public class Day2Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day2Part1Answer, new Day2().Part1(DaysInputs.D2));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day2Part2Answer, new Day2().Part2(DaysInputs.D2));
		}
	}
}
