using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    public IContainer Container;

    public void AddToContainer(IContainer container, Vector2 worldPosition)
    {
        Container = container;
        Container.AddInteractable(this, worldPosition);
    }

    public void RemoveFromContainer()
    {
        Container?.RemoveInteractable(this);
        Container = null;
    }

    public virtual void Press()
    {
        if (Container is IInteractable interactable)
            interactable.Press();
    }
    public virtual void DoublePress()
    {
        if (Container is IInteractable interactable)
            interactable.Press();
    }
    public virtual void StartDrag(Vector2 startPressPosition)
    {
        if (Container is IInteractable interactable)
            interactable.StartDrag(startPressPosition);
    }
    public virtual void Drag(Vector2 pressPosition)
    {
        if (Container is IInteractable interactable)
            interactable.Drag(pressPosition);
    }
    public virtual void StopDrag(Vector2 pressPosition)
    {
        if (Container is IInteractable interactable)
            interactable.StopDrag(pressPosition);
    }
    public virtual void StartDoublePressedDrag(Vector2 startPressPosition) => StartDrag(startPressPosition);
    public virtual void DoublePressedDrag(Vector2 pressPosition) => Drag(pressPosition);
    public virtual void StopDoublePressedDrag(Vector2 pressPosition) => StopDrag(pressPosition);
    public virtual void Scale(Vector2 centerPressPosition, float scale)
    {
        if (Container is IInteractable interactable)
            interactable.Scale(centerPressPosition, scale);
    }
    public virtual void StartTwoFingerDrag(Vector2 startCenterPressPosition)
    {
        if (Container is IInteractable interactable)
            interactable.StartTwoFingerDrag(startCenterPressPosition);
    }
    public virtual void TwoFingerDrag(Vector2 centerPressPosition)
    {
        if (Container is IInteractable interactable)
            interactable.TwoFingerDrag(centerPressPosition);
    }
    public virtual void StopTwoFingerDrag(Vector2 centerPressPosition)
    {
        if (Container is IInteractable interactable)
            interactable.StopTwoFingerDrag(centerPressPosition);
    }
}

public interface IInteractable
{
    public virtual void Press() { }
    public virtual void DoublePress() { }
    public virtual void StartDrag(Vector2 startPressPosition) { }
    public virtual void Drag(Vector2 pressPosition) { }
    public virtual void StopDrag(Vector2 pressPosition) { }
    public virtual void StartDoublePressedDrag(Vector2 startPressPosition) => StartDrag(startPressPosition);
    public virtual void DoublePressedDrag(Vector2 pressPosition) => Drag(pressPosition);
    public virtual void StopDoublePressedDrag(Vector2 pressPosition) => StopDrag(pressPosition);
    public virtual void Scale(Vector2 centerPressPosition, float scale) { }
    public virtual void StartTwoFingerDrag(Vector2 startCenterPressPosition) { }
    public virtual void TwoFingerDrag(Vector2 centerPressPosition) { }
    public virtual void StopTwoFingerDrag(Vector2 centerPressPosition) { }
}