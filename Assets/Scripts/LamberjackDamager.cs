using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LamberjackDamager : Damager
{
    [SerializeField] private Enemie enemie;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        enemie.Damager = this;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        CollisionHandler collisionHandler = col.collider.GetComponent<CollisionHandler>();
        if (collisionHandler != null)
        {
            Tree tree = collisionHandler.ScriptsHolder.GetComponent<Tree>();
            if (tree != null)
            {
                enemie.TreeToChop = tree;
            }
        }
    }
    public override void DealDamage(Damageable damageable)
    {
        animator.SetTrigger("attack");
    }
}
