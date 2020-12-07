using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text.RegularExpressions;


public static class NodeUtils
{
	public static int NodesThatContainKey(NodeGroup group, string key)
	{
		return group.Nodes.Count(keyValue => NodeContainsKey(group, keyValue.Value, key));
	}

	public static bool NodeContainsKey(NodeGroup group, Node node, string key)
	{
		foreach (var child in node.Children)
		{
			if (child.Name == key)
				return true;
			else if (NodeContainsKey(group, group.Nodes[child.Name], key))
				return true;
		}
		return false;
	}


	public static int CountQuantityOfChildren(NodeGroup group, string key)
	{
		int amount = 1;
		foreach (var child in group.Nodes[key].Children)
		{
			var childNode = group.Nodes[child.Name];
			if (childNode.Children.Count == 0)
				amount += childNode.Quantity;
			else
				amount += childNode.Quantity * CountQuantityOfChildren(group, child.Name);
		}

		return amount;
	}
}