using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: I couldn't finish to write this class, sorry it does not work
public class ObjectsInRangeHandler : MonoBehaviour
{
    private enum ObjectTypes
    {
        NONE = 0,
        ENEMY = 1,
        SEED = 2,
    }

    [SerializeField] private ObjectTypes objectType = 0;

    private List<object> enemiesInRange = new List<object>();

    private void Awake()
    {
        switch (objectType)
        {
            case ObjectTypes.ENEMY:
                break;
            case ObjectTypes.SEED:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == Enemy.EnemyTag)
        {
            GameObject enemyScriptsNode = col.GetComponent<CollisionHandler>().ScriptsHolder;
            Enemy enemy = enemyScriptsNode.GetComponent<Enemy>();

            if (!enemiesInRange.Contains(enemy))
            {
                enemiesInRange.Add(enemy);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == Enemy.EnemyTag)
        {
            GameObject enemyScriptsNode = col.GetComponent<CollisionHandler>().ScriptsHolder;
            Enemy enemy = enemyScriptsNode.GetComponent<Enemy>();

            enemiesInRange.Remove(enemy);
        }
    }
}
