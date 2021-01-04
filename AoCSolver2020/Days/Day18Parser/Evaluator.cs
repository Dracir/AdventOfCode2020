

using System;

public class Evaluator
{
	private readonly ExpressionSyntax _root;

	public Evaluator(ExpressionSyntax root)
	{
		this._root = root;
	}

	public long Evaluate()
	{
		return EvaluateExpression(_root);
	}

	private long EvaluateExpression(ExpressionSyntax root)
	{
		if (root is NumberExpressionSyntax n)
			return (int)n.NumberToken.Value;
		else if (root is BinaryExpressionSyntax b)
		{
			var left = EvaluateExpression(b.LeftExpression);
			var right = EvaluateExpression(b.RightExpression);
			if (b.Operator.Kind == SyntaxKind.PlusToken)
				return left + right;
			else if (b.Operator.Kind == SyntaxKind.StarToken)
				return left * right;
			else
				return 0;
		}
		else if (root is ParenthesizedExpression p)
			return EvaluateExpression(p.Expression);
		else
			return 0;

	}
}