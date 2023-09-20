using UnityEngine;

public interface IContainer
{
    Transform Content { get; }

    /// <summary>
    /// Only call from <see cref="Interactable"/> otherwise use <see cref="Interactable.AddToContainer(IContainer, Vector2)"/>
    /// </summary>
    public void AddInteractable(Interactable interactable, Vector2 worldPosition);
    /// <summary>
    /// Only call from <see cref="Interactable"/> otherwise use <see cref="Interactable.RemoveFromContainer()"/>
    /// </summary>
    public void RemoveInteractable(Interactable interactable);
}
