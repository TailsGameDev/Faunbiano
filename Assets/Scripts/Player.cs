using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D = null;
    [SerializeField] private float speed = 0.0f;

    [SerializeField] private Transform handTransform = null;
    private TransformWrapper handTransformWrapper;
    [SerializeField] private Transform dropPosition = null;
    // NOTE seedsInRange code is duplicated in Tree.cs
    private List<Seed> seedsInRange = new List<Seed>();
    private Seed seedInHand;
    private TransformWrapper nullTransformWrapper;

    private void Awake()
    {
        handTransformWrapper = new TransformWrapper(handTransform);
        nullTransformWrapper = new TransformWrapper(null);
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float VerticalInput = Input.GetAxisRaw("Vertical");
        Vector3 movementDirection;
        if (Mathf.Abs(horizontalInput) + Mathf.Abs(VerticalInput) > 0.0f)
        {
            movementDirection = (Vector3.up * VerticalInput + Vector3.right * horizontalInput).normalized;
        }
        else
        {
            movementDirection = Vector3.zero;
        }
        rb2D.AddForce(speed * movementDirection, ForceMode2D.Force);
    }

    private void Update()
    {
        bool pickUpInput = Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0);
        if (pickUpInput && seedInHand != null)
        {
            /* // Drop seed
            Seed droppedSeed = seedInHand;
            droppedSeed.TransformWrapper.SetParent(nullTransformWrapper);
            droppedSeed.TransformWrapper.Position = dropPosition.position;
            droppedSeed.Collider.enabled = true;
            droppedSeed.RB2D.isKinematic = false;
            */

            Instantiate(seedInHand.TreeToPlantPrototype, 
                            dropPosition.position, Quaternion.identity);
            Destroy(seedInHand.gameObject);

            seedInHand = null;
        }
        else if (pickUpInput && seedInHand == null && seedsInRange.Count > 0)
        {
            // PickUp seed
            if (seedInHand == null)
            {
                Seed nextSeed = seedsInRange[0];
                nextSeed.RB2D.isKinematic = true;
                nextSeed.Collider.enabled = false;
                nextSeed.TransformWrapper.SetParent(handTransformWrapper);
                nextSeed.TransformWrapper.LocalPosition = Vector3.zero;
                nextSeed.RB2D.velocity = Vector3.zero;
                nextSeed.PickedUp = true;

                seedInHand = nextSeed;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == Seed.SeedTag)
        {
            GameObject seedScriptsNode = col.GetComponent<CollisionHandler>().ScriptsHolder;
            Seed seed = seedScriptsNode.GetComponent<Seed>();

            if (!seedsInRange.Contains(seed))
            {
                seedsInRange.Add(seed);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == Seed.SeedTag)
        {
            GameObject seedScriptsNode = col.GetComponent<CollisionHandler>().ScriptsHolder;
            Seed seed = seedScriptsNode.GetComponent<Seed>();

            seedsInRange.Remove(seed);
        }
    }
}