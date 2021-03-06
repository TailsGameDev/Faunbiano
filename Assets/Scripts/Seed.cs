using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    [SerializeField] private Collider2D col = null;
    [SerializeField] private Rigidbody2D rb2D = null;
    [SerializeField] private Treee treeToPlantPrototype = null;

    private bool pickedUp;

    private TransformWrapper transformWrapper;

    public readonly static string SeedTag = "Seed";

    public Collider2D Collider { get => col; set => col = value; }
    public TransformWrapper TransformWrapper { get => transformWrapper; }
    public Rigidbody2D RB2D { get => rb2D; }
    public Treee TreeToPlantPrototype { get => treeToPlantPrototype; }
    public bool PickedUp { get => pickedUp; set => pickedUp = value; }

    private void Awake()
    {
        transformWrapper = new TransformWrapper(transform);
    }
}
