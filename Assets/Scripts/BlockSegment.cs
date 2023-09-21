using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class BlockSegment : Interactable
{
    [FormerlySerializedAs("blockPositions")] public Vector2Int[] BlockPositions;
    [field:SerializeField]
    public int PointAmount { private set; get; }

    [SerializeField] private SpriteRenderer[] spriteRenderers;
    [SerializeField] private Vector2Int size;

    [SerializeField] private GameObject waterOverlay;

    private bool _usingMask = false;

    [ContextMenu(nameof(FindSprites))]
    private void FindSprites()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
    }

    public override void StartDrag(Vector2 startPressPosition)
    {
        transform.localScale = Vector2.one;
        foreach (var sprite in spriteRenderers)
        {
            sprite.sortingLayerName = "Foreground";
        }
    }

    public override void Drag(Vector2 pressPosition)
    {
        Vector2 position = pressPosition + new Vector2(0.5f, 0.5f);
        if (CameraManager.Instance.TryHitContainer(out _))
        {
            UpdateMask(true);

            Vector2Int cellPosition = GridContainter.Instance.FitToGrid(position, size);
            transform.position = GridContainter.Instance.Grid.CellToWorld((Vector3Int)cellPosition);
        }
        else
        {
            UpdateMask(false);
            transform.position = pressPosition;
        }
    }

    public override void StopDrag(Vector2 pressPosition)
    {
        Vector2 position = pressPosition + new Vector2(0.5f, 0.5f);
        Vector2Int cellPosition = GridContainter.Instance.FitToGrid(position, size);

        // Check each collider with grid
        if (CameraManager.Instance.TryHitContainer(out _) && !BlockPositions.Any(blockPosition => !GridContainter.Instance.GridSpaces(cellPosition.x + blockPosition.x, cellPosition.y + blockPosition.y)))
        {
            // Place segment
            UpdateMask(true);
            RemoveFromContainer();
            AddToContainer(GridContainter.Instance, GridContainter.Instance.Grid.CellToWorld((Vector3Int)cellPosition));
            waterOverlay.SetActive(true);
        }        else
        {
            // Return segment to options
            UpdateMask(false);
            transform.position = BlockSpawnManager.Instance.blockAndSpawn[this];
            transform.localScale = BlockSpawnManager.Instance.spawnSize;
        }

        foreach (var sprite in spriteRenderers)
        {
            sprite.sortingLayerName = "Default";
        }
    }

    private void UpdateMask(bool useMask)
    {
        if (_usingMask == useMask)
            return;

        _usingMask = useMask;
        foreach (var sprite in spriteRenderers)
        {
            sprite.maskInteraction = useMask ? SpriteMaskInteraction.VisibleInsideMask : SpriteMaskInteraction.None;
        }
    }
}
