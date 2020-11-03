using System;
using System.IO;
using System.Collections.Generic;

public class ConsoleCenter
{
	private int Lines = 3;
	private int StartingLine;
	private int _currentLine;

	public void Reset(int lines, int startingLine)
	{
		Lines = lines;
		StartingLine = startingLine;
		_currentLine = 0;
	}

	public void WriteLine(string text)
	{
		BetterConsole.WriteAt(text, _currentLine + StartingLine);

		_currentLine++;
		if (_currentLine >= Lines)
			_currentLine = 0;
	}
}