using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherTree : Tree
{
    private static MotherTree instance;

    private TransformWrapper transformWrapper;

    public static MotherTree Instance { get => instance; }
    public TransformWrapper TransformWrapper { get => transformWrapper; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            transformWrapper = new TransformWrapper(transform);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
