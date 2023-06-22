using UnityEngine;

public class BulletMovement : MonoBehaviour
{
	public float moveSpeed;

	private void FixedUpdate()
	{
		transform.Translate(moveSpeed * Time.fixedDeltaTime * Vector3.up);
	}
}
