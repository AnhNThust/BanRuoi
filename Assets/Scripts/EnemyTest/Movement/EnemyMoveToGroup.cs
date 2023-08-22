using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveToGroup : EnemyMoveBase
{
	[Header("In Runtime")]
	[SerializeField] private DOTweenPath pathStart;
	[SerializeField] private Transform pointInGroup;
	[SerializeField] private float timeMoveStart;

	public DOTweenPath PathStart { get => pathStart; set => pathStart = value; }
	public Transform PointInGroup { get => pointInGroup; set => pointInGroup = value; }
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

	public void SetInfo(DOTweenPath pPathStart, Transform pPointInGroup, float pTimeMoveStart)
    {
		pathStart = pPathStart;
		pointInGroup = pPointInGroup;
		timeMoveStart = pTimeMoveStart;
		MoveToGroup();
	}

	private void MoveToGroup()
	{
		var points = new List<Vector3>(pathStart.wps)
		{
			pointInGroup.position
		};
		TweenerCore = transform.DOPath(points.ToArray(), TimeMoveStart, PathType.CatmullRom, PathMode.TopDown2D)
			.SetLoops(0)
			.SetEase(Ease.Linear)
			.SetLookAt(0.01f, transform.forward, Vector3.left)
			.OnComplete(() =>
			{
				transform.up = Vector3.up;
				Controller.IsReady = true;
			});
	}
}
