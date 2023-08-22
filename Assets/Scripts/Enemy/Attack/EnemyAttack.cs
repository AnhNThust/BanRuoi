using Assets.Scripts.Enum;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAttack : MonoBehaviour
{
	public Transform alertAttack;

	private void Start()
	{
		StartCoroutine(Attack());
	}

	IEnumerator Attack()
	{
		while (true)
		{
			float delay = Random.Range(2, 4);
			yield return new WaitForSeconds(delay);
			alertAttack.gameObject.SetActive(true);
			GameObject bulletClone = PoolingManager.GetObject(BulletID.ENEMY1_BULLET, transform.position, Quaternion.identity);
			bulletClone.SetActive(true);
		}
	}
}
