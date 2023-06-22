using UnityEngine;

public class HomingMissle : MonoBehaviour
{
	public Transform target;
	public Rigidbody2D rb;

	public float angleChangingSpeed;
	public float movementSpeed;

	private void FixedUpdate()
	{
		Vector2 direction = (Vector2)target.position - rb.position;
		direction.Normalize();
		float rotateAmount = Vector3.Cross(direction, transform.up).z;
		rb.angularVelocity = -angleChangingSpeed * rotateAmount;
		rb.velocity = transform.up * movementSpeed;

		// Degree angle changing speed
		if (angleChangingSpeed > 0 && transform.position.y <= 1)
		{
			angleChangingSpeed = 0;
		}
	}

	private void OnDisable()
	{
		angleChangingSpeed = 120;
		transform.rotation = Quaternion.identity;
	}
}
