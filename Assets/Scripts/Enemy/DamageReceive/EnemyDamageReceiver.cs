using Assets.Scripts.Enum;
using UnityEngine;

public class EnemyDamageReceiver : MonoBehaviour
{
	public Transform hpTransform;
	public SpriteRenderer hpRender;
	public Transform itemPrefab;

	public int itemID;
	public int explosionId;
	public int hitId;

	public float currentHp;
	public float totalHp;
	public bool isReady = false;

	private Transform _target;

	private void Start()
	{
		hitId = EffectID.HIT_01;
	}

	private void OnEnable()
	{
		currentHp = totalHp;
		hpRender.material.SetFloat("_Progress", 1f);
		hpTransform.gameObject.SetActive(false);
		explosionId = EffectID.EXPLOSION_3_MINI;
	}

	private void Update()
	{
		_target = GetComponent<EnemyMovement>().target;

		if (_target != null && transform.position == _target.position)
		{
			isReady = true;
		}

		if (currentHp <= 0)
		{
			if (itemPrefab != null)
			{
				itemID = itemPrefab.GetComponent<ObjectPool>().GetID();
				GameObject itemClone = PoolingManager.GetObject(itemID, transform.position, Quaternion.identity);
				itemClone.SetActive(true);
			}

			ShowExplode();
			UIManager.AddScore(500);
			PoolingManager.PoolObject(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PlayerBullet"))
		{
			float damage = collision.GetComponent<BulletDamageSender>().damage;

			if (isReady)
			{
				TakeDamage(damage);
			}

			ShowHit(collision);
			PoolingManager.PoolObject(collision.gameObject);
		}
	}

	public void TakeDamage(float damage)
	{
		currentHp -= damage;
		float offset = currentHp / totalHp;
		hpTransform.gameObject.SetActive(true);
		hpRender.material.SetFloat("_Progress", offset);
	}

	public void ShowExplode()
	{
		GameObject explo = PoolingManager.GetObject(explosionId,
				transform.position, Quaternion.identity);
		explo.SetActive(true);
	}

	public void ShowHit(Collider2D collision)
	{
		Vector3 pos = collision.ClosestPoint(collision.transform.position);
		GameObject hitClone = PoolingManager.GetObject(hitId, pos,
			Quaternion.identity);
		hitClone.SetActive(true);
	}
}
