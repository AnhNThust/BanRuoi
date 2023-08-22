using DG.Tweening;
using DG.Tweening.Core;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveChangePath : EnemyMoveBase
{
	[Header("In Runtime")]
	[SerializeField] private DOTweenPath pathStart;
	[SerializeField] private float timeMoveStart;
	[SerializeField] private DOTweenPath path;
	[SerializeField] private float timeMove;

	public DOTweenPath PathStart { get => pathStart; set => pathStart = value; }
	public float TimeMoveStart { get => timeMoveStart; set => timeMoveStart = value; }
	public DOTweenPath Path { get => path; set => path = value; }
	public float TimeMove { get => timeMove; set => timeMove = value; }

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

	public void SetInfo(DOTweenPath pPathStart, float pTimeMoveStart, DOTweenPath pPath, float pTimeMove, bool pIsRotate)
	{
		PathStart = pPathStart;	
		TimeMoveStart = pTimeMoveStart;
		path = pPath;
		timeMove = pTimeMove;

		MoveChangePath(pIsRotate);
	}

	private void MoveChangePath(bool pIsRotate)
	{
		var points = PathStart.wps;
		TweenerCore = transform.DOPath(points.ToArray(), TimeMoveStart, PathType.CatmullRom, PathMode.TopDown2D)
			.SetEase(Ease.Linear)
			.SetLookAt(0.01f, transform.forward, Vector3.left)
			.OnComplete(pIsRotate ? OnComplete : OnCompleteDontRotate);
	}

	private void OnComplete()
	{
		Controller.IsReady = true;
		List<Vector3> newPoints = Path.wps;
		TweenerCore = transform.DOPath(newPoints.ToArray(), TimeMove, PathType.CatmullRom, PathMode.TopDown2D)
			.SetOptions(true)
			.SetLoops(-1, LoopType.Restart)
			.SetEase(Ease.Linear)
			.SetLookAt(0.01f, transform.forward, Vector3.left);
	}

	private void OnCompleteDontRotate()
	{
		Controller.IsReady = true;
		List<Vector3> newPoints = Path.wps;
		TweenerCore = transform.DOPath(newPoints.ToArray(), TimeMove, PathType.CatmullRom, PathMode.TopDown2D)
			.SetOptions(true)
			.SetLoops(-1, LoopType.Restart)
			.SetEase(Ease.Linear);
	}
}
