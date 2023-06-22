using UnityEngine;

public class Despawner : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerBullet") || collision.CompareTag("Item") || collision.CompareTag("EnemyBullet"))
		{
			PoolingManager.PoolObject(collision.gameObject);
		}
	}
}
