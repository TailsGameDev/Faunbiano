using System.Collections.Generic;
using UnityEngine;

public class LamberjackDamager : Damager
{
    [SerializeField] private Enemie enemie;
    [SerializeField] private Animator animator;
    [SerializeField] private List<Tree.TreeTypeEnum> treeTypesEnabledToHit = null;

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
            // TODO Not accept all types of trees
            if (tree != null && treeTypesEnabledToHit.Contains(tree.TreeType))
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
