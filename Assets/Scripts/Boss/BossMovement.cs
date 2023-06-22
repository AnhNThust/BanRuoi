using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public DOTweenPath path;
	public float moveSpeed;
	public DG.Tweening.Core.TweenerCore<Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> tweenerCore;

	private void Start()
	{
		List<Vector3> points = path.wps;
		tweenerCore = transform.DOPath(points.ToArray(), moveSpeed, PathType.CatmullRom)
			.SetOptions(true)
			.SetLoops(-1)
			.SetSpeedBased()
			.SetEase(Ease.Linear);
	}
}
