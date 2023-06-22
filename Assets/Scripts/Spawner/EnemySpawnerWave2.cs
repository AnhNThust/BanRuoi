using Assets.Scripts.Enum;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Manager;
using UnityEngine;

public class EnemySpawnerWave2 : MonoBehaviour
{
	[Header("Common")]
	public Transform holder;
	public Transform nextWave;
	public float timeSpawn;
	public Transform[] listItem;

	[Header("Group 1")]
	public Transform group1;
	public Stack<Transform> listPoint1;
	public Transform spawnPosition1;
	public int enemyID1;

	public int numSpawn1;
	public int counter1 = 0;

	public int numberItemSpawn1;
	public int[] listIndexHaveItem1;

	[Header("Group 2")]
	public Transform group2;
	public Stack<Transform> listPoint2;
	public Transform spawnPosition2;
	public int enemyID2;

	public int numSpawn2;
	public int counter2 = 0;

	public int numberItemSpawn2;
	public int[] listIndexHaveItem2;

	private void Start()
	{
		TakeListTarget(group1, out listPoint1);
		TakeListTarget(group2, out listPoint2);
		CreateListIndexHasItem(out listIndexHaveItem1);
		CreateListIndexHasItem(out listIndexHaveItem2);
		StartCoroutine(Spawn(counter1, enemyID1, spawnPosition1, listIndexHaveItem1, listPoint1, numSpawn1));
		StartCoroutine(Spawn(counter2, enemyID2, spawnPosition2, listIndexHaveItem2, listPoint2, numSpawn2));
		StartCoroutine(CheckHolderEmpty());
	}

	IEnumerator Spawn(int _counter, int _enemyID, Transform _spawnPos, int[] _listIndexHaveItem, 
		Stack<Transform> _listPoint, int _numSpawn)
	{
		while (_counter < _numSpawn)
		{
			yield return new WaitForSeconds(timeSpawn);

			GameObject enemyClone = PoolingManager.GetObject(_enemyID, _spawnPos.position, Quaternion.identity);
			enemyClone.transform.SetParent(holder);
			enemyClone.SetActive(true);

			if (_listIndexHaveItem.Contains(_counter))
			{
				Transform itemClone = GetRandomItem();
				enemyClone.GetComponent<EnemyDamageReceiver>().itemPrefab = itemClone;
			}

			enemyClone.GetComponent<EnemyMovement>().path = PathManager.GetPathByIndex(1);
			enemyClone.GetComponent<EnemyMovement>().SetInfo(_listPoint.Pop());
			_counter++;
		}
	}

	IEnumerator CheckHolderEmpty()
	{
		while (true)
		{
			yield return new WaitForSeconds(3f);
			if (holder.childCount <= 0)
			{
				UIManager.ShowWaveText(3);
				yield return new WaitForSeconds(2f);
				GameManager.CallWave(nextWave);
				yield return new WaitForSeconds(1f);
				gameObject.SetActive(false);
			}
		}
	}

	public int[] CreateListIndexHasItem(out int[] _listIndexHaveItem)
	{
		_listIndexHaveItem = new int[numberItemSpawn1];
		int index = 0;
		while (index < numberItemSpawn1)
		{
			int randIndex = Random.Range(0, numSpawn1 - 1);

			if (!_listIndexHaveItem.Contains(randIndex))
			{
				_listIndexHaveItem[index] = randIndex;
				index++;
			}
		}

		return _listIndexHaveItem;
	}

	public Transform GetRandomItem()
	{
		int randIndex = Random.Range(0, 5) switch {
			1 or 2 or 3 or 4 => 1,
			_ => 0
		};

		return listItem[randIndex];
	}

	public void TakeListTarget(Transform _group, out Stack<Transform> _listPoint)
	{
		_listPoint = new Stack<Transform>();
		for (int i = 0; i < _group.childCount; i++)
		{
			_listPoint.Push(_group.GetChild(i));
		}
	}
}
