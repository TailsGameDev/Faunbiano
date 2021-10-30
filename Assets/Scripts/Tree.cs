using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawnPoint;
    
    // NOTE: cooldown unity is seconds
    [SerializeField] private float cooldown = 0.0f;
    private float timeToShoot;

    private List<Enemie> enemiesInRange = new List<Enemie>();

    private TransformWrapper bulletSpawnPointWrapper;
    protected TransformWrapper BulletSpawnPoint { get => bulletSpawnPointWrapper; }
    protected float Cooldown { get => cooldown; }

    private void Awake()
    {
        bulletSpawnPointWrapper = new TransformWrapper(bulletSpawnPoint);
    }

    // cooldown related
    protected bool IsTimeToShoot()
    {
        return Time.time > timeToShoot;
    }
    protected void MarkShootInTimer()
    {
        timeToShoot = Time.time + cooldown;
    }

    // enemiesInRange related
    protected bool IsThereEnemieInRange()
    {
        return enemiesInRange.Count > 0;
    }
    protected Enemie GetClosestEnemie()
    {
        float minDif = Mathf.Infinity;
        Enemie closestEnemie = enemiesInRange[0];
        for (int e = 1; e < enemiesInRange.Count; e++)
        {
            Enemie loopEnemie = enemiesInRange[e];

            // Calculate distance
            float xDif = Mathf.Abs(transform.position.x - loopEnemie.transform.position.x);
            float yDif = Mathf.Abs(transform.position.y - loopEnemie.transform.position.y);
            float dif = xDif + yDif;

            bool foundEvenCloserEnemie = dif < minDif;
            if (foundEvenCloserEnemie)
            {
                closestEnemie = loopEnemie;
            }
        }

        return closestEnemie;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemie")
        {
            GameObject enemieScriptsNode = col.GetComponent<CollisionHandler>().ScriptsHolder;
            Enemie enemie = enemieScriptsNode.GetComponent<Enemie>();

            if (!enemiesInRange.Contains(enemie))
            {
                Debug.Log("Lumberyard inserted", enemie);
                enemiesInRange.Add(enemie);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Enemie")
        {
            GameObject enemieScriptsNode = col.GetComponent<CollisionHandler>().ScriptsHolder;
            Enemie enemie = enemieScriptsNode.GetComponent<Enemie>();

            enemiesInRange.Remove(enemie);
        }
    }
}