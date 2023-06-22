using Assets.Scripts.Enum;
using System.Collections;
using UnityEngine;

public class Ship1_Attack : MonoBehaviour
{
	[SerializeField] private int bulletId; // Have 2 bullet type (Normal and Special)

	[SerializeField] protected float spawnDelay;
	[SerializeField] protected int numberBarrel;
	[SerializeField] protected int numberBarrelPower;

	[SerializeField] protected float betweenDegree;

	private float timer = 0;
	private readonly float delay = 5f;
	private ShipAttackBase attackBase;
	private Coroutine coroutine;
	private int bulletSpecialId;

	IEnumerator Attack()
	{
		while (true)
		{
			yield return new WaitForSeconds(spawnDelay);
			for (int i = 0; i < transform.childCount; i++)
			{
				GameObject bulletClone = attackBase.IsPowerUp ?
					PoolingManager.GetObject(bulletSpecialId, transform.position, transform.GetChild(i).rotation) :
					PoolingManager.GetObject(bulletId, transform.position, transform.GetChild(i).rotation);
				bulletClone.SetActive(true);
			}
		}
	}

	private void Start()
	{
		attackBase = GetComponentInParent<ShipAttackBase>();
		bulletId = BulletID.SHIP1_BULLET;
		bulletSpecialId = BulletID.SHIP1_BULLET_SPECIAL;
		CreateBarrel(attackBase.BulletLevel);
		coroutine = StartCoroutine(Attack());
	}

	private void Update()
	{
		if (attackBase.IsUpgrade)
		{
			CreateBarrel(attackBase.BulletLevel);
			attackBase.IsUpgrade = false;
		}

		if (attackBase.IsPowerUp)
		{
			if (attackBase.IsCreated)
			{
				CreateBarrel(attackBase.MaxBulletLevel);
				attackBase.IsCreated = false;
			}

			timer += Time.deltaTime;
			if (timer < delay) return;
			timer = 0;

			attackBase.IsPowerUp = false;
			CreateBarrel(attackBase.BulletLevel);
		}
	}

	private void OnDisable()
	{
		if (coroutine == null) return;
		coroutine = null;
	}

	private void OnEnable()
	{
		attackBase = GetComponentInParent<ShipAttackBase>();
		bulletId = BulletID.SHIP1_BULLET;
		bulletSpecialId = BulletID.SHIP1_BULLET_SPECIAL;
		CreateBarrel(attackBase.BulletLevel);
		coroutine = StartCoroutine(Attack());
	}

	public void CreateBarrel(int _bulletLevel)
	{
		ResetBarrel();

		numberBarrel = _bulletLevel + 1;
		for (int i = 0; i < numberBarrel; i++)
		{
			GameObject barrel = new($"Barrel_{i + 1}");
			barrel.transform.SetLocalPositionAndRotation
				(transform.position, Quaternion.Euler(0f, 0f, i * betweenDegree));
			barrel.transform.SetParent(transform, false);
		}

		// Rotation parent
		float offsetAngle = (numberBarrel - 1) * betweenDegree / 2;
		transform.localRotation = Quaternion.Euler(0f, 0f, -offsetAngle);
	}

	public void ResetBarrel()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}

		transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
	}
}
