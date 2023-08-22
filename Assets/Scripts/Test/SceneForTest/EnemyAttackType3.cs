using Assets.Scripts.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackType3 : MonoBehaviour
{
	[SerializeField] private EnemyControllerTest controller;
	[SerializeField] private List<Transform> firePoints;
	[SerializeField] private Transform groupShootPoint;
	[SerializeField] private int numberBullet;
	[SerializeField] private float coolDown;
	[SerializeField] private int bulletId;

	private IEnumerator Attack()
	{
		yield return new WaitUntil(() => controller.IsReady);

		RotateChildrenToParent();

		while (true)
		{
			GetRandomFirePoint();

			for (int i = 0; i < firePoints.Count; i++)
			{
				GameObject alert = PoolingManager.GetObject(EffectID.ALERT_ATTACK_MINI, transform.position,
					firePoints[i].rotation);
				alert.SetActive(true);
			}

			yield return new WaitForSeconds(0.5f);

			for (int i = 0; i < firePoints.Count; i++)
			{
				GameObject bullet = PoolingManager.GetObject(bulletId, firePoints[i].position, 
					firePoints[i].rotation);
				bullet.SetActive(true);
			}

			yield return new WaitForSeconds(coolDown);
		}
	}

	private void OnDisable()
	{
		Destroy(GetComponent<EnemyAttackType3>());
	}

	private void Start()
	{
		controller = GetComponent<EnemyControllerTest>();
		StartCoroutine(Attack());
	}

	public void SetInfo(EnemyBulletType pBulletType, int pNumberBullet, float pCoolDown, Transform pGroupShootPoint)
	{
		bulletId = (int)pBulletType;
		numberBullet = pNumberBullet;
		coolDown = pCoolDown;
		groupShootPoint = pGroupShootPoint;
	}

	private void GetRandomFirePoint()
	{
		int counter = 0;
		firePoints = new List<Transform>();
		while (true)
		{
			int randIndex = Random.Range(0, groupShootPoint.childCount);
			if (firePoints.Contains(groupShootPoint.GetChild(randIndex))) continue;
			firePoints.Add(groupShootPoint.GetChild(randIndex));

			counter++;
			if (counter >= numberBullet) break;
		}
	}

	private void RotateChildrenToParent()
	{
		for (int i = 0; i < groupShootPoint.childCount; i++)
		{
			Vector3 dir = transform.position - groupShootPoint.GetChild(i).position;
			dir.Normalize();
			groupShootPoint.GetChild(i).up = dir;
		}
	}
}
