using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [System.Serializable]
    private struct WaveData
    {
        [SerializeField] private List<Enemy> enemiesToClone;
        [SerializeField] private float minTimeBetweenSpawns;
        [SerializeField] private float maxTimeBetweenSpawns;

        public List<Enemy> EnemiesToSpawn { get => enemiesToClone; }
        public float MinTimeBetweenSpawns { get => minTimeBetweenSpawns; }
        public float MaxTimeBetweenSpawns { get => maxTimeBetweenSpawns; }
    }

    [SerializeField] private Transform[] spawnPoints = null;

    [SerializeField] private WaveData[] waves = null;
    private int currentWaveIndex;

    private List<Enemy> aliveEnemies = new List<Enemy>();

    private float timeToSpawn;

    private static System.Action onGameWin;

    private void OnDestroy()
    {
        onGameWin = null;
    }

    public static void RegisterOnGameWinAction(System.Action action)
    {
        onGameWin += action;
    }

    private void Update()
    {
        if (currentWaveIndex < waves.Length)
        {
            WaveData currentWaveData = waves[currentWaveIndex];

            if (Time.time > timeToSpawn)
            {
                // Instantiate prototype and remove it from list if needed
                if (currentWaveData.EnemiesToSpawn.Count > 0)
                {
                    Enemy enemyToClone = currentWaveData.EnemiesToSpawn[0];
                    currentWaveData.EnemiesToSpawn.RemoveAt(0);

                    Vector3 spawnPosition =
                        spawnPoints[GetRandomIndex(max: spawnPoints.Length)].position;

                    Enemy enemy =
                        Instantiate(enemyToClone, spawnPosition, Quaternion.identity);
                    aliveEnemies.Add(enemy);
                }

                // Set new timeToSpawn based on cooldown from currentWaveData
                float cooldownToSpawn = Random.Range
                    (currentWaveData.MinTimeBetweenSpawns, currentWaveData.MaxTimeBetweenSpawns);
                timeToSpawn = Time.time + cooldownToSpawn;
            }

            bool allEnemiesOfThisWaveGotKilled =
                aliveEnemies.Count == 0 && currentWaveData.EnemiesToSpawn.Count == 0;
            if (allEnemiesOfThisWaveGotKilled)
            {
                // Start next wave
                currentWaveIndex++;
            }
            else
            {
                for (int e = aliveEnemies.Count - 1; e >= 0; e--)
                {
                    Enemy enemy = aliveEnemies[e];
                    bool enemyIsDead = enemy == null;
                    if (enemyIsDead)
                    {
                        aliveEnemies.RemoveAt(e);
                    }
                }
            }
        }
        else
        {
            onGameWin?.Invoke();
            this.enabled = false;
        }
    }

    private int GetRandomIndex(int max)
    {
        return Random.Range(minInclusive: 0, maxExclusive: max);
    }
}
