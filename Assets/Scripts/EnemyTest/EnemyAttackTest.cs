using Assets.Scripts.Enum;
using System.Collections;
using UnityEngine;

public class EnemyAttackTest : MonoBehaviour
{
    public Transform barrel1;
	public Transform barrel2;
	public float timeSpawn;
    private int bulletId1;
	private int bulletId2;

	IEnumerator Attack1()
	{
		while (true)
		{
			GameObject bullet = PoolingManager.GetObject(bulletId1, barrel1.position, barrel1.rotation);
			bullet.SetActive(true);
			yield return new WaitForSeconds(timeSpawn);
		}
	}

	IEnumerator Attack2()
	{
		while (true)
		{
			barrel2.rotation = barrel2.rotation == Quaternion.Euler(0, 0, 0) ?
				Quaternion.Euler(0, 0, 45f) :
				Quaternion.Euler(0, 0, 0);

			for (int i = 0; i < barrel2.childCount; i++)
			{
				GameObject bullet = PoolingManager.GetObject(bulletId2, barrel2.GetChild(i).position, barrel2.GetChild(i).rotation);
				bullet.SetActive(true);
			}

			yield return new WaitForSeconds(timeSpawn * 2);
		}
	}

	private void Start()
	{
		bulletId1 = BulletID.ENEMY2_BULLET;
		bulletId2 = BulletID.ENEMY_WAVE_BULLET;

		StartCoroutine(Attack1());
		StartCoroutine(Attack2());
	}
}
