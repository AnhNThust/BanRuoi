using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
	private Vector3 originPosition;

	private void Start()
	{
		originPosition = transform.position;
	}

	private void Update()
	{
		transform.localPosition = originPosition;
		transform.rotation = Quaternion.identity;
	}
}
