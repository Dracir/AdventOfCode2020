using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

public class ConsoleCenter
{
	private int Lines = 3;
	private int StartingLine;
	private int _currentLine;
	private int longestLine = 0;

	public void Reset(int lines, int startingLine)
	{
		Lines = lines;
		StartingLine = startingLine;
		_currentLine = 0;
	}

	public void WriteLine(string text)
	{
		var lines = text.Split("\n");
		foreach (var line in lines)
		{
			longestLine = Math.Max(longestLine, line.Length);
			BetterConsole.WriteAtLine(line, _currentLine + StartingLine, longestLine);

			_currentLine++;
			if (_currentLine >= Lines)
				_currentLine = 0;
		}

	}
}