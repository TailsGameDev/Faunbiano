using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] private Bullet apple = null;
    
    private static BulletFactory instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static Bullet CreateAndShootApple(TransformWrapper origin, TransformWrapper target)
    {
        Bullet apple = Instantiate(instance.apple, origin.Position, Quaternion.identity);
        Vector3 targetDirection = (target.Position - origin.Position).normalized;
        apple.RB2D.velocity = apple.Speed * targetDirection;
        return apple;
    }
}
