using System.Collections;
using Xunit;

namespace Test
{
	public class Day7Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day7Part1Answer, new Day7().Part1(DaysInputs.D7));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day7Part2Answer, new Day7().Part2(DaysInputs.D7));
		}
	}
}
