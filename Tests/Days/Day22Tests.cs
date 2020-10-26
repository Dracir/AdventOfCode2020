using System.Collections;
using Xunit;

namespace Test
{
	public class Day22Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day22Part1Answer, new Day22().Part1(DaysInputs.D22));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day22Part2Answer, new Day22().Part2(DaysInputs.D22));
		}
	}
}
