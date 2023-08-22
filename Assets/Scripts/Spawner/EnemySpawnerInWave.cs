using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnerInWave : MonoBehaviour
{
	public Transform[] listItem;
	public DOTweenPath path;
	public Transform holder;
	public Transform group;
	[SerializeField] private CheckHolder checkHolder;

	public float timeSpawn;
	public int numSpawn;
	private int counter = 0;
	[SerializeField] private int enemyId;

	public int numberItemSpawn;
	public int[] listIndexHaveItem;
	private Stack<Transform> listPoint;

	private void Start()
	{
		checkHolder = GetComponentInParent<CheckHolder>();
		listPoint = TakeListTarget(group);
		listIndexHaveItem = CreateListIndexHasItem();

		StartCoroutine(Spawn());
	}

	/// <summary>
	/// Spawn from 2 position
	/// </summary>
	/// <returns></returns>
	private IEnumerator Spawn()
	{
		while (counter < numSpawn)
		{
			yield return new WaitForSeconds(timeSpawn);
			var enemyClone = PoolingManager.GetObject(enemyId, transform.position, Quaternion.identity);
			enemyClone.transform.SetParent(holder);
			enemyClone.SetActive(true);

			if (listIndexHaveItem.Contains(counter))
			{
				var itemClone = GetRandomItem();
				enemyClone.GetComponent<EnemyDamageReceiver>().itemPrefab = itemClone;
			}

			enemyClone.GetComponent<EnemyMovement>().path = path;
			enemyClone.GetComponent<EnemyMovement>().SetInfo(listPoint.Pop());

			// Enable Check Holder
			if (!checkHolder.enabled && counter == numSpawn - 1)
				checkHolder.enabled = true;

			counter++;
		}
	}

	private int[] CreateListIndexHasItem()
	{
		var listIndex = new int[numberItemSpawn];
		var index = 0;
		while (index < numberItemSpawn)
		{
			var randIndex = Random.Range(0, numSpawn - 1);

			if (listIndex.Contains(randIndex)) continue;
			listIndex[index] = randIndex;
			index++;
		}

		return listIndex;
	}

	private Transform GetRandomItem()
	{
		var randIndex = Random.Range(0, listItem.Length - 1);
		return listItem[randIndex];
	}

	private Stack<Transform> TakeListTarget(Transform pGroup)
	{
		var lstPoint = new Stack<Transform>();
		for (var i = 0; i < pGroup.childCount; i++)
		{
			lstPoint.Push(pGroup.GetChild(i));
		}

		return lstPoint;
	}
}