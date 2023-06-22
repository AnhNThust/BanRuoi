using Assets.Scripts.Enum;
using Spine.Unity.Modules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageReceiver : MonoBehaviour
{
	public Transform hpTransform;
	public SpriteRenderer hpRender;
	public Transform player;

	public int itemID;
	private int hitId;
	private int explosionId;

	public float currentHp;
	public float totalHp;
	public bool isReady = false;

	public Transform breakEfx;
	private int count = 0;

	private SkeletonRendererCustomMaterials refEfxEnemyHit;
	private Dictionary<Spine.Slot, Material> customSlotMaterials;
	private Spine.Slot head2;

	public Transform[] eggBreaks;

	private void Start()
	{
		hitId = EffectID.HIT_01;
		explosionId = EffectID.EXPLOSION_1;

		GetColorEfxEnemyHit();
		customSlotMaterials[head2].SetColor("_Black", Color.black);
	}

	private void Update()
	{
		if (currentHp <= 0)
		{
			ShowExplo();
			ActionAfterDestroyBoss();
			gameObject.SetActive(false);
		}

		if (count < 1 && currentHp <= (2 * totalHp / 3))
		{
			GameObject itemClone = PoolingManager.GetObject(itemID, transform.position, Quaternion.identity);
			itemClone.SetActive(true);
			count++;
		}

		ShowEggBreak();
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
			StartCoroutine(ShowHitEfx());
			StartCoroutine(ShowBreakEfx());
		}
	}

	private void OnDisable()
	{
		customSlotMaterials[head2].SetColor("_Black", Color.black);
	}

	public void TakeDamage(float damage)
	{
		currentHp -= damage;
		float offset = currentHp / totalHp;
		hpTransform.gameObject.SetActive(true);
		hpRender.material.SetFloat("_Progress", offset);
	}

	public void ActionAfterDestroyBoss()
	{
		player.GetComponent<ShipAttackBase>().StopAttack();
		player.GetComponent<CircleCollider2D>().isTrigger = false;
		player.GetComponent<ShipEnd>().enabled = true;
	}

	public void ShowExplo()
	{
		GameObject explo = PoolingManager.GetObject(explosionId, transform.position, Quaternion.identity);
		explo.SetActive(true);
	}

	public void ShowHit(Collider2D collision)
	{
		Vector3 pos = collision.ClosestPoint(collision.transform.position);
		GameObject hitClone = PoolingManager.GetObject(hitId, pos, Quaternion.identity);
		hitClone.SetActive(true);
	}

	IEnumerator ShowBreakEfx()
	{
		breakEfx.GetComponent<ParticleSystem>().Play();
		yield return new WaitForSeconds(1.4f);
	}

	public void GetColorEfxEnemyHit()
	{
		refEfxEnemyHit = GetComponent<SkeletonRendererCustomMaterials>();
		customSlotMaterials = refEfxEnemyHit.skeletonRenderer.CustomSlotMaterials;
		foreach (var c in customSlotMaterials)
		{
			head2 = c.Key;
		}
	}

	IEnumerator ShowHitEfx()
	{
		customSlotMaterials[head2].SetColor("_Black", Color.red);
		yield return new WaitForSeconds(0.3f);
		customSlotMaterials[head2].SetColor("_Black", Color.black);
	}

	public void ShowEggBreak()
	{
		if (currentHp <= 0.75 * totalHp && !eggBreaks[0].gameObject.activeSelf)
		{
			eggBreaks[0].gameObject.SetActive(true);
		}

		if (currentHp <= 0.5 * totalHp && !eggBreaks[1].gameObject.activeSelf)
		{
			eggBreaks[1].gameObject.SetActive(true);
		}

		if (currentHp <= 0.25 * totalHp && !eggBreaks[2].gameObject.activeSelf)
		{
			eggBreaks[2].gameObject.SetActive(true);
		}
	}
}
