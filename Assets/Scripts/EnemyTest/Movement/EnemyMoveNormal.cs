using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

public class EnemyMoveNormal : EnemyMoveBase
{
	private DOTweenPath pathStart;
	private float timeMoveStart;

	public DOTweenPath PathStart { get => pathStart; set => pathStart = value; }
	public float TimeMoveStart { get => timeMoveStart; set => timeMoveStart = value; }

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	protected override void Update()
	{
		base.Update();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	public void SetInfo(DOTweenPath pPathStart, float pTimeMoveStart)
	{
		pathStart = pPathStart;
		timeMoveStart = pTimeMoveStart;

		Move();
	}

	private void Move()
	{
		var points = PathStart.wps;
		TweenerCore = transform.DOPath(points.ToArray(), TimeMoveStart, PathType.CatmullRom, PathMode.TopDown2D)
			.SetEase(Ease.Linear)
			.SetLookAt(0.01f, transform.forward, Vector3.left)
			.OnComplete(() =>
			{
				transform.up = Vector3.up;
				Controller.IsReady = true;
			});
	}
}
