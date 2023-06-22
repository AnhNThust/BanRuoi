using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementTest : MonoBehaviour
{
	public DOTweenPath Path { get; set; }
	public DOTweenPath NewPath { get; set; }
	public Transform Target { get; set; }

	[SerializeField] private float timeMove;
	[SerializeField] private Transform bodyTransform;

	[SerializeField] private Transform ringEfx;
	[SerializeField] private Transform healEfx;
	[SerializeField] private EnemyAttackTest attack;

	private void Start()
	{
		attack = GetComponent<EnemyAttackTest>();
	}

	public void SetInfo(DOTweenPath pPath, Transform pTarget, DOTweenPath pNewPath, bool pIsChangePath, bool pIsLoop)
	{
		Target = pTarget;
		Path = pPath;

		if (pIsLoop)
		{
			MoveLoop();
		}

		if (pIsChangePath)
		{
			NewPath = pNewPath;
			MoveChangePath();
		}

		if (!pIsLoop && !pIsChangePath)
		{
			Move();
		}
	}

	private void Update()
	{
		// For rotate body, identity this transform and health bar 
		bodyTransform.localRotation = transform.rotation;
		transform.rotation = Quaternion.identity;
	}

	private void Move()
	{
		var points = Path.wps;
		transform.DOPath(points.ToArray(), timeMove, PathType.CatmullRom, PathMode.TopDown2D)
			.SetEase(Ease.Linear)
			.SetLookAt(0.01f, transform.forward, Vector3.left)
			.OnComplete(() =>
			{
				transform.up = Vector3.up;

				ringEfx.gameObject.SetActive(true);
				healEfx.gameObject.SetActive(true);
				attack.enabled = true;
			});
	}

	private void MoveChangePath()
	{
		var points = Path.wps;
		transform.DOPath(points.ToArray(), timeMove, PathType.CatmullRom, PathMode.TopDown2D)
			.SetEase(Ease.Linear)
			.SetLookAt(0.01f, transform.forward, Vector3.left)
			.OnComplete(OnComplete);
	}

	private void MoveLoop()
	{
		var points = Path.wps;
		transform.DOPath(points.ToArray(), timeMove, PathType.CatmullRom, PathMode.TopDown2D)
			.SetLoops(-1, LoopType.Restart)
			.SetEase(Ease.Linear)
			.SetLookAt(0.01f, transform.forward, Vector3.left);
	}

	private void OnComplete()
	{
		List<Vector3> newPoints = NewPath.wps;
		transform.DOPath(newPoints.ToArray(), timeMove, PathType.CatmullRom, PathMode.TopDown2D)
			.SetOptions(true)
			.SetLoops(-1, LoopType.Restart)
			.SetEase(Ease.Linear)
			.SetLookAt(0.01f, transform.forward, Vector3.left);

		//transform.up = Vector3.up;
		//if (transform.position == target.position && GetComponent<EnemyAttack>() != null)
		//{
		//	GetComponent<EnemyAttack>().enabled = true;
		//}
	}
}
