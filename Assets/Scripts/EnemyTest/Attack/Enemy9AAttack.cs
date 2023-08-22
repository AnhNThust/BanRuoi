using Assets.Scripts.Enum;
using System.Collections;
using UnityEngine;

public class Enemy9AAttack : MonoBehaviour
{
	[SerializeField] private float coolDown;
	[SerializeField] private Transform[] barrels;
    [SerializeField] private int bulletId1;
	[SerializeField] private int bulletId2;
	[SerializeField] private EnemyControllerTest controller;

	public float CoolDown { get => coolDown; set => coolDown = value; }
	public Transform[] Barrels { get => barrels; set => barrels = value; }
	public EnemyControllerTest Controller { get => controller; set => controller = value; }

	private IEnumerator Attack1()
	{
		yield return new WaitUntil(() => Controller.IsReady);

		while (true)
		{
			var bullet = PoolingManager.GetObject(bulletId1, Barrels[0].position, Barrels[0].rotation);
			bullet.SetActive(true);
			yield return new WaitForSeconds(CoolDown);
		}
	}

	private IEnumerator Attack2()
	{
		yield return new WaitUntil(() => Controller.IsReady);

		while (true)
		{
			Barrels[1].rotation = Barrels[1].rotation == Quaternion.Euler(0, 0, 0) ?
				Quaternion.Euler(0, 0, 45f) :
				Quaternion.Euler(0, 0, 0);

			for (var i = 0; i < Barrels[1].childCount; i++)
			{
				var bullet = PoolingManager.GetObject(bulletId2, Barrels[1].GetChild(i).position,
					Barrels[1].GetChild(i).rotation);
				bullet.SetActive(true);
			}

			yield return new WaitForSeconds(CoolDown * 2);
		}
	}

	public void SetInfo(int pNumberBarrel, float pCoolDown)
	{
		Barrels = new Transform[pNumberBarrel];
		for (int i = 0; i < pNumberBarrel; i++)
		{
			Barrels[i] = transform.Find($"BarrelAttack_{i + 1}");
		}

		coolDown = pCoolDown;
	}

	private void Start()
	{
		Controller = GetComponent<EnemyControllerTest>();
		bulletId1 = BulletID.ENEMY2_BULLET;
		bulletId2 = BulletID.ENEMY_WAVE_BULLET;

		StartCoroutine(Attack1());
		StartCoroutine(Attack2());
	}
}
