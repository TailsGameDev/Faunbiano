using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum State
    {
        WALK_TO_MOTHER_TREE,
        WANDER,
        ATTACK,
    }

    private State currentState;

    [SerializeField] private Rigidbody2D rb2D = null;
    [SerializeField] private float speed = 0.0f;
    [SerializeField] private float cooldownToChangeDirection = 0.0f;
    [SerializeField] private List<Treee.TreeTypeEnum> treeTypesToIgnoreInPathfinding = null;

    private Vector3[] wanderDirections;
    private Vector3 wanderDirection = Vector3.zero;
    private float timeToEndCurrentWandering;
    private TransformWrapper transformWrapper;

    [SerializeField] private float attacksCooldown = 0.0f;
    private float timeToAttack;
    private Treee treeToChop;
    private Damager damager = null;

    [SerializeField] private Damageable damageable = null;

    public static readonly string EnemyTag = "Enemy";

    public TransformWrapper TransformWrapper { get => transformWrapper; }
    public Treee TreeToChop { set => treeToChop = value; }
    public Damager Damager { set => damager = value; }
    public Damageable Damageable { get => damageable; }

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
                    rb2D.isKinematic = false;
                    rb2D.AddForce(GetMotherTreeDirection() * speed * Time.deltaTime, ForceMode2D.Force);

                    if (this.treeToChop != null)
                    {
                        nextState = State.ATTACK;
                    }
                    else
                    {
                        DecideIfToWanderOrPursueMotherTree(ref nextState);
                    }
                    break;
                }
            case State.WANDER:
                {
                    rb2D.isKinematic = false;
                    rb2D.AddForce(this.wanderDirection * speed * Time.deltaTime, ForceMode2D.Force);

                    if (this.treeToChop != null)
                    {
                        nextState = State.ATTACK;
                    }
                    else if (Time.time > timeToEndCurrentWandering)
                    {
                        DecideIfToWanderOrPursueMotherTree(ref nextState);
                    }
                    break;
                }
            case State.ATTACK:
                {
                    rb2D.isKinematic = true;
                    rb2D.velocity = Vector3.zero;

                    if (treeToChop == null)
                    {
                        DecideIfToWanderOrPursueMotherTree(ref nextState);
                    }
                    else if (Time.time > timeToAttack)
                    {
                        timeToAttack = Time.time + attacksCooldown;

                        damager.DealDamage(treeToChop.Damageable);
                        treeToChop.Damageable.TakeDamage(damager);
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
    private void DecideIfToWanderOrPursueMotherTree(ref State nextState)
    {
        // Change state to wander if needed
        if (!IsThereObstacleInTheWayToMotherTree())
        {
            nextState = State.WALK_TO_MOTHER_TREE;
        }
        else
        {
            // Then let's wander a little bit
            nextState = State.WANDER;

            int randomIndex = Random.Range(minInclusive: 0, maxExclusive: wanderDirections.Length);
            this.wanderDirection = wanderDirections[randomIndex];

            timeToEndCurrentWandering = Time.time + cooldownToChangeDirection;
        }
    }
    private bool IsThereObstacleInTheWayToMotherTree()
    {
        // Check if have hit something
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

        // Check if TreeType is an obstable
        bool isThereAnObstacleInTheWay = false;
        if (isThereSomethingInTheWay)
        {
            CollisionHandler collisionHandler = raycastHit.collider.GetComponent<CollisionHandler>();
            if (collisionHandler != null)
            {
                Treee tree = collisionHandler.ScriptsHolder.GetComponent<Treee>();
                if (tree != null)
                {
                    isThereAnObstacleInTheWay = !treeTypesToIgnoreInPathfinding.Contains(tree.TreeType);
                }
            }
        }

        return isThereAnObstacleInTheWay;
    }
}
