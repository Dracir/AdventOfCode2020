using System;
using System.IO;
using System.Collections.Generic;

public class HeaderValue
{
	private Point Position;
	private int Width;
	private string? Format;

	public HeaderValue(Point position, int width, string? format = null)
	{
		Position = position;
		Width = width;
		Format = format;
	}

	public void SetValue(int value)
	{
		if (Format != null)
			WriteValue(value.ToString(Format));
		else
			WriteValue(FormatDefault(value.ToString()));
	}

	public void SetValue(float value)
	{
		if (Format != null)
			WriteValue(value.ToString(Format));
		else
			WriteValue(FormatDefault(value.ToString()));
	}

	public void SetValue(string value)
	{
		if (Format != null)
			WriteValue(string.Format(Format, value));
		else
			WriteValue(FormatDefault(value));
	}

	private string FormatDefault(string value)
	{
		if (value.Length > Width)
			return value.ToString().Substring(0, Width);
		else
			return value;
	}

	private void WriteValue(string value)
	{
		var p = BetterConsole.Position;

		if (value.Length < Width)
			value = value.PadRight(Width);
		else if (value.Length > Width)
			value = value.ToString().Substring(0, Width);

		BetterConsole.WriteAt(value, Position.X, Position.Y);

		BetterConsole.Position = p;

	}

}