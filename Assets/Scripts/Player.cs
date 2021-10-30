using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D = null;
    [SerializeField] private float speed = 0.0f;

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
}