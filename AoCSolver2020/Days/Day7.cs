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
		var nodeGroup = InputParser.ParseDictionnaryNodes(input, "contain", ", ", @"(\d+)", @"([a-z]+ [a-z]+) bag", "no other bags");
		return NodeUtils.NodesThatContainKey(nodeGroup, "shiny gold");
	}

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		var nodeGroup = InputParser.ParseDictionnaryNodes(input, "contain", ", ", @"(\d+)", @"([a-z]+ [a-z]+) bag", "no other bags");
		var shinyBagTree = NodeUtils.MakeTreeOfNode(nodeGroup, "shiny gold");
		var qty = NodeUtils.CountTreeQuantities(shinyBagTree) - 1;
		DrawNode(shinyBagTree);
		return qty;
	}


	public int indentation = 0;
	private void DrawNode(Node node)
	{
		var leftPading = new String('-', indentation * 2);
		var s = node.Quantity > 1 ? "s" : "";
		Console.WriteLine($"{leftPading}{node.Quantity} {node.Name} bag{s}");
		indentation++;
		foreach (var child in node.Children)
			DrawNode(child);
		indentation--;
	}

}
