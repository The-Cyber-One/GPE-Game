using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSegment : Interactable
{

    public override void Drag(Vector2 pressPosition)
    {
        transform.position = pressPosition;
    }

    public override void StopDrag(Vector2 pressPosition)
    {
        if (GridContainter.Instance.IsInGrid(pressPosition))
        {
            AddToContainer(GridContainter.Instance, pressPosition + new Vector2(0.5f, 0.5f));
        }
    }
}
