using UnityEngine;

public class AppleTree : Tree
{
    private void Update()
    {
        // Shoot if needed
        if (IsTimeToShoot() && IsThereEnemieInRange())
        {
            Enemie closestEnemie = GetClosestEnemie();
            BulletFactory.CreateAndShootApple(BulletSpawnPoint, closestEnemie.TransformWrapper);
            MarkShootInTimer();
        }
    }
}