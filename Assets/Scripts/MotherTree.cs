using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherTree : Treee
{
    [SerializeField] private Seed[] seedPrototypes = null;

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

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == Seed.SeedTag)
        {
            CollisionHandler collisionHandler = col.collider.GetComponent<CollisionHandler>();
            Seed seed = collisionHandler.ScriptsHolder.GetComponent<Seed>();
            if (seed != null)
            {
                seed.TransformWrapper.Position = BulletSpawnPointOriginal.position;
                
                /* NOTE: THIS WAS CAUSING ERROR I DON'T KNOW WHY, PEACE AND LOVE
                 * FROM YOUR PAST SELF TAILS
                try
                {
                    // Put seed back on it's place.
                    // seed.TransformWrapper.Position = BulletSpawnPoint.Position;
                }
                catch(System.Exception e)
                {
                    Debug.LogError(e.Message);
                    
                    Debug.LogError("BulletSpawnPoint: " + BulletSpawnPoint);
                    if (BulletSpawnPoint != null)
                    {
                        Debug.LogError("BulletSpawnPoint.Position: "+BulletSpawnPoint.Position);
                    }

                    Debug.LogError("seed: "+seed);
                    if (seed != null)
                    {
                        Debug.LogError("seed transform wrapper: " + seed.TransformWrapper);
                        if (seed.TransformWrapper != null)
                        {
                            Debug.LogError("seed transform wrapper position: "+
                                seed.TransformWrapper.Position);
                        }
                    }
                }
                */
            }
        }
    }
}
