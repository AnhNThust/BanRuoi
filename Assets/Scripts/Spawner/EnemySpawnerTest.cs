using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerTest : MonoBehaviour
{
	[SerializeField] private DOTweenPath path;
	[SerializeField] private DOTweenPath newPath;
	[SerializeField] private Transform holder;

	[SerializeField] private int enemyId;
	[SerializeField] private int numberSpawn;
	[SerializeField] private float timeSpawn;
	[SerializeField] private bool isLoop;
	[SerializeField] private bool isChangePath;

	private int counter = 0;
	private GameObject target;
	private Vector3 spawnPosition;

	private void Start()
	{
		List<Vector3> points = path.wps;
		target = new GameObject();
		target.transform.position = points[^1];

		spawnPosition = transform.position;

		StartCoroutine(Spawn());
	}

	IEnumerator Spawn()
	{
		while (counter < numberSpawn)
		{
			yield return new WaitForSeconds(timeSpawn);

			// Logic spawn: 1. Spawn at path position
			GameObject enemy = PoolingManager.GetObject(enemyId, spawnPosition, Quaternion.identity);

			// 2: Set parent for enemy is path
			enemy.transform.SetParent(holder, true);
			enemy.SetActive(true);

			// 3: Set target for enemy
			enemy.GetComponent<EnemyMovementTest>().SetInfo(path, target.transform, newPath, isChangePath, isLoop);

			counter++;
		}
	}
}
