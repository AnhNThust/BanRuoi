using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEnd : MonoBehaviour
{
	public DOTweenPath path;
	public Vector3 beginPos;

	private void Start()
	{
		beginPos = new Vector3(0f, -6.8f, 0f);
		StartCoroutine(ShipAction());
	}

	IEnumerator ShipAction()
	{
		yield return new WaitForSeconds(1f);
		GetComponent<ShipAttackBase>().enabled = false;
		yield return new WaitForSeconds(3f);
		List<Vector3> points = path.wps;
		_ = transform.DOPath(points.ToArray(), 2f, PathType.CatmullRom)
			.SetEase(Ease.Linear)
			.OnComplete(OnComplete);
	}

	private void OnComplete()
	{
		transform.position = beginPos;
		UIManager.ShowVicPanel();
		Time.timeScale = 0;
		this.enabled = false;
	}
}
