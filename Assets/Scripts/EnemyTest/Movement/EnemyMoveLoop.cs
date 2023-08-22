using DG.Tweening;
using UnityEngine;

public class EnemyMoveLoop : EnemyMoveBase
{
	[Header("In Runtime")]
	[SerializeField] private DOTweenPath pathStart;
	[SerializeField] private float timeMove;

	public DOTweenPath PathStart { get => pathStart; set => pathStart = value; }
	public float TimeMove { get => timeMove; set => timeMove = value; }

	protected override void OnEnable()
	{
		base.OnEnable();
		Controller.IsReady = true;
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
		PathStart = pPathStart;
		TimeMove = pTimeMoveStart;
		Move();
	}

	private void Move()
	{
		var points = PathStart.wps;
		TweenerCore = transform.DOPath(points.ToArray(), timeMove, PathType.CatmullRom, PathMode.TopDown2D)
			.SetLoops(-1, LoopType.Restart)
			.SetEase(Ease.Linear)
			.SetLookAt(0.01f, transform.forward, Vector3.left);
	}
}
