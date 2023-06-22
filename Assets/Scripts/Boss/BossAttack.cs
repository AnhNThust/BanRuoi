using Assets.Scripts.Enum;
using DG.Tweening;
using Spine.Unity;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossAttack : MonoBehaviour
{
    public Transform[] attackTypePos;
	public Transform _target;
	public DOTweenPath pathForEnemyChild;

	public float delayAttack;

    public BossDamageReceiver damageReceiver;
	public new SkeletonAnimation animation;

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
		yield return new WaitForSeconds(1f);
		Spawn(index);
		yield return new WaitForSeconds(track.Animation.duration);
		animation.AnimationState.SetAnimation(0, "idle", true);
	}

	public void Spawn(int index)
	{
		Transform parent = attackTypePos[index - 1];

		switch (index)
		{
			case 1:
				
				for (int i = 0; i < parent.childCount; i++)
				{
					GameObject rocketMini = PoolingManager.GetObject(BulletID.ROCKET_MINI, parent.GetChild(i).position,
						parent.GetChild(i).rotation);
					rocketMini.GetComponent<HomingMissle>().target = _target;
					rocketMini.SetActive(true);
				}
				break;
			case 2:
				GameObject fireBall = PoolingManager.GetObject(BulletID.FIRE_BALL, parent.position, Quaternion.identity);
				fireBall.SetActive(true);
				break;
			case 3:
				for (int i = 0; i < pathForEnemyChild.transform.childCount; i++)
				{
					GameObject enemyClone = PoolingManager.GetObject(EnemyID.ENEMY_4, parent.GetChild(i).position,
						Quaternion.identity);
					enemyClone.SetActive(true);
					enemyClone.GetComponent<EnemyMovement>().path = pathForEnemyChild;
					enemyClone.GetComponent<EnemyMovement>().SetInfo(pathForEnemyChild.transform.GetChild(i));
				}
				break;
			case 4:
				for (int i = 0; i < parent.childCount; i++)
				{
					GameObject bullet = PoolingManager.GetObject(BulletID.ENEMY1_BULLET, parent.GetChild(i).position, parent.GetChild(i).rotation);
					bullet.SetActive(true);
				}
				break;
			case 5:
			case 6:
				GameObject rocket = PoolingManager.GetObject(BulletID.ROCKET, parent.position,
					parent.rotation);
				rocket.GetComponent<HomingMissle>().target = _target;
				rocket.SetActive(true);
				break;
			default:
				break;
		}
	}

	public int GetRandomAttackType()
	{
		return Random.Range(1, 6);
	}
}
