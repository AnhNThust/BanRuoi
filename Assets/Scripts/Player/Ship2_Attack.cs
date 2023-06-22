using Assets.Scripts.Enum;
using System.Collections;
using UnityEngine;

public class Ship2_Attack : MonoBehaviour
{
	[SerializeField] protected float spawnDelay;
	[SerializeField] protected int numberBarrel;
	[SerializeField] protected int numberBarrelPower;

	[SerializeField] protected float betweenDistance;
	[SerializeField] protected float betweenAngle;

	private float timer = 0;
	private readonly float delay = 5f;
	private ShipAttackBase attackBase;
	private Coroutine coroutine;
	private Coroutine coroutine1; // For Power Up state
	private Coroutine coroutine2; // For Power Up state

	private int bulletId;
	private int bulletSpecialId;
	private int bulletSuperId;

	IEnumerator Attack()
	{
		while (true)
		{
			yield return new WaitForSeconds(spawnDelay);
			for (int i = 0; i < transform.childCount; i++)
			{
				Vector3 pos = transform.GetChild(i).position;
				Quaternion rot = transform.GetChild(i).rotation;
				GameObject bulletClone = attackBase.BulletLevel switch
				{
					3 or 4 or 5 or 6 => (attackBase.BulletLevel == 5 && i == 1) ?
						PoolingManager.GetObject(bulletId, pos, rot) :
						PoolingManager.GetObject(bulletSpecialId, pos, rot),
					7 or 8 or 9 => (i == 1 && attackBase.BulletLevel == 8) ?
						PoolingManager.GetObject(bulletId, pos, rot) :
						(i == 1 && attackBase.BulletLevel == 9) ?
						PoolingManager.GetObject(bulletSpecialId, pos, rot) :
						PoolingManager.GetObject(bulletSuperId, pos, rot),
					_ => PoolingManager.GetObject(bulletId, pos, rot),
				};
				bulletClone.SetActive(true);
			}
		}
	}

	/// <summary>
	/// For Power Up state (2 super bullet outside)
	/// </summary>
	/// <returns></returns>
	IEnumerator Attack1()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.5f);
			for (int i = 0; i < 2; i++)
			{
				Vector3 pos = transform.GetChild(i * 4).position;
				Quaternion rot = transform.GetChild(i * 4).rotation;
				GameObject bulletClone = PoolingManager.GetObject(bulletSuperId, pos, rot);
				bulletClone.SetActive(true);
			}
		}
	}

	/// <summary>
	/// For Power Up state (2 super bullet inside)
	/// </summary>
	/// <returns></returns>
	IEnumerator Attack2()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.2f);
			for (int i = 1; i < transform.childCount - 1; i++)
			{
				Vector3 pos = transform.GetChild(i).position;
				Quaternion rot = transform.GetChild(i).rotation;
				GameObject bClone = PoolingManager.GetObject(bulletSuperId, pos, rot);
				bClone.SetActive(true);
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
				StopAllCoroutines();
				coroutine = null;

				CreateBarrelPower();
				attackBase.IsCreated = false;

				coroutine1 = StartCoroutine(Attack1());
				coroutine2 = StartCoroutine(Attack2());
			}

			timer += Time.deltaTime;
			if (timer < delay) return;
			timer = 0;

			attackBase.IsPowerUp = false;
			CreateBarrel(attackBase.BulletLevel);

			StopAllCoroutines();
			coroutine1 = null;
			coroutine2 = null;

			coroutine = StartCoroutine(Attack());
		}
	}

	private void OnEnable()
	{
		attackBase = GetComponentInParent<ShipAttackBase>();

		bulletId = BulletID.SHIP2_BULLET;
		bulletSpecialId = BulletID.SHIP2_BULLET_SPECIAL;
		bulletSuperId = BulletID.SHIP2_BULLET_SUPER;

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
			case 1: // Level 1: 2 Normal bullet
				numberBarrel = 2;
				betweenDistance = 0.54f;
				for (int i = 0; i < numberBarrel; i++)
				{
					GameObject barrel = new($"Barrel_{i + 1}");
					barrel.transform.localPosition = new(i * betweenDistance, 0, 0);
					barrel.transform.SetParent(transform, false);
				}

				// Set position parent
				float offset = (numberBarrel - 1) * betweenDistance / 2;
				transform.localPosition = new(-offset, 0, 0);
				break;
			case 3: // Level 3: 1 Special bullet	
			default:
				GameObject barrel1 = new("Barrel_1");
				barrel1.transform.localPosition = Vector3.zero;
				barrel1.transform.SetParent(transform, false);
				break;
			case 4: // Level 4: 2 Special bullet
			case 7: // Level 7: 2 Super bullet (fastest)
				numberBarrel = 2;
				betweenAngle = 6;
				for (int i = 0; i < numberBarrel; i++)
				{
					GameObject barrel = new($"Barrel_{i + 1}");
					Vector3 pos = new(0, -0.3f, 0);
					barrel.transform.
						SetLocalPositionAndRotation(pos,
						Quaternion.Euler(0, 0, i * betweenAngle));
					barrel.transform.SetParent(transform, false);
				}

				// Rotation parent
				float offset3 = (numberBarrel - 1) * betweenAngle / 2;
				transform.localRotation = Quaternion.Euler(0f, 0f, -offset3);
				break;
			case 2: // Level 2: 3 normal bullet
			case 5: // Level 5: 2 Special bullet (faster) + 1 Normal Bullet
			case 6: // Level 6: 3 Special bullet (faster)
			case 8: // Level 8: 2 Super bullet (fastest) + 1 Normal bullet
			case 9: // Level 9: 2 Super bullet, 1 Special bullet
				numberBarrel = 3;
				betweenAngle = 3;
				for (int i = 0; i < numberBarrel; i++)
				{
					GameObject barrel = new($"Barrel_{i + 1}");
					Vector3 pos = new(0, -0.3f, 0);
					barrel.transform.
						SetLocalPositionAndRotation(pos,
						Quaternion.Euler(0, 0, i * betweenAngle));
					barrel.transform.SetParent(transform, false);
				}

				// Rotation parent
				float offset4 = (numberBarrel - 1) * betweenAngle / 2;
				transform.localRotation = Quaternion.Euler(0f, 0f, -offset4);
				break;
		}
	}

	public void CreateBarrelPower()
	{
		ResetBarrel();

		betweenAngle = 5;
		for (int i = 0; i < numberBarrelPower; i++)
		{
			GameObject barrel;
			barrel = new($"Barrel_{i + 1}");
			Vector3 pos = new((i - 2) * 0.5f, 0, 0);
			if (i == 0)
			{
				barrel.transform.SetLocalPositionAndRotation(pos, Quaternion.Euler(0, 0, betweenAngle));
			}
			else if (i == numberBarrelPower - 1)
			{
				barrel.transform.SetLocalPositionAndRotation(pos, Quaternion.Euler(0, 0, -betweenAngle));
			}
			else
			{
				barrel.transform.localPosition = pos;
			}
			barrel.transform.SetParent(transform, false);
		}
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
