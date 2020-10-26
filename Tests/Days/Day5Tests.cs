using System.Collections;
using Xunit;

namespace Test
{
	public class Day5Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day5Part1Answer, new Day5().Part1(DaysInputs.D5));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day5Part2Answer, new Day5().Part2(DaysInputs.D5));
		}
	}
}
