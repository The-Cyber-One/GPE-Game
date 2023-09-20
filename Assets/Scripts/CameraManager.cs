using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraManager : Singleton<CameraManager>
{
    public Camera Camera { get; private set; }

    [SerializeField] private float rayBuffer = 0.1f;

    protected override void Awake()
    {
        base.Awake();

        Camera = GetComponent<Camera>();
    }

    /// <param name="worldPosition"> Will default to current press point</param>
    public bool TryHitContainer(out IContainer container, Vector3? worldPosition = null)
    {
        container = null;
        return TryCastRay(out RaycastHit raycastHit, LayerMask.GetMask("Container", "InteractableContainer"), worldPosition) && raycastHit.transform.TryGetComponent(out container);
    }

    /// <param name="worldPosition"> Will default to current press point</param>
    public bool TryCastRay(out RaycastHit raycastHit, LayerMask layerMask, Vector3? worldPosition = null)
    {
        RaycastHit[] hitInfos = Physics.RaycastAll(
            Camera.ScreenPointToRay(worldPosition != null ? Camera.WorldToScreenPoint(worldPosition.Value) : PressInteractionManager.Instance.ScreenPressPosition, Camera.MonoOrStereoscopicEye.Mono),
            Mathf.Abs(Camera.transform.position.z) + rayBuffer,
            layerMask);

        if (hitInfos.Length == 0)
        {
            raycastHit = default;
            return false;
        }

        // Pick the most visable interactable
        raycastHit = hitInfos[0];
        for (int i = 1; i < hitInfos.Length; i++)
        {
            if (hitInfos[i].transform.position.x > raycastHit.transform.position.x || hitInfos[i].transform.position.y < raycastHit.transform.position.y)
            {
                raycastHit = hitInfos[i];
            }
        }
        return raycastHit.transform != null;
    }
}
