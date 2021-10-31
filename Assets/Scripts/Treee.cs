using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treee : MonoBehaviour
{
    public enum TreeTypeEnum
    {
        NONE,
        APPLE_TREE,
        MOTHER_TREE,
        BUSH,
    }

    [SerializeField] private TreeTypeEnum treeType = 0;

    // NOTE: cooldown unity is seconds
    [SerializeField] private float cooldown = 0.0f;
    private float timeToShoot;

    [SerializeField] private Transform bulletSpawnPoint;
    private TransformWrapper bulletSpawnPointWrapper;
    
    [SerializeField] private Damageable damageable;

    // NOTE enemiesInRange code is duplicated in Player.cs
    private List<Enemy> enemiesInRange = new List<Enemy>();

    protected TransformWrapper BulletSpawnPoint { get => bulletSpawnPointWrapper; }
    public Damageable Damageable { get => damageable; }
    public TreeTypeEnum TreeType { get => treeType; }

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
    protected Enemy GetClosestEnemie()
    {
        float minDif = Mathf.Infinity;
        Enemy closestEnemie = enemiesInRange[0];
        for (int e = 1; e < enemiesInRange.Count; e++)
        {
            Enemy loopEnemie = enemiesInRange[e];

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
        if (col.tag == Enemy.EnemyTag)
        {
            GameObject enemieScriptsNode = col.GetComponent<CollisionHandler>().ScriptsHolder;
            Enemy enemie = enemieScriptsNode.GetComponent<Enemy>();

            if (!enemiesInRange.Contains(enemie))
            {
                enemiesInRange.Add(enemie);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == Enemy.EnemyTag)
        {
            GameObject enemieScriptsNode = col.GetComponent<CollisionHandler>().ScriptsHolder;
            Enemy enemie = enemieScriptsNode.GetComponent<Enemy>();

            enemiesInRange.Remove(enemie);
        }
    }
}
