using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day18 : DayBase
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

	public override bool Equals(object obj) => base.Equals(obj);
	public override int GetHashCode() => base.GetHashCode();

	//-----------------------------------------------------------------

	public override long Part1(string input)
	{
		var sum = 0L;
		var lines = input.Split("\n");
		foreach (var line in lines)
		{
			var value = Handle(line, lines.Length == 1, true);
			sum += value;
		}
		return sum;
	}

	private long Handle(string line, bool print, bool operatorSamePrecedence)
	{
		var parser = new Parser(line);
		var expression = parser.Parse(operatorSamePrecedence);
		if (print)
			PrettyPrint(expression, "");
		var evaluator = new Evaluator(expression);
		var result = evaluator.Evaluate();
		return result;
	}

	public static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
	{
		var marker = isLast ? "└──" : "├──";
		var value = "";
		if (node is SyntaxToken t)
		{
			value = t.Value == null ? "" : t.Value.ToString();
		}
		Console.WriteLine($"{indent}{marker}{node.Kind.ToString()} {value}");

		indent += isLast ? "    " : "|   ";

		var last = node.GetChildren().LastOrDefault();
		foreach (var child in node.GetChildren())
			PrettyPrint(child, indent, child == last);


	}

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		var sum = 0L;
		var lines = input.Split("\n");
		foreach (var line in lines)
		{
			var value = Handle(line, lines.Length == 1, false);
			sum += value;
		}
		return sum;
	}

}
