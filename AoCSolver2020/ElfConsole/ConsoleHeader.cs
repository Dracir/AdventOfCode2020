using System;
using System.IO;
using System.Collections.Generic;

public class ConsoleHeader
{
	private static int ReservedLineStart = 3;
	private int ReservedLines;
	private Point CurrentHeaderPointer;


	public void SetTitle(int day, string title, int part)
	{
		Console.ForegroundColor = ConsoleManager.Skin.FramesColor;
		var line = new String('═', BetterConsole.Width - 2);
		var titleText = $"  Day {day}: {title} - Part {part}";

		Console.ForegroundColor = ConsoleManager.Skin.FramesColor;
		BetterConsole.WriteAt($"╔{line}╗", 0);
		BetterConsole.WriteAt("║", 1);
		BetterConsole.WriteAt("║", BetterConsole.Width - 1, 1);
		BetterConsole.WriteAt($"╚{line}╝", 2);

		Console.ForegroundColor = ConsoleManager.Skin.DayTitleColor;
		BetterConsole.WriteAt(titleText, 1, 1);

		Console.ResetColor();
	}


	public void ReserveLines(int reserveLines)
	{
		CurrentHeaderPointer = new Point(0, ReservedLineStart);
		ReservedLines = reserveLines;
		if (reserveLines == 0)
		{
			BetterConsole.Position = new Point(0, ReservedLineStart + reserveLines);
		}
		else
		{
			BetterConsole.Position = new Point(0, ReservedLineStart + reserveLines + 1);
			var line = new String('═', BetterConsole.Width);
			Console.ForegroundColor = ConsoleManager.Skin.FramesColor;
			BetterConsole.WriteAt(line, ReservedLineStart + reserveLines);

			Console.ResetColor();

		}
	}

	private HeaderValue CreateValue(int valueWidth, string title, Func<Point, HeaderValue> createHeader)
	{
		var consolePositionBefore = BetterConsole.Position;
		var totalWidth = valueWidth + title.Length + 1;

		if (CurrentHeaderPointer.X + totalWidth > BetterConsole.Width)
			CurrentHeaderPointer = new Point(1, CurrentHeaderPointer.Y + 1);

		Console.ForegroundColor = ConsoleManager.Skin.FramesColor;
		BetterConsole.WriteAt("|", CurrentHeaderPointer.X, CurrentHeaderPointer.Y);
		BetterConsole.WriteAt("|", CurrentHeaderPointer.X + totalWidth, CurrentHeaderPointer.Y);

		Console.ForegroundColor = ConsoleManager.Skin.HeaderLabelColor;
		BetterConsole.WriteAt(title, CurrentHeaderPointer.X + 1, CurrentHeaderPointer.Y);

		var p = new Point(CurrentHeaderPointer.X + title.Length, CurrentHeaderPointer.Y);
		var headerValue = createHeader(p);

		CurrentHeaderPointer = new Point(CurrentHeaderPointer.X + totalWidth, CurrentHeaderPointer.Y);
		BetterConsole.Position = consolePositionBefore;

		Console.ResetColor();

		return headerValue;
	}

	public HeaderValue CreateFormatedValue(int valueWidth, string title, string? format = null)
	{
		return CreateValue(valueWidth, title,
		(position) => new FormatedHeaderValue(position, valueWidth, format));
	}

	public HeaderValue CreateBlockValue(int valueWidth, string title, ValueToUTFBars.Styles style)
	{
		return CreateValue(valueWidth, title,
		(position) => new BlockFillBar(position, valueWidth, style));
	}
}