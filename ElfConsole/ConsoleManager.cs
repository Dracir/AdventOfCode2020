using System;
using System.IO;
using System.Collections.Generic; //for dictionary
using System.Runtime.InteropServices; //for P/Invoke DLLImport

public static class ConsoleManager
{
	/// <summary>
	/// Contains native methods imported as unmanaged code.
	/// </summary>
	internal static class DllImports
	{
		[DllImport("kernel32.dll", ExactSpelling = true)]
		internal static extern IntPtr GetConsoleWindow();
		internal static IntPtr ThisConsole = GetConsoleWindow();
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		internal const int HIDE = 0;
		internal const int MAXIMIZE = 3;
		internal const int MINIMIZE = 6;
		internal const int RESTORE = 9;

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleOutputCP(uint wCodePageID);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetConsoleCP(uint wCodePageID);
	}

	public static void SetFullScreen()
	{
		DllImports.SetConsoleOutputCP(65001);
		DllImports.SetConsoleCP(65001);
		Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
		DllImports.ShowWindow(DllImports.ThisConsole, DllImports.MAXIMIZE);
	}

	public static void Redraw() { }
	public static void SetTitle(int day, string title, int part)
	{
		var line = new String('═', BetterConsole.Width - 2);
		var titleText = $"║  Day {day}: {title} - Part {part}";

		BetterConsole.WriteAt($"╔{line}╗", 0);
		BetterConsole.WriteAt(titleText, 1);
		BetterConsole.WriteAt("║", BetterConsole.Width - 1, 1);
		BetterConsole.WriteAt($"╚{line}╝", 2);
	}


}