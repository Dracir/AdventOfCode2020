using System;
using System.IO;
using System.Collections.Generic;

public class HeaderValue
{
	private Point Position;
	private int Width;

	public HeaderValue(Point position, int width)
	{
		Position = position;
		Width = width;
	}

	public void SetValue(int value)
	{
		var p = BetterConsole.Position;
		BetterConsole.WriteAt(value.ToString(), Position.X, Position.Y);
		BetterConsole.Position = p;
	}
}