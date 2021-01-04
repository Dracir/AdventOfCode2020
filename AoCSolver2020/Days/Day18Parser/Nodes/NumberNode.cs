
using System.Collections.Generic;

public class NumberExpressionSyntax : ExpressionSyntax
{
	public SyntaxToken NumberToken;
	public override SyntaxKind Kind => SyntaxKind.NumberToken;

	public NumberExpressionSyntax(SyntaxToken numberToken)
	{
		NumberToken = numberToken;
	}

	public override IEnumerable<SyntaxNode> GetChildren()
	{
		yield return NumberToken;
	}
}