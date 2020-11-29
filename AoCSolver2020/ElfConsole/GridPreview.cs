using System;
using System.Collections.Generic;


public class GridPreview<T>
{

	private IGrid<T> _grid;
	private Func<T, char> _getTilePreview;

	public GridPreview(IGrid<T> grid, Func<T, char> getTilePreview)
	{
		_grid = grid;
		_getTilePreview = getTilePreview;
	}

}