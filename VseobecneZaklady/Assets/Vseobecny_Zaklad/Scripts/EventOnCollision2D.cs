using UnityEngine;
using UnityEngine.Events;

public class EventOnCollision2D : MonoBehaviour
{
    public enum DetectionType { OnEnter, OnStay, OnExit }
    
    public DetectionType detectionType = DetectionType.OnEnter;

    public UnityEvent onCollision;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (detectionType == DetectionType.OnEnter) {
            onCollision.Invoke();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (detectionType == DetectionType.OnStay) {
            onCollision.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (detectionType == DetectionType.OnExit) {
            onCollision.Invoke();
        }
    }
}
