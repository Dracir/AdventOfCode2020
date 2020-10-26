using System.Collections;
using Xunit;

namespace Test
{
	public class Day9Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day9Part1Answer, new Day9().Part1(DaysInputs.D9));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day9Part2Answer, new Day9().Part2(DaysInputs.D9));
		}
	}
}
