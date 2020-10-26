using System.Collections;
using Xunit;

namespace Test
{
	public class Day6Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day6Part1Answer, new Day6().Part1(DaysInputs.D6));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day6Part2Answer, new Day6().Part2(DaysInputs.D6));
		}
	}
}
