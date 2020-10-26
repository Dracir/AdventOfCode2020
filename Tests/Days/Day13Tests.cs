using System.Collections;
using Xunit;

namespace Test
{
	public class Day13Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day13Part1Answer, new Day13().Part1(DaysInputs.D13));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day13Part2Answer, new Day13().Part2(DaysInputs.D13));
		}
	}
}
