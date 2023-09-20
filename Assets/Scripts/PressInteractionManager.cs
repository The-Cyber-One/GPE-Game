using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PressInteractionManager : Singleton<PressInteractionManager>
{
    [SerializeField] float dragDistance = 0.5f;
    [SerializeField] LayerMask interactableLayerMask, containerLayerMask;

    private InputSettings _inputActions;
    private InputSettings.InteractionsActions _actions;
    private readonly HashSet<IInteractable> _interactables = new();

    public Vector2 ScreenPressPosition => _actions.PointMain.ReadValue<Vector2>();
    public Vector2 WorldPressPosition => CameraManager.Instance.Camera.ScreenToWorldPoint((Vector3)ScreenPressPosition + Vector3.back * CameraManager.Instance.Camera.transform.position.z);

    protected override void Awake()
    {
        base.Awake();

        _inputActions = new();
        _inputActions.Enable();
        _actions = _inputActions.Interactions;

        _actions.PressMain.performed += OnPress;
        _actions.MouseScroll.performed += OnMouseScroll;
        _actions.MousePressSecondary.performed += OnMousePressSecondary;
    }

    private void OnDestroy()
    {
        _inputActions.Disable();
        _inputActions.Dispose();
    }

    private bool TryHitInteractable(out IInteractable interactable)
    {
        interactable = null;
        return
            ((CameraManager.Instance.TryCastRay(out RaycastHit raycastHitInteractable, interactableLayerMask) && raycastHitInteractable.transform.TryGetComponent(out interactable))
            || (CameraManager.Instance.TryCastRay(out RaycastHit raycastHitContainer, containerLayerMask) && raycastHitContainer.transform.TryGetComponent(out interactable)))
            && !_interactables.Contains(interactable);
    }

    private void OnPress(InputAction.CallbackContext obj)
    {
        if (TryHitInteractable(out IInteractable interactable))
        {
            _interactables.Add(interactable);
            StartCoroutine(C_UpdatePressing(interactable, WorldPressPosition));
        }
    }

    private void OnMouseScroll(InputAction.CallbackContext obj)
    {
        if (TryHitInteractable(out IInteractable interactable))
        {
            interactable.Scale(WorldPressPosition, _actions.MouseScroll.ReadValue<float>());
        }
    }

    private void OnMousePressSecondary(InputAction.CallbackContext obj)
    {
        if (TryHitInteractable(out IInteractable interactable))
        {
            _interactables.Add(interactable);
            StartCoroutine(C_UpdateMousePressSecondary(interactable, WorldPressPosition));
        }
    }

    private IEnumerator C_UpdateMousePressSecondary(IInteractable interactable, Vector2 startPosition)
    {
        interactable.StartTwoFingerDrag(startPosition);
        while (_actions.MousePressSecondary.ReadValue<float>() >= InputSystem.settings.defaultButtonPressPoint)
        {
            interactable.TwoFingerDrag(WorldPressPosition);
            yield return null;
        }
        interactable.StopTwoFingerDrag(WorldPressPosition);
        _interactables.Remove(interactable);
    }

    private IEnumerator C_UpdatePressing(IInteractable interactable, Vector2 startPosition)
    {
        bool IsHoldingMain() => _actions.PressMain.ReadValue<float>() >= InputSystem.settings.defaultButtonPressPoint;
        bool IsHoldingSecondary() => _actions.PressSecondary.ReadValue<float>() >= InputSystem.settings.defaultButtonPressPoint;
        bool doublePressed = false;

        // Wait for movement, canceling of press or second press
        while (Vector2.Distance(startPosition, WorldPressPosition) < dragDistance && IsHoldingMain() && !IsHoldingSecondary())
        {
            yield return null;
        }

        // Handle two finger movement
        if (IsHoldingSecondary())
        {
            Vector2 CenterPoint()
            {
                Vector2 mainPressPosition = WorldPressPosition;
                Vector2 direction = (Vector2)CameraManager.Instance.Camera.ScreenToWorldPoint(_actions.PointSecondary.ReadValue<Vector2>()) - mainPressPosition;
                return mainPressPosition + direction / 2;
            }
            Vector2 PressPointMain() => _actions.PointMain.ReadValue<Vector2>() / Mathf.Min(Screen.width, Screen.height);
            Vector2 PressPointSecondary() => _actions.PointSecondary.ReadValue<Vector2>() / Mathf.Min(Screen.width, Screen.height);

            Vector2 startCenterPoint = CenterPoint();
            bool drag = false;
            Vector2 previousPressPointMain = PressPointMain();
            Vector2 previousPressPointSecondary = PressPointSecondary();

            while (IsHoldingMain() && IsHoldingSecondary())
            {
                // Move
                Vector2 centerPoint = CenterPoint();
                if (!drag && Vector2.Distance(startCenterPoint, centerPoint) >= dragDistance)
                {
                    drag = true;
                    interactable.StartTwoFingerDrag(startCenterPoint);
                }

                if (drag)
                {
                    interactable.TwoFingerDrag(centerPoint);
                }

                // Scale
                float distance = Vector2.Distance(PressPointMain(), PressPointSecondary());
                float previousDistance = Vector2.Distance(previousPressPointMain, previousPressPointSecondary);
                interactable.Scale(centerPoint, distance - previousDistance);

                previousPressPointMain = PressPointMain();
                previousPressPointSecondary = PressPointSecondary();
                yield return null;
            }

            if (drag)
            {
                interactable.StopTwoFingerDrag(CenterPoint());
            }
            _interactables.Remove(interactable);
            yield break;
        }

        // Handle clicks
        if (!IsHoldingMain())
        {
            // Wait for second press
            float releaseTime = 0;
            while (!IsHoldingMain() && releaseTime < InputSystem.settings.multiTapDelayTime)
            {
                yield return null;
                releaseTime += Time.deltaTime;
            }

            if (releaseTime >= InputSystem.settings.multiTapDelayTime || !IsHoldingMain())
            {
                interactable.Press();
                _interactables.Remove(interactable);
                yield break;
            }

            // Only continue checking double press if we hit the same target
            _interactables.Remove(interactable);
            if (!TryHitInteractable(out IInteractable hit))
                yield break;
            _interactables.Add(interactable);

            startPosition = WorldPressPosition;

            // Wait for movement or canceling of press to check dragging
            while (Vector2.Distance(startPosition, WorldPressPosition) < dragDistance && IsHoldingMain())
                yield return null;

            if (!IsHoldingMain())
            {
                interactable.DoublePress();
                _interactables.Remove(interactable);

                yield break;
            }

            doublePressed = true;
        }

        // Handle movement
        if (doublePressed)
        {
            interactable.StartDoublePressedDrag(startPosition);
        }
        else
        {
            interactable.StartDrag(startPosition);
        }

        // Drag
        while (IsHoldingMain())
        {
            if (doublePressed)
            {
                interactable.DoublePressedDrag(WorldPressPosition);
            }
            else
            {
                interactable.Drag(WorldPressPosition);
            }

            yield return null;
        }

        // Place
        if (doublePressed)
        {
            interactable.StopDoublePressedDrag(WorldPressPosition);
        }
        else
        {
            interactable.StopDrag(WorldPressPosition);
        }

        _interactables.Remove(interactable);
    }
}
