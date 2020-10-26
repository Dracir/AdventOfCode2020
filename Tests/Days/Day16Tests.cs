using System.Collections;
using Xunit;

namespace Test
{
	public class Day16Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day16Part1Answer, new Day16().Part1(DaysInputs.D16));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day16Part2Answer, new Day16().Part2(DaysInputs.D16));
		}
	}
}
