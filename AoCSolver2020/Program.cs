using System;
using System.Threading;

namespace AoC2020
{
	class Program
	{
		static Random RandomGen = new Random();
		static void Main(string[] args)
		{
			//YearFileCreator.CreateYear();
			TestConsole();
		}

		private static void TestConsole()
		{
			ConsoleManager.SetFullScreen();
			ConsoleManager.Header.SetTitle(1, "Cryostasis", 1);
			ConsoleManager.Header.ReserveLines(2);
			var itteration = ConsoleManager.Header.CreateFormatedValue(5, "Itteration: ");

			Console.WriteLine("Hello World");
			itteration.SetValue(10);
			var aFloat = ConsoleManager.Header.CreateFormatedValue(6, "Float: ");
			var aFormatedFloat = ConsoleManager.Header.CreateFormatedValue(6, "Formated Float: ", "F3");

			var percent1 = ConsoleManager.Header.CreateFormatedValue(6, "Percent: ");
			var percent2 = ConsoleManager.Header.CreateBlockValue(2, "Percent: ", ValueToUTFBars.Styles.Horizontal);
			var percent3 = ConsoleManager.Header.CreateBlockValue(5, "Percent: ", ValueToUTFBars.Styles.Vertical);
			var percent4 = ConsoleManager.Header.CreateBlockValue(10, "Percent: ", ValueToUTFBars.Styles.Shades);
			var percent5 = ConsoleManager.Header.CreateBlockValue(15, "Percent: ", ValueToUTFBars.Styles.Circle);
			var percent6 = ConsoleManager.Header.CreateBlockValue(1, "Percent: ", ValueToUTFBars.Styles.CenteredVerticalBar);

			Console.WriteLine("▃ ╭ ▟ ╈");
			Console.WriteLine(DaysInputs.D1);
			;

			for (int i = 0; i < 10; i++)
			{
				Thread.Sleep(1000);
				itteration.SetValue(i);
				var f = (float)RandomGen.NextDouble();
				aFloat.SetValue(f);
				aFormatedFloat.SetValue(f);
				Console.WriteLine("Itteration " + i);


				var percent = i * 1f / 9f;
				percent1.SetValue(percent);
				percent2.SetValue(percent);
				percent3.SetValue(percent);
				percent4.SetValue(percent);
				percent5.SetValue(percent);
				percent6.SetValue(percent);
			}

			Console.Read();
		}
	}
}
