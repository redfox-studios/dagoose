using UnityEngine;

public class FlipOnMove : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Flip Settings")]
    [SerializeField] private bool useFlipX = true;
    [SerializeField] private bool invertFlip = false;
    [SerializeField] private float flipThreshold = 0.01f;

    [Header("Movement Source")]
    [SerializeField] private Rigidbody2D rb;

    private Transform targetTransform;
    private Vector3 lastPosition;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        targetTransform = transform;
        lastPosition = targetTransform.position;
    }

    private void LateUpdate()
    {
        float direction = GetMovementDirection();

        if (Mathf.Abs(direction) > flipThreshold)
        {
            ApplyFlip(direction);
        }
    }

    private float GetMovementDirection()
    {
        if (rb != null)
        {
            return rb.linearVelocity.x;
        }
        else
        {
            float direction = targetTransform.position.x - lastPosition.x;
            lastPosition = targetTransform.position;
            return direction;
        }
    }

    private void ApplyFlip(float direction)
    {
        bool shouldFlip = direction < 0;

        if (invertFlip)
            shouldFlip = !shouldFlip;

        if (useFlipX)
        {
            spriteRenderer.flipX = shouldFlip;
        }
        else
        {
            spriteRenderer.flipY = shouldFlip;
        }
    }
}
