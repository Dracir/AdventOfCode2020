using System.Collections;
using Xunit;

namespace Test
{
	public class Day1Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day1Part1Answer, new Day1().Part1(DaysInputs.D1));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day1Part2Answer, new Day1().Part2(DaysInputs.D1));
		}
	}
}
