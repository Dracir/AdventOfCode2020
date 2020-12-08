using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CodeCompiler
{
	public static CodeProgram ParseProgram(string input)
	{
		var instructions = new List<CodeInstruction>();

		foreach (var instructionLine in input.Split("\n"))
		{
			var lineSplited = instructionLine.Split(" ");
			var operation = lineSplited[0];
			var argument = long.Parse(lineSplited[1]);
			instructions.Add(new CodeInstruction(operation, argument));
		}

		var initialState = new CodeState(0L, 0L);
		return new CodeProgram(initialState, instructions);
	}
}