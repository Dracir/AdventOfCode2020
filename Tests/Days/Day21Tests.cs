using System.Collections;
using Xunit;

namespace Test
{
	public class Day21Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day21Part1Answer, new Day21().Part1(DaysInputs.D21));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day21Part2Answer, new Day21().Part2(DaysInputs.D21));
		}
	}
}
