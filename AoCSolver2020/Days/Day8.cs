using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day8 : DayBase
{

	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
		Console.Header.ReserveLines(0);
	}

	//-----------------------------------------------------------------

	public override void SetUpConsolePart2()
	{
		Console.Header.ReserveLines(0);
	}

	//-----------------------------------------------------------------

	public override void CleanUp()
	{
	}

	//-----------------------------------------------------------------

	public override bool Equals(object? obj) => base.Equals(obj);
	public override int GetHashCode() => base.GetHashCode();

	//-----------------------------------------------------------------

	public override long Part1(string input)
	{
		var program = CodeCompiler.ParseProgram(input);
		var instructionDone = new List<int>();

		int infinitProtection = 100000;
		while (infinitProtection-- > 0)
		{
			if (instructionDone.Contains(program.CurrentState.InstructionPointer))
				break;
			instructionDone.Add(program.CurrentState.InstructionPointer);
			program.Step();

			Console.WriteLine(program.GetStateString());

		}
		return program.CurrentState.Accumulator;
	}

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		var program = CodeCompiler.ParseProgram(input);

		for (var instructionLine = 7; instructionLine < program.Instructions.Count; instructionLine++)
		{

			var instruction = program.Instructions[instructionLine];
			if (instruction.Operation == "jmp")
				program.Instructions[instructionLine] = new CodeInstruction("nop", instruction.Argument);
			else if (instruction.Operation == "nop")
				program.Instructions[instructionLine] = new CodeInstruction("jmp", instruction.Argument);
			else
				continue;

			Console.WriteLine($"Trying with change of line {instructionLine}\n");

			int infinitProtection = 10000;
			while (infinitProtection-- > 0)
			{
				if (program.IsProgramDone)
					return program.CurrentState.Accumulator;
				program.Step();
				//Console.WriteLine(program.GetStateString());
			}

			program = CodeCompiler.ParseProgram(input);
		}
		return 0;
	}

}
