using Assets.Scripts.Enum;
using UnityEngine;

public class EnemyDamageReceiverTest : MonoBehaviour
{
	public Transform hpTransform;
	public SpriteRenderer hpRender;
	public Transform itemPrefab;

	public int itemID;
	public int explosionId;
	public int hitId;

	[SerializeField] private float currentHp;
	[SerializeField] private float totalHp;
	public bool isReady = false;

	private Transform _target;

	public float CurrentHp { get => currentHp; set => currentHp = value; }
	public float TotalHp { get => totalHp; set => totalHp = value; }

	private void Start()
	{
		hitId = EffectID.HIT_01;
	}

	private void OnEnable()
	{
		hpTransform = transform.GetChild(0);
		hpRender = transform.GetChild(0).GetComponent<SpriteRenderer>();

		CurrentHp = TotalHp;
		hpRender.material.SetFloat("_Progress", 1f);
		hpTransform.gameObject.SetActive(false);
		explosionId = EffectID.EXPLOSION_3_MINI;
	}

	private void Update()
	{
		_target = GetComponent<EnemyMovementTest>().Target;

		if (_target != null && transform.position == _target.position)
		{
			isReady = true;
		}

		if (CurrentHp <= 0)
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
		CurrentHp -= damage;
		float offset = CurrentHp / TotalHp;
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
