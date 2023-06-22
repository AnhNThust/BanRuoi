using System.Collections;
using UnityEngine;

public class HideText : MonoBehaviour
{
	private void OnEnable()
	{
		StartCoroutine(Hiding());
	}

	IEnumerator Hiding()
	{
		yield return new WaitForSeconds(1f);
		gameObject.SetActive(false);
	}
}
