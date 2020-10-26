using System.Collections;
using Xunit;

namespace Test
{
	public class Day24Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day24Part1Answer, new Day24().Part1(DaysInputs.D24));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day24Part2Answer, new Day24().Part2(DaysInputs.D24));
		}
	}
}
