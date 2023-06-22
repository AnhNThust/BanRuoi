using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Manager;
using UnityEngine;

public class EnemySpawnerWave3 : MonoBehaviour
{
	public Transform group;
	public Stack<Transform> listPoint;

	public Transform spawnPosition;
	public Transform holder;
	public Transform nextWave;

	public int enemyTeamID;
	public float timeSpawn;
	public int numSpawn;
	public int counter = 0;

	public Transform[] listItem;
	public int numberItemSpawn;
	public int counterItem = 0;
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

			GameObject enemyTeamClone = PoolingManager.GetObject(enemyTeamID, spawnPosition.position, Quaternion.identity);
			enemyTeamClone.transform.SetParent(holder);
			enemyTeamClone.SetActive(true);

			Transform[] lp = new Transform[2];
			for (int i = 0; i < 2; i++)
			{
				if (listIndexHaveItem.Contains(2 * counter + i))
				{
					Transform itemClone = GetRandomItem();
					enemyTeamClone.transform.GetChild(i)
						.GetComponent<EnemyDamageReceiver>().itemPrefab = itemClone;
				}

				lp[i] = listPoint.Pop();
			}

			enemyTeamClone.GetComponent<EnemyTeamMovement>().path = PathManager.GetPathByIndex(2);
			enemyTeamClone.GetComponent<EnemyTeamMovement>().SetInfo(lp, holder);
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
				UIManager.ShowWarningUI();
				GameManager.CallWave(nextWave);
				yield return new WaitForSeconds(1f);
				gameObject.SetActive(false);
			}
		}
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
		int randIndex = Random.Range(0, 12);

		switch (randIndex)
		{
			case 1:
			case 10:
			case 11:
				randIndex = 1;
				break;
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
				randIndex = 2;
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
		for (int i = 0; i < group.childCount; i++)
		{
			listPoint.Push(group.GetChild(i));
		}
	}
}
