using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: I couldn't finish to write this class, sorry it does not work
public class ObjectsInRangeHandler : MonoBehaviour
{
    private enum ObjectTypes
    {
        NONE = 0,
        ENEMIE = 1,
        SEED = 2,
    }

    [SerializeField] private ObjectTypes objectType = 0;

    private List<object> enemiesInRange = new List<object>();

    private void Awake()
    {
        switch (objectType)
        {
            case ObjectTypes.ENEMIE:
                break;
            case ObjectTypes.SEED:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemie")
        {
            GameObject enemieScriptsNode = col.GetComponent<CollisionHandler>().ScriptsHolder;
            Enemie enemie = enemieScriptsNode.GetComponent<Enemie>();

            if (!enemiesInRange.Contains(enemie))
            {
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
