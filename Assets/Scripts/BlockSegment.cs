using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSegment : Interactable
{

    public override void Drag(Vector2 pressPosition)
    {
        transform.position = pressPosition;
        base.Drag(pressPosition);
    }

    public override void StopDrag(Vector2 pressPosition)
    {
        base.StopDrag(pressPosition);
        if (GridContainter.Instance.TryGetAvailableGridPosition(out Vector2 gridPosition, pressPosition))
        {
            AddToContainer(GridContainter.Instance, pressPosition);
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
