using UnityEngine;

public class BulletScale : MonoBehaviour
{
	private Vector3 newScale;

	private void Start()
	{
		newScale = transform.localScale + new Vector3(0.8f, 0.8f, 0.8f);
	}

	private void Update()
	{
		transform.localScale = Vector3.Slerp(transform.localScale, newScale, Time.deltaTime);
	}
}
