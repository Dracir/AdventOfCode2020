using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public static class InputParser
{
	public static int[] ListOfInts(string input)
	{
		if (input.Contains("\n"))
			return ListOfInts(input, '\n');
		else
			return ListOfInts(input, ',');
	}

	public static int[] ListOfDigitNoSeparator(string input) => input.Select(x => int.Parse(x.ToString())).ToArray();
	public static int[] ListOfInts(string input, char separator) => input.Split(separator).Select(x => int.Parse(x)).ToArray();

	public static long[] ListOfLongs(string input, char separator) => input.Split(separator).Select(x => long.Parse(x)).ToArray();

	/* a range in the format of "number-number" for exemple 130254-678275*/
	public static RangeInt ParseRangeInt(string input)
	{
		var split = input.Split('-');
		return new RangeInt(Int32.Parse(split[0]), Int32.Parse(split[1]));
	}


	public static bool[,] ParseBoolGrid(string input, char separator, char trueCharacter)
	{
		var lines = input.Split(separator);
		var grid = new bool[lines.Length, lines[0].Length];

		for (int y = 0; y < lines.Length; y++)
			for (int x = 0; x < lines[y].Length; x++)
				grid[y, x] = lines[y][x] == trueCharacter;

		return grid;
	}



	public static Tree ReadTree(string input, char lineSeparator, char linkSeparator)
	{
		var outputNodes = new List<TreeNode>();
		var comNode = new TreeNode("COM");

		var nodeDick = new Dictionary<string, TreeNode>();
		var links = input.Split(lineSeparator).Select(x =>
		{
			var split = x.Split(linkSeparator);
			return (split[0], split[1]);
		}).ToList();

		outputNodes.Add(comNode);
		nodeDick.Add("COM", comNode);

		//Debug.Log(string.Join(",", links.Select(x => x.Item1 + " ) " + x.Item2).ToArray()));
		foreach (var link in links)
		{
			var node = new TreeNode(link.Item2);
			outputNodes.Add(node);
			nodeDick.Add(link.Item2, node);
		}

		//foreach (var item in nodeDick)
		//	Debug.Log($"Key: {item.Key}, Value: {item.Value}");

		//Debug.Log(string.Join(",", outputNodes.Select(x => x.Name).ToArray()));

		foreach (var link in links)
		{
			var parentId = link.Item1;
			if (!nodeDick.ContainsKey(parentId))
			{
				//Debug.Log($"Unknown item {parentId}");
				continue;
			}
			//else
			//Debug.Log($"Trouvé item {parentId}");

			//Debug.Log(string.Join(",", nodeDick.Select(x => x.Key).ToArray()));
			var parent = nodeDick[parentId];
			var child = nodeDick[link.Item2];
			child.Parent = parent;
			parent.Children.Add(child);
		}

		return new Tree(nodeDick, comNode);
	}


	public struct Tree
	{
		public Dictionary<string, TreeNode> Nodes;
		public TreeNode Root;

		public Tree(Dictionary<string, TreeNode> nodes, TreeNode root)
		{
			Nodes = nodes;
			Root = root;
		}
	}

	public class TreeNode
	{
		public string Name;
		public TreeNode? Parent;
		public List<TreeNode> Children = new List<TreeNode>();

		public TreeNode(string name)
		{
			this.Name = name;
		}
	}
}