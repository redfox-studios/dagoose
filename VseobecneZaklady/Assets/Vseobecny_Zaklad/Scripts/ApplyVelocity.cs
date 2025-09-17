using UnityEngine;

public class ApplyVelocity : MonoBehaviour
{
    public enum Mode { Once, Constantly }

    public Mode mode;
    public float angle;
    public float speed;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("ApplyVelocity requires that the gameobject its attached to has a Rigidbody2D!");
            return;
        }
    }

    private void Start()
    {
        if (mode == Mode.Once)
        {
            Vector2 direction = DirectionFromAngle(angle);

            rb.linearVelocity = direction * speed;
        }
    }

    private void FixedUpdate()
    {
        if (mode == Mode.Constantly) 
        {
            Vector2 direction = DirectionFromAngle(angle);

            rb.linearVelocity = direction * speed;
        }
    }


    private Vector2 DirectionFromAngle(float angleDegrees)
    {
        float x = Mathf.Cos(angleDegrees * Mathf.Deg2Rad);
        float y = Mathf.Sin(angleDegrees * Mathf.Deg2Rad);

        Vector2 direction = new Vector2(x, y);
        direction.Normalize();

        return direction;
    }
}
