using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SyntaxToken : SyntaxNode
{
	private SyntaxKind _kind;
	public override SyntaxKind Kind => _kind;
	public readonly int Position;
	public readonly string Text;
	public readonly object Value;

	public SyntaxToken(SyntaxKind kind, int position, string text, object value)
	{
		_kind = kind;
		Position = position;
		Text = text;
		Value = value;
	}

	public override IEnumerable<SyntaxNode> GetChildren()
	{
		return Enumerable.Empty<SyntaxNode>();
	}
}