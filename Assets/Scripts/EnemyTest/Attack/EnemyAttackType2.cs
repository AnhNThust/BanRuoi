using Assets.Scripts.Enum;
using System.Collections;
using UnityEngine;

public class EnemyAttackType2 : MonoBehaviour
{
	[SerializeField] private int bulletId;
	[SerializeField] private GameObject barrelAttack;
	[SerializeField] private EnemyControllerTest controller;
	[SerializeField] private float coolDown;

	public EnemyControllerTest Controller { get => controller; set => controller = value; }

	private IEnumerator Spawn()
	{
		yield return new WaitUntil(() => controller.IsReady);

		while (true)
		{
			for (int i = 0; i < barrelAttack.transform.childCount; i++)
			{
				GameObject bullet = PoolingManager.GetObject(bulletId,
					barrelAttack.transform.GetChild(i).position,
					barrelAttack.transform.GetChild(i).rotation);
				bullet.SetActive(true);
				yield return new WaitForSeconds(coolDown);
			}
		}
	}

	private void Start()
	{
		controller = GetComponent<EnemyControllerTest>();

		StartCoroutine(Spawn());
	}

	private void OnDisable()
	{
		Destroy(GetComponent<EnemyAttackType2>());
	}

	public void SetInfo(EnemyBulletType pBulletType, int pNumberBarrel, float pCoolDown)
	{
		bulletId = (int)pBulletType;
		coolDown = pCoolDown;

		barrelAttack = new("BarrelAttack");
		barrelAttack.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, -35));
		barrelAttack.transform.SetParent(transform, false);
		for (int i = 0; i < pNumberBarrel; i++)
		{
			GameObject barrel = new($"Barrel_{i + 1}");
			barrel.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, 180));
			barrel.transform.SetParent(barrelAttack.transform, false);
		}

		RotateConeShape rot = barrelAttack.AddComponent(typeof(RotateConeShape)) as RotateConeShape;
		rot.SetInfo(35, 1);
	}
}
