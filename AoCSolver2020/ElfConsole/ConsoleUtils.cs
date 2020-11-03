
using System;
using System.Runtime.InteropServices; //for P/Invoke DLLImport

public static class ConsoleUtils
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
}
