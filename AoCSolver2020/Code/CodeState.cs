using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CodeState
{
	public long Accumulator;
	public long Pointer;

	public CodeState(long accumulator, long pointer)
	{
		Accumulator = accumulator;
		Pointer = pointer;
	}
}