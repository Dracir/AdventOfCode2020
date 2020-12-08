using System.Collections;
using Xunit;

namespace Test
{
	public class CodeTests
	{
		public class Day8Tests
		{
			[Fact]
			public void Part1ExempleAnswer() => Assert.Equal(5, new Day8().Part1(DaysInputs.ReadInput("Day8Exemple1")));
			[Fact]
			public void Part1Answer() => Assert.Equal(DaysAnswers.Day8Part1Answer, new Day8().Part1(DaysInputs.D8));
			[Fact]
			public void Part2ExempleAnswer() => Assert.Equal(8, new Day8().Part2(DaysInputs.ReadInput("Day8Exemple1")));
			[Fact]
			public void Part2Answer() => Assert.Equal(DaysAnswers.Day8Part2Answer, new Day8().Part2(DaysInputs.D8));

			[Fact]
			public void Nop_OnlyIncreaseInstructionPointer()
			{
				var program = CodeCompiler.ParseProgram(DaysInputs.ReadInput("Day8Exemple1"));

			}


		}
		public class Part2
		{

		}
	}
}
