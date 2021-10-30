using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie : MonoBehaviour
{
    private TransformWrapper transformWrapper;

    public TransformWrapper TransformWrapper { get => transformWrapper; }

    private void Awake()
    {
        transformWrapper = new TransformWrapper(transform);
    }
}
