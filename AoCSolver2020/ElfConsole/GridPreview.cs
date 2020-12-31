using System;
using System.Collections.Generic;


public class GridPreview<T>
{
	public RectInt Viewport;
	public IGrid<T> Grid;
	private Func<T, char> _getTilePreview;
	public Func<T, ConsoleColor> GetTileColor;
	public char EmptyChar = ' ';
	public Point Offset;

	private int GridUsedWidth => Grid == null ? 0 : Grid.UsedWidth;
	private int GridUsedHeight => Grid == null ? 0 : Grid.UsedHeight;

	private int GridPreviewWidth => Math.Min(GridUsedWidth, Viewport.Width);
	private int GridPreviewHeight => Math.Min(GridUsedHeight, Viewport.Height);

	public GridPreview(Func<T, char> getTilePreview, RectInt viewport)
	{
		_getTilePreview = getTilePreview;
		Viewport = viewport;
	}

	public void Update()
	{
		var p = BetterConsole.Position;
		Console.ForegroundColor = ConsoleManager.Skin.HeaderValueColor;

		if (Grid != null)
			for (int y = 0; y < GridPreviewHeight; y++)
			{
				if (Grid.YInBound(y + Offset.Y))
				{
					if (GetTileColor == null)
					{

						var line = "";
						for (int x = 0; x < GridPreviewWidth; x++)
						{
							if (Grid.XInBound(x + Offset.X))
								line += _getTilePreview(Grid[x + Offset.X, y + Offset.Y]);
							else
								line += EmptyChar;
						}
						BetterConsole.WriteAt(line, Viewport.X, y + Viewport.Y);
					}
					else
					{
						for (int x = 0; x < GridPreviewWidth; x++)
						{

							if (Grid.XInBound(x + Offset.X))
							{
								var tile = Grid[x + Offset.X, y + Offset.Y];
								Console.ForegroundColor = GetTileColor(tile);
								BetterConsole.WriteAt(_getTilePreview(tile), x + Viewport.X, y + Viewport.Y);
							}
							else
								BetterConsole.WriteAt(EmptyChar, x + Viewport.X, y + Viewport.Y);
						}
					}
				}
				else
				{
					BetterConsole.WriteAt("", Viewport.X, y + Viewport.Y);
				}

			}

		Console.ResetColor();

		BetterConsole.Position = p;
	}
}