
using System.Collections.Generic;
using System.Linq;

public abstract class Rule
{
	public int Id;
	public Rule(int id)
	{
		Id = id;
	}
}

public class SequenceRule : Rule
{
	public SequenceRule(List<int> ruleIndex,int id):base(id)
	{
		Rules = ruleIndex;
	}
	public List<int> Rules { get; }

	public override string ToString() => $"Squence({string.Join(",", Rules.Select(x => x.ToString()))})";
}

public class ValueRule : Rule
{
	public ValueRule(char value,int id):base(id)
	{
		Value = value;
	}
	public char Value { get; }
	public override string ToString() => $"Value({Value})";
}

public class ChoiceRule : Rule
{

	public ChoiceRule(List<Rule> ruleChoices,int id):base(id)
	{
		RuleChoices = ruleChoices;
	}

	public List<Rule> RuleChoices { get; }

	public override string ToString() => $"ChoiceRule({string.Join(", ", RuleChoices.Select(x => x.ToString()))})";
}