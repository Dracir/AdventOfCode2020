using System;
using System.Threading;

namespace AoC2020
{
	class Program
	{
		static void Main(string[] args)
		{
			ConsoleManager.SetFullScreen();
			ConsoleManager.Header.SetTitle(1, "Cryostasis", 1);
			ConsoleManager.Header.ReserveLines(2);
			var anumber = ConsoleManager.Header.CreateHeaderValue(5, "A Number: ");

			Console.WriteLine("Hello World");
			anumber.SetValue(10);
			var aFloat = ConsoleManager.Header.CreateHeaderValue(5, "A Float: ");
			Console.WriteLine("▃ ╭ ▟ ╈");

			for (int i = 0; i < 10; i++)
			{
				Thread.Sleep(1000);
				anumber.SetValue(i * 20);
				Console.WriteLine("On change pour " + i * 20);
			}

			Console.Read();
		}
	}
}
