using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Parser
{
	private readonly SyntaxToken[] _tokens;
	private int _position;

	public Parser(string text)
	{
		var tokens = new List<SyntaxToken>();
		var lexer = new Lexer(text);
		SyntaxToken token;
		do
		{
			token = lexer.NextToken();
			if (token.Kind != SyntaxKind.WhiteSpace && token.Kind != SyntaxKind.BadToken)
				tokens.Add(token);
		} while (token.Kind != SyntaxKind.EndOfFileToken);

		_tokens = tokens.ToArray();
	}

	private SyntaxToken Peek(int offset)
	{
		var index = _position + offset;
		if (index >= _tokens.Length)
			return _tokens[_tokens.Length - 1];
		else
			return _tokens[index];
	}
	private SyntaxToken Current => Peek(0);

	private SyntaxToken NextToken()
	{
		var current = Current;
		_position++;
		return current;
	}

	private SyntaxToken Match(SyntaxKind kind)
	{
		if (Current.Kind == kind)
			return NextToken();
		return new SyntaxToken(kind, Current.Position, null, null);
	}

	public ExpressionSyntax Parse(bool plusStarSamePrecedence)
	{
		if (plusStarSamePrecedence)
			return ParseFactor();
		else
			return ParseExpression();
	}

	private ExpressionSyntax ParseExpression()
	{
		var left = ParsePrimaryExpression(false);
		while (Current.Kind == SyntaxKind.PlusToken)
		{
			var operatorToken = NextToken();
			var right = ParsePrimaryExpression(false);
			left = new BinaryExpressionSyntax(left, operatorToken, right);
		}
		return left;
	}

	private ExpressionSyntax ParseAdition()
	{
		var left = ParsePrimaryExpression(false);
		while (Current.Kind == SyntaxKind.PlusToken)
		{
			var operatorToken = NextToken();
			var right = ParsePrimaryExpression(false);
			left = new BinaryExpressionSyntax(left, operatorToken, right);
		}
		return left;
	}

	private ExpressionSyntax ParseFactor()
	{
		var left = ParseAdition();
		while (Current.Kind == SyntaxKind.StarToken)
		{
			var operatorToken = NextToken();
			var right = ParseAdition();
			left = new BinaryExpressionSyntax(left, operatorToken, right);
		}
		return left;
	}

	private ExpressionSyntax ParsePrimaryExpression(bool plusStarSamePrecedence)
	{
		if (Current.Kind == SyntaxKind.OpenParenthesis)
		{
			var left = NextToken();
			var expression = Parse(plusStarSamePrecedence);
			var right = NextToken();
			return new ParenthesizedExpression(left, expression, right);
		}

		var numberToken = Match(SyntaxKind.NumberToken);
		return new NumberExpressionSyntax(numberToken);
	}
}