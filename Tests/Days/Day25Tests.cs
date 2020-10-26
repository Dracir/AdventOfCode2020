using System.Collections;
using Xunit;

namespace Test
{
	public class Day25Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day25Part1Answer, new Day25().Part1(DaysInputs.D25));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day25Part2Answer, new Day25().Part2(DaysInputs.D25));
		}
	}
}
