using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(BoxCollider))]
public class BlockSegment : Interactable
{
    [FormerlySerializedAs("blockPositions")] public Vector2Int[] BlockPositions;
    [field: SerializeField]
    public int PointAmount { private set; get; }
    public Animator Animator;

    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private SortingGroup sortingGroup;
    [SerializeField] private Vector2Int size;

    [SerializeField] private GameObject mistakeOverlay;

    private bool _usingMask = false;

    private void OnValidate()
    {
        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider>();
        if (Animator == null)
            Animator = GetComponentInChildren<Animator>();
        if (sortingGroup == null)
            sortingGroup = GetComponentInChildren<SortingGroup>();
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

#if UNITY_EDITOR
    [ContextMenu(nameof(FindRenderers))]
    private void FindRenderers()
    {
        renderers = GetComponentsInChildren<Renderer>(true);
        OnValidate();
        EditorUtility.SetDirty(this);
    }
#endif

    public override void StartDrag(Vector2 startPressPosition)
    {
        transform.localScale = GridContainter.Instance.transform.localScale;
        sortingGroup.sortingLayerName = "Dragging";
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
            boxCollider.enabled = false;
            Animator.SetTrigger("PlaceBlock");
            audioSource.Play();
        }
        else
        {
            // Return segment to options
            UpdateMask(false);
            transform.position = BlockSpawnManager.Instance.blockAndSpawn[this];
            transform.localScale = BlockSpawnManager.Instance.spawnSize;
        }

        mistakeOverlay.SetActive(false);
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
