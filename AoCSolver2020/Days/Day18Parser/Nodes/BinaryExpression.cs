
using System.Collections.Generic;

public class BinaryExpressionSyntax : ExpressionSyntax
{
	public ExpressionSyntax LeftExpression;
	public SyntaxToken Operator;
	public ExpressionSyntax RightExpression;

	public override SyntaxKind Kind => SyntaxKind.BinaryExpression;


	public BinaryExpressionSyntax(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
	{
		LeftExpression = left;
		Operator = operatorToken;
		RightExpression = right;
	}

	public override IEnumerable<SyntaxNode> GetChildren()
	{
		yield return LeftExpression;
		yield return Operator;
		yield return RightExpression;
	}
}