using System.Collections;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    [SerializeField] private float timeHide;

	public float TimeHide { get => timeHide; set => timeHide = value; }

	private void OnEnable()
	{
		StartCoroutine(Hiding());
	}

	IEnumerator Hiding()
	{
		yield return new WaitForSeconds(TimeHide);

		PoolingManager.PoolObject(gameObject);
	}
}
