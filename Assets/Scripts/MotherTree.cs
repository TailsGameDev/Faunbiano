using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherTree : Treee
{
    [SerializeField] private Seed[] seedPrototypes = null;
    [SerializeField] private float minCooldownToNextSeed = 0.0f;
    [SerializeField] private float maxCooldownToNextSeed = 0.0f;
    private float timeToSpawnSeed;

    private Seed seedInMouth;

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
        // Forget seeds that have already been picked up
        // Then start to count the cooldown to spawn the next seed
        if (this.seedInMouth != null && this.seedInMouth.PickedUp)
        {
            this.seedInMouth = null;

            float cooldown = Random.Range(minCooldownToNextSeed, maxCooldownToNextSeed);
            timeToSpawnSeed = Time.time + cooldown;
        }

        // Spawn seeds
        if (Time.time > timeToSpawnSeed && seedInMouth == null)
        {
            this.seedInMouth = Instantiate
                (
                    original:
                    seedPrototypes[Random.Range(0, seedPrototypes.Length)],

                    position:
                    BulletSpawnPointOriginal.position,

                    rotation:
                    Quaternion.identity
                );

            // Set time so we don't count the cooldown until the seed is picked up
            timeToSpawnSeed = Mathf.Infinity;
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
