using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeamMovement : MonoBehaviour
{
	public DOTweenPath path;
	public Transform[] target;
	public Transform parent;
	public float timeMove;
	List<Vector3> points;
	public void SetInfo(Transform[] _target, Transform _parent)
	{
		target = _target;
		parent = _parent;
		Move();
	}

	private void Move()
	{
		points = path.wps;
		transform.DOPath(points.ToArray(), timeMove, PathType.CatmullRom, PathMode.TopDown2D)
			.SetEase(Ease.Linear)
			.OnComplete(OnComplete);
	}

	public void OnComplete()
	{
		EnemyMovement[] enemies = transform.GetComponentsInChildren<EnemyMovement>();
		for (int i = 0; i < enemies.Length; i++)
		{
			enemies[i].transform.SetParent(parent);
			enemies[i].SetInfo(target[i]);
			MoveEnemy(enemies[i], target[i]);
		}

		Destroy(gameObject);
	}

	public void MoveEnemy(EnemyMovement enemy, Transform target)
	{
		var path = points.ConvertAll(_ => _);
		path.Add(target.position);

		var tweenMove = enemy.transform.DOPath(path.ToArray(), timeMove, PathType.CatmullRom, PathMode.TopDown2D)
			   .SetEase(Ease.Linear).OnComplete(enemy.MoveCompleted);
		tweenMove.GotoWaypoint(points.Count - 1, true);

	}
}
