using Assets.Scripts.Enum;
using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Threading;
using UnityEngine;

public class Boss_1_Attack : MonoBehaviour
{
	public Transform[] attackTypePos;
	public float delayAttack;

	public BossDamageReceiver damageReceiver;
	public new SkeletonAnimation animation;

	private LaserBeam laserBeam;
	public Material material;

	private void Start()
	{
		damageReceiver = GetComponent<BossDamageReceiver>();
		animation = GetComponent<SkeletonAnimation>();
		StartCoroutine(Attack());
	}

	IEnumerator Attack()
	{
		while (true)
		{
			yield return new WaitForSeconds(delayAttack);
			int attackType = GetRandomAttackType();
			StartCoroutine(AttackRountine(attackType));
		}
	}

	IEnumerator AttackRountine(int index)
	{
		var track = animation.AnimationState.SetAnimation(0, $"attack{index}", false);
		yield return new WaitForSeconds(0.8f);
		Spawn(index);
		yield return new WaitForSeconds(track.Animation.duration - 0.8f);
		animation.AnimationState.SetAnimation(0, "idle", true);
	}

	public void Spawn(int index)
	{
		Transform parent = attackTypePos[index - 1];

		switch (index)
		{
			case 1: // Laser Reflect attack
				StartCoroutine(Attack1(parent));
				break;
			case 2: // EnemyBullet_1, Count shoot: 5, delay: 0.2f
				StartCoroutine(AttackInfo(parent, BulletID.ENEMY1_BULLET, 5, 0.2f));
				break;
			case 3: // ShortLaser, Count shoot: 2, delay: 0.2f
				StartCoroutine(AttackInfo(parent, BulletID.SHORT_LASER, 2, 0.2f));
				break;
			case 4: // Laser verticle attack
				StartCoroutine(Attack4(parent));
				break;
			case 5:
				StartCoroutine(Attack5(parent));
				break;
			default:
				break;
		}
	}

	private Quaternion GenerateRandomAngle()
	{
		int rand = Random.Range(125, 130);
		return Quaternion.Euler(0, 0, rand);
	}

	/// <summary>
	/// ???
	/// </summary>
	/// <param name="pParent"></param>
	/// <param name="countShoot"></param>
	/// <param name="delay"></param>
	/// <returns></returns>
	IEnumerator AttackInfo(Transform pParent, int pBulletId, int countShoot, float delay)
	{
		int counter = 0;

		while (counter < countShoot)
		{
			for (int j = 0; j < pParent.childCount; j++)
			{
				GameObject bClone = PoolingManager.GetObject(pBulletId, pParent.GetChild(j).position,
					pParent.GetChild(j).rotation);
				bClone.SetActive(true);
			}
			counter++;
			yield return new WaitForSeconds(delay);
		}
	}

	IEnumerator Attack1(Transform pParent)
	{
		// Boss stop movement
		GetComponent<BossMovement>().tweenerCore.Pause();

		for (int i = 0; i < pParent.childCount; i++)
		{
			Quaternion rot = GenerateRandomAngle();
			pParent.GetChild(i).rotation = rot;

			GameObject laser = PoolingManager.GetObject(BulletID.LIGHTNING_1, pParent.GetChild(i).position);
			laser.GetComponent<LaserBeamTest>().SetInfo(pParent.GetChild(i).position, pParent.GetChild(i).up, 2.8f);
			laser.SetActive(true);
		}

		yield return new WaitForSeconds(2.8f);
		GetComponent<BossMovement>().tweenerCore.Play();
	}

	IEnumerator Attack4(Transform pParent)
	{
		GetComponent<BossMovement>().tweenerCore.Pause();

		for (int i = 0; i < pParent.childCount; i++)
		{
			Vector3 pos = pParent.GetChild(i).position;
			GameObject laser = PoolingManager.GetObject(BulletID.FIRE_RAY, pos, Quaternion.identity);
			laser.GetComponent<LaserBeamTest>().SetInfo(pos, -pParent.GetChild(i).up, 1.3f);
			laser.SetActive(true);
		}

		yield return new WaitForSeconds(1.2f);
		GetComponent<BossMovement>().tweenerCore.Play();
	}

	IEnumerator Attack5(Transform pParent)
	{
		int counter = 0;
		yield return new WaitForSeconds(1.5f);
		while (counter < 2)
		{
			for (int j = 0; j < 6; j++)
			{
				GameObject bClone = PoolingManager.GetObject(BulletID.ENEMY1_BULLET, pParent.GetChild(j).position,
					pParent.GetChild(j).rotation);
				bClone.SetActive(true);
			}
			counter++;
			yield return new WaitForSeconds(0.2f);
		}
	}

	public int GetRandomAttackType()
	{
		return Random.Range(1, 6);
	}
}
