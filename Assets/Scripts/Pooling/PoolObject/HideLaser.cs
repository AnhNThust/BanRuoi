using System.Collections;
using UnityEngine;

public class HideLaser : MonoBehaviour
{
	public float TimeHide;
	public Transform origin;
	public Transform Target;
	private LineRenderer laser;

	private void OnEnable()
	{
		laser = GetComponent<LineRenderer>();
		laser.SetPosition(0, origin.position - transform.position);
		laser.SetPosition(1, Target.position - transform.position);
		StartCoroutine(Hide());
	}

	IEnumerator Hide()
	{
		yield return new WaitForSeconds(TimeHide);
		PoolingManager.PoolObject(gameObject);
	}

	private void OnDisable()
	{
		TimeHide = 0.5f;
	}

	private void Update()
	{
		laser.SetPosition(0, origin.position - transform.position);
		laser.SetPosition(1, Target.position - transform.position);
	}
}
