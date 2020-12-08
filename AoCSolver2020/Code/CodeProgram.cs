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

	public void Step()
	{
		var currentInstruction = GetCurrentInstruction();
		var newState = Compute(CurrentState, currentInstruction);
		CurrentState = newState;
	}

	public CodeState Compute(CodeState state, CodeInstruction instruction)
	{
		var func = GetFunction(instruction);
		return func(state, instruction.Argument);
	}
	// -------------------------------------------

	public bool IsProgramDone => (int)CurrentState.InstructionPointer >= Instructions.Count;
	private CodeInstruction GetCurrentInstruction()
	{
		if (IsProgramDone)
			return new CodeInstruction("exit", 0L);
		else
			return Instructions[(int)CurrentState.InstructionPointer];
	}


	private Func<CodeState, long, CodeState> GetFunction(CodeInstruction instruction) => instruction.Operation switch
	{
		"nop" => NOP,
		"jmp" => JMP,
		"acc" => ACC,
		"exit" => EXIT,
		_ => (state, _) => state,
	};

	// -------------------------------------------

	public static CodeState ACC(CodeState state, long argument)
	{
		return new CodeState(state.Accumulator + argument, state.InstructionPointer + 1);
	}

	public static CodeState NOP(CodeState state, long argument)
	{
		return new CodeState(state.Accumulator, state.InstructionPointer + 1);
	}

	public static CodeState JMP(CodeState state, long argument)
	{
		return new CodeState(state.Accumulator, state.InstructionPointer + (int)argument);
	}

	public static CodeState EXIT(CodeState state, long argument)
	{
		return new CodeState(state.Accumulator, state.InstructionPointer);
	}


	// -------------------------------------------


	public string GetStateString()
	{
		var currentInstruction = GetCurrentInstruction();
		var plusSymbol = currentInstruction.Argument >= 0 ? "+" : "";
		var instruction = $"{currentInstruction.Operation} {plusSymbol}{currentInstruction.Argument}";
		return $"[{CurrentState.InstructionPointer}]{instruction} \t| {CurrentState.Accumulator}";
	}

}