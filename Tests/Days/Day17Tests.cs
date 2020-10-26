using System.Collections;
using Xunit;

namespace Test
{
	public class Day17Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day17Part1Answer, new Day17().Part1(DaysInputs.D17));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day17Part2Answer, new Day17().Part2(DaysInputs.D17));
		}
	}
}
