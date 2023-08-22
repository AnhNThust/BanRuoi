using Assets.Scripts.Enum;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnerNormal : MonoBehaviour
{
	[Header("Common")]
	[SerializeField] private DOTweenPath pathStart;
	[SerializeField] private float timeMoveStart;
	[SerializeField] private Transform holder;
	[SerializeField] private EnemyType enemyId;
	[SerializeField] private int numberSpawn;
	[SerializeField] private float timeBeforeSpawn;
	[SerializeField] private float timeSpawn;

	[Header("Enemy Attack")]
	[SerializeField] private bool canAttack;
	[SerializeField] private EnemyAttackType attackType;
	[SerializeField] private EnemyBulletType bulletType;
	[SerializeField] private Transform groupShootPoint;

	[Header("Change path")]
	[SerializeField] private bool isChangePath;
	[SerializeField] private DOTweenPath path;
	[SerializeField] private float timeMove;
	[SerializeField] private bool isRotate;

	[Header("Loop")]
	[SerializeField] private bool isLoop;

	[Header("Enemy Group Move Attack")]
	[SerializeField] private bool isRandomAttack;
	[SerializeField] private Transform player;

	[Header("Move to group")]
	[SerializeField] private bool isMoveToGroup;
	[SerializeField] private Transform group;
	[SerializeField] private List<Transform> slots = new();

	[Header("Only Start")]
	[SerializeField] private bool isOnlyStart;
	[SerializeField] private bool isASpecialEnemy;

	[Header("Item")]
	[SerializeField] private Transform[] listItem;
	[SerializeField] private int numberItemSpawn;
	[SerializeField] private int[] listIndexHaveItem;

	private int counter = 0;
	private GameObject target;
	private CheckHolder checkHolder;

	[ContextMenu("Reload")]
	private void Reload()
	{
		for (var i = 0; i < group.childCount; i++)
		{
			slots.Add(group.GetChild(i));
		}
	}

	private void Start()
	{
		var points = pathStart.wps;
		target = new();
		target.transform.position = points[^1];

		checkHolder = GetComponentInParent<CheckHolder>();
		CreateListIndexHasItem();
		StartCoroutine(Spawn());
	}

	IEnumerator Spawn()
	{
		yield return new WaitForSeconds(timeBeforeSpawn);

		while (counter < numberSpawn)
		{
			yield return new WaitForSeconds(timeSpawn);

			GameObject enemy = PoolingManager.GetObject((int)enemyId, transform.position, Quaternion.identity);

			if (listIndexHaveItem.Contains(counter))
			{
				var itemClone = GetRandomItem();
				enemy.GetComponent<EnemyDamageReceiverTest>().itemPrefab = itemClone;
			}

			if (counter == 1) checkHolder.enabled = true;

			if (isRandomAttack && counter == numberSpawn - 1)
			{
				var eAC = holder.gameObject.AddComponent(typeof(EnemyRandomAttackController))
					as EnemyRandomAttackController;
				bool isMoving = isChangePath || isOnlyStart || isLoop;
				eAC.SetInfo(player, isMoving, isMoveToGroup);
			}

			if (canAttack)
			{
				//EnemyAttackType1 ea = enemy.AddComponent(typeof(EnemyAttackType1)) as EnemyAttackType1;
				//GameObject barrel = new("Barrel");
				//barrel.transform.up = Vector3.down;
				//barrel.transform.SetParent(enemy.transform, false);
				//ea.SetInfo(BulletID.ENEMY3_BULLET, 3, 3f, barrel.transform);

				SetAttackType(enemy, attackType, bulletType);
			}

			if (isChangePath)
			{
				EnemyMoveChangePath eMCP = enemy.AddComponent(typeof(EnemyMoveChangePath)) as EnemyMoveChangePath;
				eMCP.SetInfo(pathStart, timeMoveStart, path, timeMove, isRotate);
			}
			else if (isLoop)
			{
				EnemyMoveLoop eML = enemy.AddComponent(typeof(EnemyMoveLoop)) as EnemyMoveLoop;
				eML.SetInfo(pathStart, timeMoveStart);
			}
			else if (isMoveToGroup)
			{
				EnemyMoveToGroup eMTG = enemy.AddComponent(typeof(EnemyMoveToGroup)) as EnemyMoveToGroup;
				int index = Random.Range(0, slots.Count);
				eMTG.SetInfo(pathStart, slots[index], timeMoveStart);
				slots.RemoveAt(index);
			}
			else if (isOnlyStart)
			{
				if (isASpecialEnemy)
				{
					var eAT1 = enemy.AddComponent(typeof(Enemy9AAttack)) as Enemy9AAttack;
					eAT1.SetInfo(2, 0.3f);
				}

				EnemyMoveNormal eMN = enemy.AddComponent(typeof(EnemyMoveNormal)) as EnemyMoveNormal;
				eMN.SetInfo(pathStart, timeMoveStart);
			}

			enemy.transform.SetParent(holder, false);
			enemy.SetActive(true);

			counter++;
		}
	}

	private Transform GetRandomItem()
	{
		var randIndex = Random.Range(0, 5);
		randIndex = randIndex switch
		{
			1 or 2 or 3 or 4 => 1,
			_ => 0,
		};
		return listItem[randIndex];
	}

	private int[] CreateListIndexHasItem()
	{
		listIndexHaveItem = new int[numberItemSpawn];

		var index = 0;
		while (index < numberItemSpawn)
		{
			var randIndex = Random.Range(0, numberSpawn);
			if (listIndexHaveItem.Contains(randIndex)) continue;
			listIndexHaveItem[index] = randIndex;
			index++;
		}

		return listIndexHaveItem;
	}

	private void SetAttackType(GameObject pObject, EnemyAttackType pAttackType, EnemyBulletType pBulletType)
	{
		switch (pAttackType)
		{
			case EnemyAttackType.AttackType1:
				EnemyAttackType1 eAT1 = pObject.AddComponent(typeof(EnemyAttackType1)) as EnemyAttackType1;
				eAT1.SetInfo(pBulletType, 3, 3f, 1);
				break;
			case EnemyAttackType.AttackType2:
				EnemyAttackType2 eAT2 = pObject.AddComponent(typeof(EnemyAttackType2)) as EnemyAttackType2;
				eAT2.SetInfo(pBulletType, 1, 0.2f);
				break;
			case EnemyAttackType.AttackType3:
				EnemyAttackType3 eAT3 = pObject.AddComponent(typeof(EnemyAttackType3)) as EnemyAttackType3;
				eAT3.SetInfo(pBulletType, 10, 5f, groupShootPoint);
				break;
			case EnemyAttackType.AttackType4:
				break;
			case EnemyAttackType.AttackType5:
				break;
			case EnemyAttackType.AttackType6:
				break;
			case EnemyAttackType.None:
			default:
				break;
		}
	}
}
