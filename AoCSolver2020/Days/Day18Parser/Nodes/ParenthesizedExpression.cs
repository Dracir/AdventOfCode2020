
using System.Collections.Generic;

public class ParenthesizedExpression : ExpressionSyntax
{
	public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;

	public SyntaxToken OpenParenthesis { get; }
	public ExpressionSyntax Expression { get; }
	public SyntaxToken CloseParenthesis { get; }

	public ParenthesizedExpression(SyntaxToken openParenthesis, ExpressionSyntax expression, SyntaxToken closeParenthesis)
	{
		OpenParenthesis = openParenthesis;
		Expression = expression;
		CloseParenthesis = closeParenthesis;
	}

	public override IEnumerable<SyntaxNode> GetChildren()
	{
		yield return OpenParenthesis;
		yield return Expression;
		yield return CloseParenthesis;
	}
}