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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == Seed.SeedTag)
        {
            CollisionHandler collisionHandler = col.collider.GetComponent<CollisionHandler>();
            Seed seed = collisionHandler.ScriptsHolder.GetComponent<Seed>();
            if (seed != null)
            {
                // Put seed back on it's place.
                seed.TransformWrapper.Position = BulletSpawnPoint.Position;
            }
        }
    }
}
