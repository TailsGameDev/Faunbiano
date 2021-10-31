using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleDamager : Damager
{
    private bool markedToBeDestroyedOnUpdate;

    private void Update()
    {
        if (markedToBeDestroyedOnUpdate)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == Enemy.EnemyTag)
        {
            CollisionHandler collisionHandler = col.GetComponent<CollisionHandler>();
            if (collisionHandler != null)
            {
                Enemy enemy = collisionHandler.ScriptsHolder.GetComponent<Enemy>();
                if (enemy != null)
                {
                    DealDamage(enemy.Damageable);
                }
            }
        }
    }

    public override void DealDamage(Damageable damageable)
    {
        markedToBeDestroyedOnUpdate = true;
    }
}
