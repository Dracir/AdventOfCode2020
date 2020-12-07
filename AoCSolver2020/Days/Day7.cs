using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day7 : DayBase
{

	//-----------------------------------------------------------------

	public override void SetUpConsolePart1()
	{
		Console.Header.ReserveLines(1);
	}

	//-----------------------------------------------------------------

	public override void SetUpConsolePart2()
	{
		Console.Header.ReserveLines(1);
	}

	//-----------------------------------------------------------------

	public override void CleanUp()
	{
	}

	//-----------------------------------------------------------------

	public override bool Equals(object? obj)
	{
		return base.Equals(obj);
	}

	//-----------------------------------------------------------------

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	//-----------------------------------------------------------------

	public override long Part1(string input)
	{
		var dic = InputParser.ParseDictionnaryNodes(input, "contain", ", ", @"(\d+)", @"([a-z]+ [a-z]+) bag", "no other bags");
		return NodeUtils.NodesThatContainKey(dic, "shiny gold");
	}

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		var dic = InputParser.ParseDictionnaryNodes(input, "contain", ", ", @"(\d+)", @"([a-z]+ [a-z]+) bag", "no other bags");
		return CountQuantityOfChildren(dic, "shiny gold") - 1;
	}

	public int indentation = 0;

	public int CountQuantityOfChildren(NodeGroup group, string key)
	{
		int amount = 1;
		var node = group.Nodes[key];
		var leftPading = new String(' ', indentation * 2);
		indentation++;
		foreach (var child in node.Children)
		{
			//Console.WriteLine($"{leftPading}{key} -> {child.Name}");
			var childNode = group.Nodes[child.Name];
			if (childNode.Children.Count == 0)
				amount += child.Quantity;
			else
				amount += child.Quantity * CountQuantityOfChildren(group, child.Name);
		}
		Console.WriteLine($"{leftPading}- {key} with {string.Join(",", node.Children.Select(x => $"{x.Quantity} {x.Name}"))} gives {amount}");
		//	Console.WriteLine($"{leftPading}<- {key}");

		indentation--;
		return amount;
	}

}
