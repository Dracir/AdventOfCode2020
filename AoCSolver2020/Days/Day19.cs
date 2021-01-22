using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day19 : DayBase
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

	(Dictionary<int, Rule> Rules, List<string> Messages) ParseInput(string input)
	{
		var splited = input.Split("\n\n");
		var rules = new Dictionary<int, Rule>();

		foreach (var ruleLine in splited[0].Split("\n"))
		{
			var indexRuleSplitted = ruleLine.Split(":");
			var index = int.Parse(indexRuleSplitted[0].Trim());
			var rule = CreateRuleFromInputLine(index, indexRuleSplitted[1]);
			rules.Add(index, rule);
		}

		return (rules, splited[1].Split("\n").ToList());
	}

	private static Rule CreateRuleFromInputLine(int index, string ruleStr)
	{
		if (ruleStr.Contains('|'))
		{
			var ruleStrPipeSplited = ruleStr.Trim().Split("|");
			var rulechoices = ruleStrPipeSplited
				.Select(x => x.Trim().Split(" ").Select(subRuleIndexes => int.Parse(subRuleIndexes)).ToList())
				.Select(x => new SequenceRule(x, index))
				.ToList<Rule>();
			return new ChoiceRule(rulechoices, index);
		}
		else if (ruleStr.Contains('"'))
		{
			var v = ruleStr.Substring(ruleStr.IndexOf('"') + 1)[0];
			return new ValueRule(v, index);
		}
		else
		{
			var sequence = ruleStr.Trim().Split(" ").Select(x => int.Parse(x)).ToList();
			return new SequenceRule(sequence, index);
		}
	}

	//-----------------------------------------------------------------

	public override long Part1(string input)
	{
		var (rules, messages) = ParseInput(input);

		var nbMatchs = 0;
		foreach (var message in messages)
		{
			if (Matches(message, rules[0], rules))
				nbMatchs++;
		}
		return nbMatchs;
	}

	private bool Matches(string message, Rule rule, Dictionary<int, Rule> rules)
	{
		var ruleDepts = new Dictionary<int, int>();
		foreach (var r in rules)
			ruleDepts.Add(r.Key, 0);

		var consumed = Consume(message, rule, rules, ruleDepts);
		return consumed.Any(consumption => consumption == message.Length);
	}

	private IEnumerable<int> Consume(string message, Rule rule, Dictionary<int, Rule> rules, Dictionary<int, int> ruleDepts)
	{
		var newDictionary = ruleDepts.ToDictionary(entry => entry.Key, entry => entry.Value);
		newDictionary[rule.Id]++;

		//Console.WriteLine($"{message} at {depth}, {rule}");

		if (ruleDepts[rule.Id] >= 100 * message.Length)
			return new int[] { 0 };
		if (rule is ChoiceRule choice)
			return ConsumeChoice(message, choice, rules, newDictionary);
		else if (rule is SequenceRule sequence)
			return ConsumeSequence(message, sequence, rules, newDictionary);
		else if (rule is ValueRule v)
			return (message[0] == v.Value) ? new int[] { 1 } : new int[] { 0 };
		else return new int[] { 0 };

	}

	private IEnumerable<int> ConsumeChoice(string message, ChoiceRule choice, Dictionary<int, Rule> rules, Dictionary<int, int> ruleDepts)
	{
		var consumations = new List<int>();
		foreach (var subRule in choice.RuleChoices)
		{
			var consume = Consume(message, subRule, rules, ruleDepts);
			if (consume.Count() != 0)
				consumations.AddRange(consume.Where(x => x != 0));
		}
		return consumations;
	}

	private IEnumerable<int> ConsumeSequence(string message, SequenceRule sequence, Dictionary<int, Rule> rules, Dictionary<int, int> ruleDepts)
	{
		if (sequence.Rules.Count > 10 * message.Length)
		{
			Console.WriteLine("trop fond");
			return new int[] { 0 };
		}

		else
		{
			var consumations = new List<int>();
			consumations.Add(0);
			foreach (var ruleIndex in sequence.Rules)
			{
				var newConsumptions = new List<int>();
				var subRule = rules[ruleIndex];
				foreach (var consumeCheck in consumations)
				{
					var subMessage = message.Substring(consumeCheck);
					var subConsumptions = Consume(subMessage, subRule, rules, ruleDepts)
						.Where(x => x != 0);
					newConsumptions = subConsumptions.Select(x => x + consumeCheck).ToList();
				}
				consumations = newConsumptions;
			}
			return consumations;
		}
	}

	//-----------------------------------------------------------------

	public override long Part2(string input)
	{
		var (rules, messages) = ParseInput(input);

		rules[8] = CreateRuleFromInputLine(8, "42 | 42 8");
		rules[11] = CreateRuleFromInputLine(11, "42 31 | 42 11 31");
		//PrettyPrintRule(rules, rules[0]);

		var nbMatchs = 0;
		foreach (var message in messages)
		{
			if (Matches(message, rules[0], rules))
				nbMatchs++;
		}
		return nbMatchs;
	}

	public static void PrettyPrintRule(Dictionary<int, Rule> rules, Rule rule, string indent = "", bool isLast = true)
	{
		if (indent.Length > 12)
			return;
		var marker = isLast ? "└──" : "├──";

		Console.WriteLine($"{indent}{marker}{rule.Id.ToString()}- {rule.ToString()} ");

		indent += isLast ? "    " : "|   ";

		if (rule is ChoiceRule c)
		{
			var last = c.RuleChoices.LastOrDefault();
			foreach (var child in c.RuleChoices)
				PrettyPrintRule(rules, child, indent, child == last);
		}
		else if (rule is SequenceRule s)
		{
			var last = s.Rules.LastOrDefault();
			foreach (var child in s.Rules)
				PrettyPrintRule(rules, rules[child], indent, child == last);
		}

	}

}
