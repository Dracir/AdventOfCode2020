using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CodeProgram
{
	public CodeState CurrentState;
	public List<CodeInstruction> Instructions;

	public CodeProgram(CodeState initialState, List<CodeInstruction> instructions)
	{
		CurrentState = initialState;
		Instructions = instructions;
	}
}