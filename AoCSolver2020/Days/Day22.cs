using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console = ConsoleManager;


public class Day22 : DayBase
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

	public override bool Equals(object? obj) => base.Equals(obj);
	public override int GetHashCode() => base.GetHashCode();

	//-----------------------------------------------------------------

	public override long Part1(string input)
	{
		var playerCards = ParseInput(input);

		var p1Deck = new List<int>();
		var p2Deck = new List<int>();
		p1Deck.AddRange(playerCards.P1);
		p2Deck.AddRange(playerCards.P2);

		int round = 1;
		while (p1Deck.Count != 0 && p2Deck.Count != 0)
		{
			var topP1 = p1Deck.First();
			p1Deck.RemoveAt(0);
			var topP2 = p2Deck.First();
			p2Deck.RemoveAt(0);

			if (topP1 > topP2)
			{
				p1Deck.Add(topP1);
				p1Deck.Add(topP2);
			}
			else
			{
				p2Deck.Add(topP2);
				p2Deck.Add(topP1);
			}

			var outputStr = $"--Round {round} -\n"
				+ $"Player 1's deck: {p1Deck.GetPrintStr()}\n"
				+ $"Player 2's deck: {p2Deck.GetPrintStr()}\n"
				+ $"Player 1 plays: {topP1}\n"
				+ $"Player 2 plays: {topP2}\n"
				+ $"Player {((topP1 > topP2) ? "1" : "2")} wins the round!\n\n";
			//Console.WriteLine(outputStr);
		}
		int score = 0;
		var winnerDeck = (p1Deck.Count == 0) ? p2Deck : p1Deck;
		for (int i = 0; i < winnerDeck.Count; i++)
		{
			score += (winnerDeck.Count - i) * winnerDeck[i];
		}

		return score;
	}

	private (int[] P1, int[] P2) ParseInput(string input)
	{
		var splitted = input.Replace("\r", "").Split("\n\n");

		var p1 = splitted[0].Split("\n").Skip(1).Select(x => Int32.Parse(x)).ToArray();
		var p2 = splitted[1].Split("\n").Skip(1).Select(x => Int32.Parse(x)).ToArray();
		return (p1, p2);
	}

	//-----------------------------------------------------------------
	// 8839 your answer is too low.
	public override long Part2(string input)
	{
		var playerCards = ParseInput(input);

		var p1Deck = new List<int>();
		var p2Deck = new List<int>();
		p1Deck.AddRange(playerCards.P1);
		p2Deck.AddRange(playerCards.P2);

		PlayGame(p1Deck, p2Deck);

		int score = 0;
		var winnerDeck = (p1Deck.Count == 0) ? p2Deck : p1Deck;
		for (int i = 0; i < winnerDeck.Count; i++)
		{
			score += (winnerDeck.Count - i) * winnerDeck[i];
		}

		return score;
	}

	private static int PlayGame(List<int> p1Deck, List<int> p2Deck)
	{
		var p1DeckHistory = new List<int[]>();
		var p2DeckHistory = new List<int[]>();
		p1DeckHistory.Add(p1Deck.ToArray());
		p2DeckHistory.Add(p2Deck.ToArray());
		var comparer = new ArrayByValueComparer();

		int round = 1;
		while (p1Deck.Count != 0 && p2Deck.Count != 0)
		{
			var topP1 = p1Deck.First();
			p1Deck.RemoveAt(0);
			var topP2 = p2Deck.First();
			p2Deck.RemoveAt(0);

			if (p1DeckHistory.Contains(p1Deck.ToArray(), comparer)
					|| p2DeckHistory.Contains(p2Deck.ToArray(), comparer))
			{
				Console.WriteLine("SAME!");
				return 1;
			}

			p1DeckHistory.Add(p1Deck.ToArray());
			p2DeckHistory.Add(p2Deck.ToArray());

			var playerWinner = 0;

			if (p1Deck.Count >= topP1 && p2Deck.Count >= topP2)
				playerWinner = PlayGame(p1Deck.ToArray().ToList(), p2Deck.ToArray().ToList());
			else
				playerWinner = (topP1 > topP2) ? 1 : 2;

			if (playerWinner == 1)
			{
				p1Deck.Add(topP1);
				p1Deck.Add(topP2);
			}
			else
			{
				p2Deck.Add(topP2);
				p2Deck.Add(topP1);
			}
			p1DeckHistory.Add(p1Deck.ToArray());
			p2DeckHistory.Add(p2Deck.ToArray());

			var outputStr = $"--Round {round} -\n"
				+ $"Player 1's deck: {p1Deck.GetPrintStr()}\n"
				+ $"Player 2's deck: {p2Deck.GetPrintStr()}\n"
				+ $"Player 1 plays: {topP1}\n"
				+ $"Player 2 plays: {topP2}\n"
				+ $"Player {((topP1 > topP2) ? "1" : "2")} wins the round!\n\n";
			//Console.WriteLine(outputStr);
		}
		return (p1Deck.Count == 0) ? 2 : 1;
	}
}
