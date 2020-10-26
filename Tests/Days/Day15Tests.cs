using System.Collections;
using Xunit;

namespace Test
{
	public class Day15Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day15Part1Answer, new Day15().Part1(DaysInputs.D15));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day15Part2Answer, new Day15().Part2(DaysInputs.D15));
		}
	}
}
