using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public struct CodeInstruction
{
	public string Operation;
	public long Argument;

	public CodeInstruction(string operation, long argument)
	{
		Operation = operation;
		Argument = argument;
	}
}
