using System.Collections;
using Xunit;

namespace Test
{
	public class Day19Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day19Part1Answer, new Day19().Part1(DaysInputs.D19));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day19Part2Answer, new Day19().Part2(DaysInputs.D19));
		}
	}
}
