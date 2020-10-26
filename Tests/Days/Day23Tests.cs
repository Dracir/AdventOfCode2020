using System.Collections;
using Xunit;

namespace Test
{
	public class Day23Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day23Part1Answer, new Day23().Part1(DaysInputs.D23));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day23Part2Answer, new Day23().Part2(DaysInputs.D23));
		}
	}
}
