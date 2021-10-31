using UnityEngine;

public class EnemyDabageable : Damageable
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private Shaker shaker = null;

    [SerializeField] private AudioClip damagedHitSFX = null;
    [SerializeField] private AudioClip deathSound = null;

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
        SFXPlayer.PlaySFX(damagedHitSFX);

        CurrentLife -= damager.AttackPower;

        if (CurrentLife < LifeToDie)
        {
            SFXPlayer.PlaySFX(deathSound);
            markedToBeDestroyed = true;
        }

        shaker.Shake();

        animator.SetTrigger("Damaged");
    }
}
