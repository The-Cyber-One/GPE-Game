using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(BoxCollider))]
public class BlockSegment : Interactable
{
    [FormerlySerializedAs("blockPositions")] public Vector2Int[] BlockPositions;
    [field: SerializeField]
    public int PointAmount { private set; get; }

    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private Vector2Int size;

    private Animator animator;

    [SerializeField] private GameObject waterOverlay, mistakeOverlay;

    private bool _usingMask = false;

    private void OnValidate()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        waterOverlay.SetActive(false);
        animator = GetComponentInChildren<Animator>();
    }

    [ContextMenu(nameof(FindRenderers))]
    private void FindRenderers()
    {
        renderers = GetComponentsInChildren<Renderer>(true);
        EditorUtility.SetDirty(this);
    }

    public override void StartDrag(Vector2 startPressPosition)
    {
        transform.localScale = Vector2.one;
        foreach (var sprite in renderers)
        {
            sprite.sortingLayerName = "Dragging";
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
            mistakeOverlay.SetActive(BlockPositions.Any(blockPosition => !GridContainter.Instance.GridSpaces(cellPosition.x + blockPosition.x, cellPosition.y + blockPosition.y)));
            animator.SetBool("PlaceBlock", true);
        }
        else
        {
            UpdateMask(false);
            mistakeOverlay.SetActive(false);
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
            boxCollider.enabled = false;
        }
        else
        {
            // Return segment to options
            UpdateMask(false);
            transform.position = BlockSpawnManager.Instance.blockAndSpawn[this];
            transform.localScale = BlockSpawnManager.Instance.spawnSize;
        }

        mistakeOverlay.SetActive(false);
        foreach (var sprite in renderers)
        {
            sprite.sortingLayerName = "Default";
        }
    }

    private void UpdateMask(bool useMask)
    {
        if (_usingMask == useMask)
            return;

        _usingMask = useMask;
        foreach (var sprite in renderers)
        {
            switch (sprite)
            {
                case TilemapRenderer tilemapRenderer:
                    tilemapRenderer.maskInteraction = useMask ? SpriteMaskInteraction.VisibleInsideMask : SpriteMaskInteraction.None;
                    break;
                case SpriteRenderer spriteRenderer:
                    spriteRenderer.maskInteraction = useMask ? SpriteMaskInteraction.VisibleInsideMask : SpriteMaskInteraction.None;
                    break;
            }
        }
    }
}
