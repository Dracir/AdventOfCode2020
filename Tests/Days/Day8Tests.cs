using System.Collections;
using Xunit;

namespace Test
{
	public class Day8Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day8Part1Answer, new Day8().Part1(DaysInputs.D8));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day8Part2Answer, new Day8().Part2(DaysInputs.D8));
		}
	}
}
