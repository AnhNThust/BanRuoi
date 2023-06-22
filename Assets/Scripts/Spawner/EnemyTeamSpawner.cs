using Assets.Scripts.Enum;
using System.Collections;
using UnityEngine;

public class EnemyTeamSpawner : MonoBehaviour
{
	[SerializeField] private int numberTeam;
	[SerializeField] private int counter = 0;
	[SerializeField] private float timeSpawn;

	private void Start()
	{
		StartCoroutine(SpawnTeam());
	}

	IEnumerator SpawnTeam()
	{
		while (counter < numberTeam)
		{
			yield return new WaitForSeconds(timeSpawn);
			counter++;
			GameObject teamClone = PoolingManager.GetObject(EnemyTeamID.ENEMY_TEAM_1, transform.position, Quaternion.identity);
			teamClone.SetActive(true);
		}
	}
}
