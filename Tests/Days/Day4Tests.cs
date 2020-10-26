using System.Collections;
using Xunit;

namespace Test
{
	public class Day4Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day4Part1Answer, new Day4().Part1(DaysInputs.D4));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day4Part2Answer, new Day4().Part2(DaysInputs.D4));
		}
	}
}
