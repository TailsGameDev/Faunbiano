using UnityEngine;

public class EnemyDabageable : Damageable
{
    [SerializeField] Animator animator = null;
    [SerializeField] Shaker shaker = null;

    private bool markedToBeDestroyed;

    private void Update()
    {
        if (markedToBeDestroyed)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == Bullet.BulletTag)
        {
            CollisionHandler collisionHandler = col.GetComponent<CollisionHandler>();
            if (collisionHandler != null)
            {
                Damager damager = collisionHandler.ScriptsHolder.GetComponent<Damager>();
                if (damager != null)
                {
                    TakeDamage(damager);
                }
            }
        }
    }

    public override void TakeDamage(Damager damager)
    {
        CurrentLife -= damager.AttackPower;

        if (CurrentLife < LifeToDie)
        {
            markedToBeDestroyed = true;
        }

        shaker.Shake();

        animator.SetTrigger("Damaged");
    }
}
