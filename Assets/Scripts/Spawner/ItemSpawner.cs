using Assets.Scripts.Enum;
using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
	public float timeSpawn;

	private void Start()
	{
		StartCoroutine(SpawnItem());
	}

	IEnumerator SpawnItem()
	{
		while (true)
		{
			yield return new WaitForSeconds(timeSpawn);
			//GameObject itemClone = PoolingManager.GetObject(ItemID.ITEM_UPGRADE, transform.position, Quaternion.identity);
			GameObject itemClone = PoolingManager.GetObject(ItemID.ITEM_POW, transform.position, Quaternion.identity);
			itemClone.SetActive(true);
		}
	}
}
