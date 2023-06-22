using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Start : MonoBehaviour
{
	public DOTweenPath path;
	public float moveSpeed;
	public new SkeletonAnimation animation;
	public Transform hpTransform;

	private void Start()
	{
		List<Vector3> points = path.wps;
		_ = transform.DOPath(points.ToArray(), moveSpeed, PathType.CatmullRom).SetSpeedBased()
			.SetEase(Ease.Linear)
			.OnComplete(OnComplete);
	}

	public void OnComplete()
	{
		StartCoroutine(IEAction());
	}

	private IEnumerator IEAction()
	{
		GetComponent<BossDamageReceiver>().isReady = true;
		GetComponent<Boss_1_Attack>().enabled = true;
		GetComponent<BossMovement>().enabled = true;
		hpTransform.gameObject.SetActive(true);
		animation.AnimationState.SetAnimation(0, "idle", true);
		yield return null;
	}
}
