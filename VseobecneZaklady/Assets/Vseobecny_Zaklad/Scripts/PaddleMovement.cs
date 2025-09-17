using System;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("PaddleMovement requires that the gameobject its attached to has a Rigidbody2D!");
            return;
        }
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void FixedUpdate()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 position = rb.position;
        position.x = mouseWorldPos.x;

        //rb.MovePosition(position);
        rb.position = position;
    }
}
