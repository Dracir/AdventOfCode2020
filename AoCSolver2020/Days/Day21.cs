using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day21 : DayBase
{

	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
		Console.Header.ReserveLines(0);
	}

	//-----------------------------------------------------------------

	public override void SetUpConsolePart2()
	{
		Console.Header.ReserveLines(0);
	}

	//-----------------------------------------------------------------

	public override void CleanUp()
	{
	}

	//-----------------------------------------------------------------

	public override bool Equals(object? obj) => base.Equals(obj);
	public override int GetHashCode() => base.GetHashCode();

	//-----------------------------------------------------------------

	public override long Part1(string input)
	{
		var foods = ParseFoods(input);
		var allAllergens = foods.SelectMany(x => x.Allergens).Distinct();
		var allIngredients = foods.SelectMany(x => x.Ingredients).Distinct();
		
		return 0;
	}

	private List<Food> ParseFoods(string input)
	{
		var foods = new List<Food>();

		foreach (var item in input.Split("\n"))
		{
			var splited = item.Split(" (contains ");
			var ingredients = splited[0].Split(" ");
			var allergens = splited[1].Trim().TrimEnd(')').Split(",").Select(x => x.Trim()).ToArray();
			foods.Add(new Food(ingredients, allergens));
		}


		return foods;
	}

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		return 0;
	}

}

public class Food
{
	public string[] Ingredients;
	public string[] Allergens;

	public Food(string[] ingredients, string[] allergens)
	{
		Ingredients = ingredients;
		Allergens = allergens;
	}
}
