using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStart : MonoBehaviour
{
	public DOTweenPath path;

	private void OnEnable()
	{
		List<Vector3> points = path.wps;
		_ = transform.DOPath(points.ToArray(), 2f, PathType.CatmullRom)
			.SetEase(Ease.Linear)
			.OnComplete(OnComplete);
	}

	private void OnComplete()
	{
		StartCoroutine(ShipAction());
	}

	IEnumerator ShipAction()
	{
		yield return new WaitForSeconds(1f);
		GetComponent<ShipAttackBase>().enabled = true;
	}
}
