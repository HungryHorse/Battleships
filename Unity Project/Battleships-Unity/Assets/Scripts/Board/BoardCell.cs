using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell : MonoBehaviour
{
    public Vector2Int Coordinates { get; private set; }

    public Dictionary<CardinalDirections, BoardCell> Neighbours { get; set; } = new Dictionary<CardinalDirections, BoardCell>();

    public CellStates CellState { get; set; }

    public void Init(Vector2Int coordinates)
    {
        Coordinates = coordinates;
    }
}
