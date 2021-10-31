using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemiePrototypes = null;
    [SerializeField] private float minTimeBetweenSpawns = 0.0f;
    [SerializeField] private float maxTimeBetweenSpawns = 0.0f;

    [SerializeField] private Transform[] spawnPoints = null;

    private float timeToSpawn;

    private void Update()
    {
        if (Time.time > timeToSpawn){
            Instantiate
                (
                    original:
                    enemiePrototypes[GetRandomIndex(max: enemiePrototypes.Length)],
                    
                    position:
                    spawnPoints[GetRandomIndex(max: spawnPoints.Length)].position,

                    rotation:
                    Quaternion.identity
                );

            float cooldownToSpawn = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            timeToSpawn = Time.time + cooldownToSpawn;
        }
    }

    private int GetRandomIndex(int max)
    {
        return Random.Range(minInclusive: 0, maxExclusive: max);
    }
}
