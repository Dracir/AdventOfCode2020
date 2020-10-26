using System.Collections;
using Xunit;

namespace Test
{
	public class Day12Tests
	{
		public class Part1
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day12Part1Answer, new Day12().Part1(DaysInputs.D12));
		}
		public class Part2
		{
			[Fact]
			public void Input() => Assert.Equal(DaysAnswers.Day12Part2Answer, new Day12().Part2(DaysInputs.D12));
		}
	}
}
