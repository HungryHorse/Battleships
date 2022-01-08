using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField]
    private BoardCell boardCellPrefab;
    private BoardCell BoardCellPrefab => boardCellPrefab;

    public BoardCell[,] Board;

    public Vector2Int Dimensions { get; private set; }

    private void Start()
    {
        Load(new Vector2Int(11, 11));
    }

    public void Load(Vector2Int dimensions)
    {
        Dimensions = dimensions;
        BuildBoard();
        PopulateNeighbours();
    }

    private void BuildBoard()
    {
        Board = new BoardCell[Dimensions.x, Dimensions.y];
        for (int x = 0; x < Dimensions.x; x++)
        {
            for (int y = 0; y < Dimensions.y; y++)
            {
                Board[x, y] = CreateGridCell(x, y);
            }
        }
    }

    private BoardCell CreateGridCell(int x, int y)
    {
        BoardCell cell = Instantiate(BoardCellPrefab, transform);
        cell.name = $"Cell ({x},{y})";
        cell.Init(new Vector2Int(x, y));
        Vector3 modelScale = Vector3.one * 55;
        int halfGridSize = Dimensions.x / 2;
        Vector2Int offsetPosition = new Vector2Int(x - halfGridSize, (y - halfGridSize) * -1);
        Vector2 scaledOffsetPosition = Vector2.Scale(offsetPosition, new Vector2(modelScale.x, modelScale.y));
        cell.transform.localPosition = new Vector3(transform.position.x + scaledOffsetPosition.x, transform.position.y + scaledOffsetPosition.y, 0);
        return cell;
    }

    private void PopulateNeighbours()
    {
        for (int x = 0; x < Dimensions.x; x++)
        {
            for (int y = 0; y < Dimensions.y; y++)
            {
                Board[x, y].Neighbours = new Dictionary<CardinalDirections, BoardCell>
                {
                    {CardinalDirections.North, y-1 >= 0 ? Board[x, y-1] : null},
                    {CardinalDirections.East, x+1 < Dimensions.x ? Board[x+1, y] : null},
                    {CardinalDirections.South, y+1 < Dimensions.y ? Board[x, y+1] : null},
                    {CardinalDirections.West, x-1 >= 0 ? Board[x-1, y] : null},
                };
            }
        }
    }
}
