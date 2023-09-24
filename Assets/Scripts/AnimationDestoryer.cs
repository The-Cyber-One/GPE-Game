using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDestoryer : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(transform.parent.gameObject, 0.1f);
    }
}
