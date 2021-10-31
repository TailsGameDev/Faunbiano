using System.Collections.Generic;
using UnityEngine;

public class LamberjackDamager : Damager
{
    [SerializeField] private Enemy enemie = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private AudioSource attackSound = null;
    [SerializeField] private List<Treee.TreeTypeEnum> treeTypesEnabledToHit = null;

    private void Awake()
    {
        enemie.Damager = this;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        CollisionHandler collisionHandler = col.collider.GetComponent<CollisionHandler>();
        if (collisionHandler != null)
        {
            Treee tree = collisionHandler.ScriptsHolder.GetComponent<Treee>();
            // TODO Not accept all types of trees
            if (tree != null && treeTypesEnabledToHit.Contains(tree.TreeType))
            {
                enemie.TreeToChop = tree;
            }
        }
    }
    public override void DealDamage(Damageable damageable)
    {
        animator.SetTrigger("Attack");
        attackSound.Play();
    }
}
