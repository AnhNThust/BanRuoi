using Assets.Scripts.Enum;
using System.Collections;
using UnityEngine;

public class Ship3_Attack : MonoBehaviour
{
	[SerializeField] private int bulletId; // Have 2 bullet type (Normal and Special)

	[SerializeField] protected float spawnDelay;
	[SerializeField] protected int numberBarrel;
	[SerializeField] protected int numberBarrelPower;

	[SerializeField] protected float betweenDistance;

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
				if (i == 1 &&
					(attackBase.BulletLevel == 5 || attackBase.BulletLevel == 7))
				{
					GameObject bClone = PoolingManager.GetObject(bulletSpecialId,
						transform.GetChild(i).position, transform.GetChild(i).rotation);
					bClone.SetActive(true);
					continue;
				}
				else if (attackBase.BulletLevel == 6 || attackBase.BulletLevel == 9 ||
					attackBase.IsPowerUp)
				{
					GameObject bClone = PoolingManager.GetObject(bulletSpecialId,
						transform.GetChild(i).position, transform.GetChild(i).rotation);
					bClone.SetActive(true);
					continue;
				}

				GameObject bulletClone = PoolingManager.GetObject(bulletId,
						transform.GetChild(i).position, transform.GetChild(i).rotation);
				bulletClone.SetActive(true);
			}
		}
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
				CreateBarrelPower();
				attackBase.IsCreated = false;
			}

			timer += Time.deltaTime;
			if (timer < delay) return;
			timer = 0;

			attackBase.IsPowerUp = false;
			CreateBarrel(attackBase.BulletLevel);
		}
	}

	private void OnEnable()
	{
		attackBase = GetComponentInParent<ShipAttackBase>();
		bulletId = BulletID.SHIP3_BULLET;
		bulletSpecialId = BulletID.SHIP3_BULLET_SPECIAL;
		CreateBarrel(attackBase.BulletLevel);

		if (coroutine != null) return;
		coroutine = StartCoroutine(Attack());
	}

	private void OnDisable()
	{
		if (coroutine == null) return;
		coroutine = null;
	}

	public void CreateBarrel(int _bulletLevel)
	{
		ResetBarrel();

		switch (_bulletLevel)
		{
			case 1:
			default:
				GameObject barrel1 = new("Barrel_1");
				barrel1.transform.localPosition = Vector3.zero;
				barrel1.transform.SetParent(transform, false);
				break;
			case 2: // Level 2: 2 normal bullet
			case 3: // Level 3: Spawn Delay decrease	
				numberBarrel = 2;
				betweenDistance = 0.54f;
				//if (_bulletLevel == 3) spawnDelay -= 0.1f; // Can split an another method ?

				for (int i = 0; i < numberBarrel; i++)
				{
					GameObject barrel2 = new($"Barrel_{i + 1}");
					barrel2.transform.localPosition = new(i * betweenDistance, 0, 0);
					barrel2.transform.SetParent(transform, false);
				}

				// Set position parent
				float offset = (numberBarrel - 1) * betweenDistance / 2;
				transform.localPosition = new(-offset, 0f, 0f);
				break;
			case 4: // Level 4: 3 Normal bullet
			case 5: // Level 5: 2 Normal bullet + 1 Special Bullet (faster)
			case 6: // Level 6: 3 Special bullet
			case 7: // Level 7: 2 Normal bullet + 1 Special Bullet (faster)
				numberBarrel = 3;
				betweenDistance = 0.27f;
				for (int i = 0; i < numberBarrel; i++)
				{
					GameObject barrel2 = new($"Barrel_{i + 1}");
					barrel2.transform.localPosition = (i == 1) ?
						new(i * betweenDistance, 0.2f, 0) :
						new(i * betweenDistance, 0, 0);
					barrel2.transform.SetParent(transform, false);
				}

				// Set Position parent
				float offset1 = (numberBarrel - 1) * betweenDistance / 2;
				transform.localPosition = new(-offset1, 0, 0);
				break;
			case 8: // Level 8: 4 Normal bullet
			case 9: // Level 9: 4 Special bullet
				numberBarrel = 4;
				betweenDistance = 0.27f;
				for (int i = 0; i < numberBarrel; i++)
				{
					GameObject barrel2 = new($"Barrel_{i + 1}");
					barrel2.transform.localPosition = (i == 1 || i == 2) ?
						new(i * betweenDistance, 0.2f, 0) :
						new(i * betweenDistance, 0, 0);
					barrel2.transform.SetParent(transform, false);
				}

				// Set Position parent
				float offset2 = (numberBarrel - 1) * betweenDistance / 2;
				transform.localPosition = new(-offset2, 0, 0);
				break;
		}
	}

	public void CreateBarrelPower()
	{
		ResetBarrel();

		betweenDistance = 0.27f;
		for (int i = 0; i < numberBarrelPower; i++)
		{
			GameObject barrel = new($"Barrel_{i + 1}");
			barrel.transform.localPosition = (i == 1 || i == 4) ?
				new(i * betweenDistance, 0.2f, 0) : (i == 2 || i == 3) ?
				new(i * betweenDistance, 0.4f, 0) :
				new(i * betweenDistance, 0, 0);
			barrel.transform.SetParent(transform, false);
		}

		// Set Position parent
		float offset = (numberBarrelPower - 1) * betweenDistance / 2;
		transform.localPosition = new(-offset, 0, 0);
	}

	public void ResetBarrel()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}

		transform.localPosition = Vector3.zero;
	}
}
