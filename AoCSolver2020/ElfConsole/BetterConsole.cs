
using System;

public static class BetterConsole
{
	public static int Width { get { return Console.WindowWidth; } }
	public static int Height { get { return Console.WindowHeight - 1; } }
	public static int Right { get { return Console.WindowWidth - 1; } }
	public static int Bottom { get { return Console.WindowHeight - 2; } }

	private static int WriteLeft { get { return 0; } }
	private static int WriteRight { get { return Width - 0; } }
	private static int WriteTop { get { return 0; } }
	private static int WriteHeight { get { return Height - WriteTop; } }
	private static int WriteWidth { get { return Width; } }

	public static Point Position
	{
		get { return new Point(Console.CursorLeft, Console.CursorTop); }
		set
		{
			Console.CursorLeft = Math.Clamp(value.X, 0, Right);
			Console.CursorTop = Math.Clamp(value.Y, 0, Bottom);
		}
	}

	public static ConsoleColor ForegroundColor
	{
		get => Console.ForegroundColor;
		set => Console.ForegroundColor = value;
	}

	public static void ResetColor() => Console.ResetColor();

	public static void WriteAt(string value, int line) => WriteAt(value, 0, line);

	public static void WriteAt(string value, int x, int y)
	{
		if (value.Contains("\n"))
		{
			foreach (var line in value.Split("\n"))
				y += WriteLine(line, x, y);
		}
		else
			WriteLine(value, x, y);
	}

	private static int WriteLine(string value, int x, int y)
	{
		int lines = 1;
		foreach (var c in value)
		{
			if (y >= WriteHeight)
				return lines;
			if (x >= WriteWidth)
			{
				lines++;
				y++;
				x = 0;
			}
			Position = new Point(WriteLeft + x, WriteTop + y);
			Console.Write(c);
			x++;
		}

		return lines;
	}
}