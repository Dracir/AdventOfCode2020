using System;
using System.IO;
using System.Collections.Generic;

public abstract class HeaderValue
{
	public readonly Point Position;

	protected HeaderValue(Point position)
	{
		Position = position;
	}

	protected abstract int TotalWidth { get; }

	public abstract void SetValue(int value);
	public abstract void SetValue(float value);
	public abstract void SetValue(string value);



	protected void WriteValue(string value)
	{
		var width = TotalWidth;
		var p = BetterConsole.Position;

		if (value.Length < width)
			value = value.PadRight(width);
		else if (value.Length > width)
			value = value.ToString().Substring(0, width);

		Console.ForegroundColor = ConsoleManager.Skin.HeaderValueColor;
		BetterConsole.WriteAt(value, Position.X, Position.Y);
		Console.ResetColor();

		BetterConsole.Position = p;

	}
}