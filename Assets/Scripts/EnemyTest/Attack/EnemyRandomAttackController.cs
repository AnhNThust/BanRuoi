using System.Collections;
using UnityEngine;

public class EnemyRandomAttackController : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private bool isMoving;
	[SerializeField] private bool isInGroup;

	public Transform Player { get => player; set => player = value; }
	public bool IsMoving { get => isMoving; set => isMoving = value; }
	public bool IsInGroup { get => isInGroup; set => isInGroup = value; }

	public IEnumerator EnableRandomAttack()
	{
		while (true)
		{
			yield return new WaitForSeconds(4f);
			if (transform.childCount <= 0) break;

			int index = Random.Range(0, transform.childCount);
			EnemyActiveAttack(transform.GetChild(index));
		}
	}

	private void Start()
	{
		StartCoroutine(EnableRandomAttack());
	}

	public void SetInfo(Transform pPlayer, bool pIsMoving, bool pIsInGroup)
	{
		player = pPlayer;
		isMoving = pIsMoving;
		isInGroup = pIsInGroup;
	}

	private void EnemyActiveAttack(Transform pObject)
	{
		EnemyRandomAttack ert = pObject.gameObject.AddComponent(typeof(EnemyRandomAttack)) as EnemyRandomAttack;
		ert.SetInfo(player);

		if (isMoving)
		{
			ert.AttackWhenMoving();
		}

		if (isInGroup)
		{
			ert.AttackWhenInGroup();
		}
	}
}
