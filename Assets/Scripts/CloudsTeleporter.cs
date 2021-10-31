using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsTeleporter : MonoBehaviour
{

    [SerializeField] private Transform teleportPoint = null;

    [SerializeField] private Rigidbody2D[] clouds = null;

    [SerializeField] private float cloudsMinSpeed = 0.0f;
    [SerializeField] private float cloudsMaxSpeed = 0.0f;

    private void Awake()
    {
        for (int c = 0; c < clouds.Length; c++)
        {
            clouds[c].velocity = Vector3.right * Random.Range(cloudsMinSpeed, cloudsMaxSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Cloud")
        {
            col.transform.position = teleportPoint.position;
        }
    }
}
