using UnityEngine;

public class AppleTree : Treee
{
    [SerializeField] private Bullet applePrototype = null;

    private TransformWrapper transformWrapper = null;

    private void Awake()
    {
        transformWrapper = new TransformWrapper(transform);
    }

    private void Update()
    {
        // Shoot if needed
        if (IsTimeToShoot() && IsThereEnemieInRange())
        {
            Vector3 spawnPosition = transformWrapper.Position;
            Vector3 enemyPosition = GetClosestEnemie().TransformWrapper.Position;

            // Instantiate Apple
            Bullet apple = Instantiate(applePrototype, spawnPosition, Quaternion.identity);
            Vector3 targetDirection = (enemyPosition - spawnPosition).normalized;
            apple.RB2D.velocity = apple.Speed * targetDirection;

            MarkShootInTimer();
        }
    }
}