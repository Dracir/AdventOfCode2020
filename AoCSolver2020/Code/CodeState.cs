using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public struct CodeState
{
	public long Accumulator;
	public int InstructionPointer;

	public CodeState(long accumulator, int instructionPointer)
	{
		Accumulator = accumulator;
		InstructionPointer = instructionPointer;
	}
}