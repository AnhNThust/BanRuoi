using UnityEngine;

public class CircleRotation : MonoBehaviour
{
	public float rotateSpeed;

	private void Update()
	{
		Vector3 targetRotation = transform.rotation.eulerAngles + new Vector3(0f, 0f, 20f);
		transform.rotation = Quaternion.Slerp(transform.rotation,
			Quaternion.Euler(targetRotation),
			rotateSpeed * Time.deltaTime);
	}
}
