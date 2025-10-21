using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 20f;
	[SerializeField] private float smoothTime = 0.25f;

	private Vector3 targetPosition;
	private Vector3 velocity = Vector3.zero;

	private void Start()
	{
		targetPosition = transform.position;
	}

	private void Update()
	{
		HandleInput();
		SmoothMove();
	}

	private void HandleInput()
	{
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");

		Vector3 movement = new Vector3(horizontal, vertical, 0f) * moveSpeed * Time.deltaTime;
		targetPosition += movement;
	}

	private void SmoothMove()
	{
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
	}
}
