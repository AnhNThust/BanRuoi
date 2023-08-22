using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroupAttackTest : MonoBehaviour
{
	[SerializeField] private Transform group;
	[SerializeField] private List<Transform> listPosition;
	[SerializeField] private Transform player;
	[SerializeField] private int[] listRandIndexOfEnemy;
	//[SerializeField] private int[] listIdPosition;

	private int counter = 0;

	private IEnumerator EnemyAssemble()
	{
		yield return new WaitUntil(() => transform.childCount >= 15);

		while (counter < 4)
		{

			listPosition = new List<Transform>();
			for (int i = 0; i < group.childCount; i++)
			{
				listPosition.Add(group.GetChild(i));
			}

			yield return new WaitForSeconds(6f);
			if (transform.childCount <= 10) break;

			//GetRandomEnemies();
			listRandIndexOfEnemy = new int[group.childCount];
			int count1 = 0;
			while (count1 < group.childCount)
			{
				int randIdEnemy = Random.Range(0, transform.childCount);
				if (listRandIndexOfEnemy.Contains(randIdEnemy)) continue;

				listRandIndexOfEnemy[count1] = randIdEnemy;

				count1++;
				//if (count1 >= group.childCount) break;
			}

			//listIdPosition = new int[group.childCount];
			//int count2 = 0;
			//while (count2 < group.childCount)
			//{
			//	int idPosition = Random.Range(0, listPosition.Count);
			//	if (listIdPosition.Contains(idPosition)) continue;

			//	listIdPosition[count2] = idPosition;

			//	count2++;
			//	//if (count2 >= group.childCount) break;
			//}

			yield return new WaitForSeconds(1f);

			for (int i = 0; i < group.childCount; i++)
			{
				var points = new List<Vector3>()
				{
					listPosition[i].localPosition
				};

				if (transform.GetChild(listRandIndexOfEnemy[i]) == null) continue;

				Destroy(transform.GetChild(listRandIndexOfEnemy[i])
					.GetComponent<EnemyMoveToGroup>());

				transform.GetChild(listRandIndexOfEnemy[i])
					.GetComponent<EnemyControllerTest>().IsReady = false;

				transform.GetChild(listRandIndexOfEnemy[i])
					.DOPath(points.ToArray(), 1f, PathType.Linear, PathMode.TopDown2D);
				//listPosition.RemoveAt(i);
			}

			yield return new WaitForSeconds(3f);

			//GameObject groupAttack = new("GroupAttack");
			//Array.Reverse(listRandIndexOfEnemy);
			//for (int i = 0; i < listPosition.Count; i++)
			//{
			//	transform.GetChild(listRandIndexOfEnemy[i])
			//		.transform.SetParent(groupAttack.transform);

			//	if (groupAttack.transform.position != Vector3.zero)
			//	{
			//		groupAttack.transform.position = Vector3.zero;
			//	}

			//	//transform.GetChild(listRandIndexOfEnemy[i]).transform.localPosition = group.GetChild(i).position;
			//}

			//yield return new WaitForSeconds(2f);

			//var wp = new Vector3[]
			//{
			//	new Vector3(0, -10, 0)
			//};
			//groupAttack.transform.DOPath(wp, 2, PathType.Linear, PathMode.TopDown2D)
			//	.OnComplete(() =>
			//	{
			//		//for (int i = 0; i < groupAttack.transform.childCount; i++)
			//		//{
			//		//	PoolingManager.PoolObject(groupAttack.transform.GetChild(i).gameObject);
			//		//}
			//		Destroy(groupAttack);
			//	});

			Array.Reverse(listRandIndexOfEnemy);
			for (int i = 0; i < group.childCount; i++)
			{
				Transform enemy = transform.GetChild(listRandIndexOfEnemy[i]);
				var points = new Vector3[]
				{
				new Vector3(enemy.position.x, -10, 0)
				};

				transform.GetChild(listRandIndexOfEnemy[i]).DOPath(points, 2f, PathType.Linear, PathMode.TopDown2D)
					.OnComplete(() =>
					{
						PoolingManager.PoolObject(enemy.gameObject);
					});
			}

			counter++;
		}
	}

	private void Start()
	{
		listPosition = new List<Transform>();
		for (int i = 0; i < group.childCount; i++)
		{
			listPosition.Add(group.GetChild(i));
		}
		StartCoroutine(EnemyAssemble());
	}

	private void GetRandomEnemies()
	{
		listRandIndexOfEnemy = new int[group.childCount];
		for (int i = 0; i < group.childCount; i++)
		{
			int randIndex = Random.Range(0, transform.childCount);
			listRandIndexOfEnemy[i] = randIndex;
			int indexPosition = Random.Range(0, listPosition.Count);

			var points = new List<Vector3>()
			{
				listPosition[indexPosition].localPosition
			};

			Destroy(transform.GetChild(randIndex).GetComponent<EnemyMoveToGroup>());
			transform.GetChild(randIndex).DOPath(points.ToArray(), 1f, PathType.Linear, PathMode.TopDown2D);
			listPosition.RemoveAt(indexPosition);
		}

		for (int i = 0; i < group.childCount; i++)
		{
			Transform enemy = transform.GetChild(listRandIndexOfEnemy[i]);
			var points = new Vector3[]
			{
				new Vector3(enemy.position.x, -10, 0)
			};

			transform.GetChild(listRandIndexOfEnemy[i]).DOPath(points, 2f, PathType.Linear, PathMode.TopDown2D)
				.OnComplete(() =>
				{
					PoolingManager.PoolObject(enemy.gameObject);
				});
		}
	}
}
