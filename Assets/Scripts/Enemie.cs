using UnityEngine;

public class Enemie : MonoBehaviour
{
    private enum State
    {
        WALK_TO_MOTHER_TREE,
        WANDER,
    }

    [SerializeField] private Rigidbody2D rb2D = null;
    [SerializeField] private float speed = 0.0f;
    [SerializeField] private float cooldownToChangeDirection = 0.0f;

    private State currentState;

    private TransformWrapper transformWrapper;

    private Vector3[] wanderDirections;
    private Vector3 wanderDirection = Vector3.zero;

    private float timeToChangeDirection;

    public TransformWrapper TransformWrapper { get => transformWrapper; }

    private void Awake()
    {
        transformWrapper = new TransformWrapper(transform);

        wanderDirections = new Vector3[] { Vector3.left, Vector3.right, Vector3.down };
    }

    private void Update()
    {
        State nextState = currentState;
        switch (currentState)
        {
            case State.WALK_TO_MOTHER_TREE:
                {
                    rb2D.AddForce(GetMotherTreeDirection() * speed, ForceMode2D.Force);

                    DecideIfToWanderOrToReachMotherTree(ref nextState);
                    break;
                }
            case State.WANDER:
                {
                    rb2D.AddForce(this.wanderDirection * speed, ForceMode2D.Force);

                    if (Time.time > timeToChangeDirection)
                    {
                        DecideIfToWanderOrToReachMotherTree(ref nextState);
                    }
                    break;
                }
        }

        this.currentState = nextState;
    }
    private Vector3 GetMotherTreeDirection()
    {
        Vector3 motherTreeDirection = MotherTree.Instance.TransformWrapper.Position - transformWrapper.Position;
        return motherTreeDirection.normalized;
    }
    private void DecideIfToWanderOrToReachMotherTree(ref State nextState)
    {
        // Change state to wander if needed
        if (!IsThereSomethingInTheWayToMotherTree())
        {
            nextState = State.WALK_TO_MOTHER_TREE;
        }
        else
        {
            // Then let's wander a little bit
            nextState = State.WANDER;

            int randomIndex = Random.Range(minInclusive: 0, maxExclusive: wanderDirections.Length);
            this.wanderDirection = wanderDirections[randomIndex];

            timeToChangeDirection = Time.time + cooldownToChangeDirection;
        }
    }
    private bool IsThereSomethingInTheWayToMotherTree()
    {
        const float PATHFINDING_MAX_DISTANCE = 2.0f;
        RaycastHit2D raycastHit = Physics2D.BoxCast
            (
                origin: transformWrapper.Position,
                size: new Vector2(1.0f, PATHFINDING_MAX_DISTANCE),
                angle: 0.0f,
                direction: GetMotherTreeDirection(), 
                distance: /*1.0f for the size of the player*/1.0f + PATHFINDING_MAX_DISTANCE/2
            );
        bool isThereSomethingInTheWay = raycastHit.collider != null;

        return isThereSomethingInTheWay;
    }
}
