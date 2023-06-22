using Assets.Scripts.Enum;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Manager;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	public Stack<Transform> listPoint;
	public Transform[] listItem;
	public Transform holder;

	public Transform nextWave;

	public float timeSpawn;
	public int numSpawn;
	public int counter = 0;

	public int numberItemSpawn;
	public int[] listIndexHaveItem;

	private void Start()
	{
		TakeListTarget();
		CreateListIndexHasItem();
		StartCoroutine(Spawn());
		StartCoroutine(CheckHolderEmpty());
	}

	IEnumerator Spawn()
	{
		while (counter < numSpawn)
		{
			yield return new WaitForSeconds(timeSpawn);

			Vector3 pos = GetRandomPos();
			GameObject enemyClone = PoolingManager.GetObject(EnemyID.ENEMY_1, pos, Quaternion.identity);
			enemyClone.transform.SetParent(holder);
			enemyClone.SetActive(true);

			if (listIndexHaveItem.Contains(counter))
			{
				Transform itemClone = GetRandomItem();
				enemyClone.GetComponent<EnemyDamageReceiver>().itemPrefab = itemClone;
			}

			enemyClone.GetComponent<EnemyMovement>().path = PathManager.GetPathByIndex(0);
			enemyClone.GetComponent<EnemyMovement>().SetInfo(listPoint.Pop());
			counter++;
		}
	}

	IEnumerator CheckHolderEmpty()
	{
        while (true)
        {
			yield return new WaitForSeconds(3f);
			if (holder.childCount <= 0)
			{
				UIManager.ShowWaveText(2);
				yield return new WaitForSeconds(2f);
				GameManager.CallWave(nextWave);
				yield return new WaitForSeconds(1f);
				gameObject.SetActive(false);
			}
		}
    }

	public Vector3 GetRandomPos()
	{
		int random = Random.Range(0, transform.childCount);
		return transform.GetChild(random).position;
	}

	public int[] CreateListIndexHasItem()
	{
		listIndexHaveItem = new int[numberItemSpawn];
		int index = 0;
		while (index < numberItemSpawn)
		{
			int randIndex = Random.Range(0, numSpawn - 1);
			
			if (!listIndexHaveItem.Contains(randIndex))
			{
				listIndexHaveItem[index] = randIndex;
				index++;
			}
		}

		return listIndexHaveItem;
	}

	public Transform GetRandomItem()
	{
		int randIndex = Random.Range(0, 5);
		switch (randIndex)
		{
			case 1:
			case 2:
			case 3:
			case 4:
				randIndex = 1;
				break;
			default:
				randIndex = 0;
				break;
		}

		return listItem[randIndex];
	}

	public void TakeListTarget()
	{
		listPoint = new Stack<Transform>();
		Transform group = GroupManager.Instance.GetGroupByIndex(0);
		for (int i = 0; i < group.childCount; i++)
		{
			listPoint.Push(group.GetChild(i));
		}
	}
}
