using System.Collections;
using Xunit;

namespace Test
{
	public class Day3Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day3Part1Answer, new Day3().Part1(DaysInputs.D3));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day3Part2Answer, new Day3().Part2(DaysInputs.D3));
		}
	}
}
