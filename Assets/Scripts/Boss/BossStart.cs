using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour
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
		//GetComponent<BossDamageReceiver>().isReady = true;
		//var track = animation.AnimationState.SetAnimation(0, "appear", false);
		//animation.AnimationState.SetAnimation(0, "idle", true);
	}

	private IEnumerator IEAction()
	{
		GetComponent<BossDamageReceiver>().isReady = true;
		GetComponent<BossAttack>().enabled = true;
		GetComponent<BossMovement>().enabled = true;
		hpTransform.gameObject.SetActive(true);
		var track = animation.AnimationState.SetAnimation(0, "appear", false);
		yield return new WaitForSeconds(track.Animation.duration);
		animation.AnimationState.SetAnimation(0, "idle", true);
	}
}
