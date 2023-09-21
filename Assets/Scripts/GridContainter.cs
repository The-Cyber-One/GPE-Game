using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GridContainter : Singleton<GridContainter>, IContainer
{
    public Grid Grid;

    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private float gridScaleMax = 1.5f;
    [SerializeField] GameObject backgroundSquare;
    [SerializeField] GameObject backgroundHolder;

    private readonly Dictionary<Vector2Int, Interactable> _interactablePlaces = new();
    private bool[,] _gridSpaces;
    private Vector2Int _gridMin, _gridMax;
    private float _gridScaleMin;
    private Vector2 _previousPressPoint;

    public Transform Content => Grid.transform;

    protected override void Awake()
    {
        base.Awake();

        if (Grid == null) Grid = GetComponent<Grid>();
        if (boxCollider == null) boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        SetupGridSpaces();
    }

    public void AddInteractable(Interactable interactable, Vector2 worldPosition)
    {
        Vector2Int cellPosition = GetCellPosition(worldPosition);
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
        _gridSpaces = new bool[gridSize.x, gridSize.y];
        _gridMin = -new Vector2Int((gridSize.x - 1) / 2, (gridSize.y - 1) / 2);
        _gridMax = new Vector2Int(gridSize.x / 2, gridSize.y / 2);

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                //Create the background
                GameObject newBackground = Instantiate(backgroundSquare);
                newBackground.transform.localScale = Grid.cellSize;
                newBackground.transform.position = (new Vector2(x, y) + _gridMin) * Grid.cellSize + new Vector2(Grid.transform.position.x, Grid.transform.position.y);
                newBackground.transform.SetParent(backgroundHolder.transform);

                _gridSpaces[x, y] = true;
            }
        }
    }

    public Vector2Int GetCellPosition(Vector2 worldPosition)
    {
        Vector2Int cellPosition = (Vector2Int)Grid.WorldToCell(worldPosition);
        cellPosition.Clamp(_gridMin, _gridMax);
        return cellPosition;
    }

    public Vector2 GetGridPosition(Vector2 worldPosition) =>
        Grid.CellToWorld((Vector3Int)GetCellPosition(worldPosition));

    public bool IsInGrid(Vector2 worldPosition)
    {
        Vector2Int cellPosition = (Vector2Int)Grid.WorldToCell(worldPosition);
        return cellPosition.x >= _gridMin.x && cellPosition.x <= _gridMax.x
            && cellPosition.y >= _gridMin.y && cellPosition.y <= _gridMax.y;
    }

    public bool TryGetAvailableGridPosition(out Vector2 gridPosition, Vector2 worldPosition)
    {
        gridPosition = Vector2.zero;

        Vector2Int cellPosition = (Vector2Int)Grid.WorldToCell(worldPosition);
        cellPosition.Clamp(_gridMin, _gridMax);
        Vector2Int index = cellPosition - _gridMin;

        if (_gridSpaces[index.x, index.y])
        {
            gridPosition = Grid.CellToWorld((Vector3Int)cellPosition);
            return true;
        }

        int largestDistance = Mathf.Max(index.x, index.y, _gridSpaces.GetLength(0) - index.x, _gridSpaces.GetLength(1) - index.y);

        if (TryFindClosestGridSpace(out Vector2Int closestSpace, index, largestDistance * 2))
        {
            gridPosition = Grid.CellToWorld((Vector3Int)(closestSpace + _gridMin));
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
                    Gizmos.DrawSphere(Grid.CellToWorld(new Vector3Int(x + _gridMin.x, y + _gridMin.y, 0)), 0.1f);
                }
            }
        }
    }
}