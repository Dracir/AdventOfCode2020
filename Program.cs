using System;
using System.Threading;

namespace AoC2020
{
	class Program
	{
		static Random RandomGen = new Random();
		static void Main(string[] args)
		{
			ConsoleManager.SetFullScreen();
			ConsoleManager.Header.SetTitle(1, "Cryostasis", 1);
			ConsoleManager.Header.ReserveLines(2);
			var itteration = ConsoleManager.Header.CreateHeaderValue(5, "Itteration: ");

			Console.WriteLine("Hello World");
			itteration.SetValue(10);
			var aFloat = ConsoleManager.Header.CreateHeaderValue(6, "Float: ");
			var aFormatedFloat = ConsoleManager.Header.CreateHeaderValue(6, "Formated Float: ", "F3");
			Console.WriteLine("▃ ╭ ▟ ╈");

			for (int i = 0; i < 10; i++)
			{
				Thread.Sleep(1000);
				itteration.SetValue(i);
				var f = (float)RandomGen.NextDouble();
				aFloat.SetValue(f);
				aFormatedFloat.SetValue(f);
				Console.WriteLine("Itteration " + i);

			}

			Console.Read();
		}
	}
}
