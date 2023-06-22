using System.Collections;
using UnityEngine;

public class LaserTargeting : MonoBehaviour
{
	public Vector3 endScale;
	public float speedScale;
	public float timeHide;

	public Transform Target { get; set; }

	IEnumerator HideTargeting()
	{
		yield return new WaitForSeconds(timeHide);
		PoolingManager.PoolObject(gameObject);
	}

	private void OnEnable()
	{
		endScale = new Vector3(0.25f, 0.25f, 0.25f);

		StartCoroutine(HideTargeting());
	}

	private void OnDisable()
	{
		transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
	}

	private void Update()
	{
		if (transform.localScale != endScale)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, endScale, speedScale * Time.deltaTime);
		}

		transform.position = Target.position;
	}
}
