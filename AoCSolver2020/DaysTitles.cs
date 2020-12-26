public static class DaysTitles
{
	public static string Day1 => "Report Repair";
	public static string Day2 => "Password Philosophy";
	public static string Day3 => "Toboggan Trajectory";
	public static string Day4 => "Passport Processing";
	public static string Day5 => "Binary Boarding";
	public static string Day6 => "Custom Customs";
	public static string Day7 => "Handy Haversacks";
	public static string Day8 => "Handheld Halting";
	public static string Day9 => "Encoding Error";
	public static string Day10 => "Adapter Array";
	public static string Day11 => "Seating System";
	public static string Day12 => "Rain Risk";
	public static string Day13 => "Shuttle Search";
	public static string Day14 => "Docking Data";
	public static string Day15 => "Rambunctious Recitation";
	public static string Day16 => "Ticket Translation";
	public static string Day17 => "Conway Cubes";
	public static string Day18 => "Operation Order";
	public static string Day19 => "Monster Messages";
	public static string Day20 => "Jurassic Jigsaw";
	public static string Day21 => "Allergen Assessment";
	public static string Day22 => "Crab Combat";
	public static string Day23 => "Crab Cups";
	public static string Day24 => "";
	public static string Day25 => "";

	public static string GetDayTitle(int day) => day switch
	{
		1 => Day1,
		2 => Day2,
		3 => Day3,
		4 => Day4,
		5 => Day5,
		6 => Day6,
		7 => Day7,
		8 => Day8,
		9 => Day9,
		10 => Day10,
		11 => Day11,
		12 => Day12,
		13 => Day13,
		14 => Day14,
		15 => Day15,
		16 => Day16,
		17 => Day17,
		18 => Day18,
		19 => Day19,
		20 => Day20,
		21 => Day21,
		22 => Day22,
		23 => Day23,
		24 => Day24,
		25 => Day25,
		_ => "Test",
	};
}