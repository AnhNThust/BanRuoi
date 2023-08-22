using System.Collections;
using UnityEngine;

public class BulletSpawnBullet : MonoBehaviour
{
	private int bulletId;
    private int numberBullet;
	private float coolDown;
	private float angle;

	private IEnumerator SpawnBulletChild()
	{
		yield return new WaitForSeconds(coolDown);
		
		for (int i = 0; i < numberBullet; i++)
		{
			Quaternion rot = Quaternion.Euler(0, 0, i * angle + angle);
			GameObject obj = PoolingManager.GetObject(bulletId, transform.position, rot);
			obj.SetActive(true);
		}

		PoolingManager.PoolObject(gameObject);
	}

	private void Start()
	{
		StartCoroutine(SpawnBulletChild());
	}

	public void SetInfo(int pBulletId, int pNumberBullet, float pCoolDown, float pAngle)
	{
		bulletId = pBulletId;
		numberBullet = pNumberBullet;
		coolDown = pCoolDown;
		angle = pAngle;
	}
}
