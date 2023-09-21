using System.Collections.Generic;
using UnityEngine;

public class GridContainter : Singleton<GridContainter>, IContainer
{
    public Grid Grid;
    public Vector2Int GridSize;
    public BoxCollider BoxCollider;
    [HideInInspector] public Vector2Int GridMin, GridMax;

    [SerializeField, Min(1)] private int minBonusWaitScore, maxBonusWaitScore;

    private readonly Dictionary<Vector2Int, Interactable> _interactablePlaces = new();
    private bool[,] _gridSpaces;
    private float _gridScaleMin;
    private Vector2 _previousPressPoint;
    private int _bonusCurrentWaitScore, _bonusWantedWaitScore;

    public bool GridSpaces(int x, int y)
    {
        x -= GridMin.x;
        y -= GridMin.y;
        return x >= 0 && x < GridSize.x && y >= 0 && y < GridSize.y && _gridSpaces[x, y];
    }

    public Transform Content => Grid.transform;

    protected override void Awake()
    {
        base.Awake();

        if (Grid == null) Grid = GetComponent<Grid>();
        SetupGridSpaces();
    }

    public void AddInteractable(Interactable interactable, Vector2 worldPosition)
    {
        Vector2Int cellPosition = GetCellPosition(worldPosition);
        switch (interactable)
        {
            case BlockSegment blockSegment:
                for (int i = 0; i < blockSegment.BlockPositions.Length; i++)
                {
                    Vector2Int blockPosition = blockSegment.BlockPositions[i];
                    Vector2Int space = new(cellPosition.x + blockPosition.x - GridMin.x, cellPosition.y + blockPosition.y - GridMin.y);
                    _gridSpaces[space.x, space.y] = false;
                }

                // Add score
                float filledPercentage = GetFilledSpaceAmount() / (float)_gridSpaces.Length;
                ScoreManager.Instance.IncreaseScore(blockSegment.PointAmount, filledPercentage);

                // Spawn bonus multiplier
                _bonusCurrentWaitScore += blockSegment.PointAmount;

                if (_bonusCurrentWaitScore >= _bonusWantedWaitScore)
                {
                    _bonusWantedWaitScore = Random.Range(minBonusWaitScore, maxBonusWaitScore);
                }
                break;
        }
        interactable.transform.position = Grid.CellToWorld((Vector3Int)cellPosition);
    }

    public void RemoveInteractable(Interactable interactable)
    {
        _interactablePlaces.Remove(GetCellPosition(interactable.transform.position));
        interactable.transform.SetParent(null);
        interactable.transform.localScale = Vector3.one;
    }

    [ContextMenu(nameof(SetupGridSpaces))]
    public void SetupGridSpaces()
    {
        _gridSpaces = new bool[GridSize.x, GridSize.y];
        GridMin = -new Vector2Int((GridSize.x - 1) / 2, (GridSize.y - 1) / 2);
        GridMax = new Vector2Int(GridSize.x / 2, GridSize.y / 2);

        for (int x = 0; x < GridSize.x; x++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                _gridSpaces[x, y] = true;
            }
        }
    }

    private int GetFilledSpaceAmount()
    {
        int amount = 0;
        for (int x = 0; x < GridSize.x; x++)
            for (int y = 0; y < GridSize.y; y++)
                if (!_gridSpaces[x, y])
                    amount++;
        return amount;
    }

    public Vector2Int GetCellPosition(Vector2 worldPosition)
    {
        Vector2Int cellPosition = (Vector2Int)Grid.WorldToCell(worldPosition);
        cellPosition.Clamp(GridMin, GridMax);
        return cellPosition;
    }

    public Vector2 GetGridPosition(Vector2 worldPosition) =>
        Grid.CellToWorld((Vector3Int)GetCellPosition(worldPosition));

    public bool IsInGrid(Vector2 worldPosition)
    {
        Vector2Int cellPosition = (Vector2Int)Grid.WorldToCell(worldPosition);
        return cellPosition.x >= GridMin.x && cellPosition.x <= GridMax.x
            && cellPosition.y >= GridMin.y && cellPosition.y <= GridMax.y;
    }

    public bool FitsInGrid(Vector2 worldPosition, Vector2Int size)
    {
        Vector2Int cellPosition = (Vector2Int)Grid.WorldToCell(worldPosition);
        return cellPosition.x >= GridMin.x && cellPosition.x <= GridMax.x - size.x
            && cellPosition.y >= GridMin.y && cellPosition.y <= GridMax.y - size.y;
    }

    public Vector2Int FitToGrid(Vector2 worldPosition, Vector2Int size)
    {
        Vector2Int cellPosition = (Vector2Int)Grid.WorldToCell(worldPosition);
        cellPosition = Vector2Int.Max(cellPosition, GridMin);
        cellPosition = Vector2Int.Min(cellPosition, GridMax - size + Vector2Int.one);
        return cellPosition;
    }

    public bool TryGetAvailableGridPosition(Vector2 worldPosition, out Vector2 gridPosition)
    {
        gridPosition = Vector2.zero;

        Vector2Int cellPosition = (Vector2Int)Grid.WorldToCell(worldPosition);
        cellPosition.Clamp(GridMin, GridMax);
        Vector2Int index = cellPosition - GridMin;

        if (_gridSpaces[index.x, index.y])
        {
            gridPosition = Grid.CellToWorld((Vector3Int)cellPosition);
            return true;
        }

        int largestDistance = Mathf.Max(index.x, index.y, _gridSpaces.GetLength(0) - index.x, _gridSpaces.GetLength(1) - index.y);

        if (TryFindClosestGridSpace(out Vector2Int closestSpace, index, largestDistance * 2))
        {
            gridPosition = Grid.CellToWorld((Vector3Int)(closestSpace + GridMin));
            return true;
        }

        return false;


        // Recursive function to find closest grid space. Check each space in a diamond shape where each step the radius increases
        bool TryFindClosestGridSpace(out Vector2Int closestSpace, Vector2Int center, int maxDistance, int distance = 1)
        {
            closestSpace = Vector2Int.zero;

            // Check if out of range
            if (distance > maxDistance)
                return false;

            // Find closest space in a diamond shape with increasing distance
            if (TryFindGridSpaceOnDiagonal(out closestSpace, Vector2Int.up, Vector2Int.right) ||
                TryFindGridSpaceOnDiagonal(out closestSpace, Vector2Int.right, Vector2Int.down) ||
                TryFindGridSpaceOnDiagonal(out closestSpace, Vector2Int.down, Vector2Int.left) ||
                TryFindGridSpaceOnDiagonal(out closestSpace, Vector2Int.left, Vector2Int.up))
            {
                return true;
            }

            return TryFindClosestGridSpace(out closestSpace, center, maxDistance, distance + 1);

            bool TryFindGridSpaceOnDiagonal(out Vector2Int closestSpace, Vector2Int from, Vector2Int to)
            {
                for (int step = 0; step < distance; step++)
                {
                    Vector2Int space = center + from * (distance - step) + to * step;
                    // if in range and active return
                    if (space.x >= 0 && space.x <= _gridSpaces.GetLength(0) - 1 && space.y >= 0 && space.y <= _gridSpaces.GetLength(1) - 1 && _gridSpaces[space.x, space.y])
                    {
                        closestSpace = space;
                        return true;
                    }
                }

                closestSpace = Vector2Int.zero;
                return false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_gridSpaces == null || _gridSpaces.Length == 0)
            return;

        for (int x = 0; x < _gridSpaces.GetLength(0); x++)
        {
            for (int y = 0; y < _gridSpaces.GetLength(1); y++)
            {
                if (_gridSpaces[x, y])
                {
                    Gizmos.DrawSphere(Grid.CellToWorld(new Vector3Int(x + GridMin.x, y + GridMin.y, 0)), 0.1f);
                }
            }
        }
    }
}