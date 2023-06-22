using Assets.Scripts.Enum;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestAttack : MonoBehaviour
{
	public float timer;
	public float delay;

	public int maxNumberAttack;
	public int numberAttack;
	public float damage;
	public float baseDamage;

	public Transform forcusPowerEfx;

	private int count;
	private List<Transform> listEnemy;
	private ShipAttackBase shipAttackBase;
	private int bulletId;
	private Coroutine targetingCrt;
	private Coroutine attackCrt;

	public IEnumerator Targeting()
	{
		count = 0;

		while (count < listEnemy.Count)
		{
			GameObject targeting = PoolingManager.GetObject(EffectID.TARGETING, listEnemy[count].position, Quaternion.identity);
			targeting.GetComponent<LaserTargeting>().Target = listEnemy[count];
			targeting.SetActive(true);

			count++;
			yield return new WaitForSeconds(0.1f);
		}
	}

	public IEnumerator Attack()
	{
		yield return new WaitForSeconds(1f);

		for (int i = 0; i < listEnemy.Count; i++)
		{
			GameObject ls = PoolingManager.GetObject(bulletId, transform.position, Quaternion.identity);
			ls.GetComponent<HideLaser>().origin = transform;
			ls.GetComponent<HideLaser>().Target = listEnemy[i];
			ls.SetActive(true);
			yield return new WaitForSeconds(0.04f);
			if (listEnemy[i].GetComponent<EnemyDamageReceiver>() != null)
				listEnemy[i].GetComponent<EnemyDamageReceiver>().TakeDamage(damage);
			else if (listEnemy[i].GetComponent<EnemyDamageReceiverTest>() != null)
				listEnemy[i].GetComponent<EnemyDamageReceiverTest>().TakeDamage(damage);
			else
				listEnemy[i].GetComponent<BossDamageReceiver>().TakeDamage(damage * 3);
		}
	}

	private void OnEnable()
	{
		bulletId = BulletID.LASER_TEST;
		shipAttackBase = GetComponentInParent<ShipAttackBase>();
		GetListEnemy();
	}

	private void OnDisable()
	{
		if (targetingCrt == null && attackCrt == null) return;
		targetingCrt = null;
		attackCrt = null;
	}

	private void Update()
	{
		if (shipAttackBase.IsUpgrade)
		{
			UpdateDamage(shipAttackBase.BulletLevel);
			shipAttackBase.IsUpgrade = false;
		}

		if (shipAttackBase.IsPowerUp)
		{
			if (shipAttackBase.IsCreated)
			{
				targetingCrt = null;
				attackCrt = null;
				timer = 0;
				attackCrt = StartCoroutine(AttackPowerUp());
				shipAttackBase.IsCreated = false;
			}

			timer += Time.deltaTime;
			if (timer < 5) return;
			timer = 0;

			StopAllCoroutines();
			shipAttackBase.IsPowerUp = false;
			attackCrt = null;
		}

		timer += Time.deltaTime;
		if (timer <= delay) return;
		timer = 0;

		GetListEnemy();

		targetingCrt = StartCoroutine(Targeting());

		attackCrt = StartCoroutine(Attack());
	}

	public void GetListEnemy()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		if (enemies.Length < maxNumberAttack)
			numberAttack = enemies.Length;
		else
			numberAttack = maxNumberAttack;

		listEnemy = new(numberAttack);
		for (int i = 0; i < numberAttack; i++)
		{
			if (enemies[i].activeSelf)
			{
				listEnemy.Add(enemies[i].transform);
			}
		}
	}

	public void UpdateDamage(int bulletLevel)
	{
		damage = baseDamage + bulletLevel;
	}

	public IEnumerator AttackPowerUp()
	{
		Transform boss = listEnemy[0];

		forcusPowerEfx.gameObject.SetActive(true);
		GameObject ls = PoolingManager.GetObject(bulletId, transform.position,
				Quaternion.identity);
		ls.GetComponent<HideLaser>().origin = transform;
		ls.GetComponent<HideLaser>().Target = boss;
		ls.GetComponent<HideLaser>().TimeHide = 5f;
		ls.SetActive(true);

		while (true)
		{
			boss.GetComponent<BossDamageReceiver>().TakeDamage(10f);
			yield return new WaitForSeconds(0.02f);
		}
	}
}
