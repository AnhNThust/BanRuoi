using DG.Tweening;
using UnityEngine;

public class EnemyRandomAttack : MonoBehaviour
{
	[SerializeField] private Transform player;

	private EnemyMoveBase enemyMovement;

	public Transform Player { get => player; set => player = value; }

	private void OnEnable()
	{
		enemyMovement = GetComponent<EnemyMoveBase>();
	}

	public void SetInfo(Transform pPlayer)
	{
		Player = pPlayer;
	}

	public void AttackWhenMoving()
	{
		enemyMovement.TweenerCore.Pause();
		transform.up = (Player.position - transform.position).normalized;

		Vector3[] points = new Vector3[]
		{
			transform.position,
			Player.position,
			transform.position
		};

		transform.DOPath(points, 7f, PathType.Linear, PathMode.TopDown2D)
			.SetEase(Ease.InOutBack)
			.SetLookAt(0.01f, transform.forward, Vector3.left)
			.OnComplete(() =>
			{
				enemyMovement.TweenerCore.Play();
			});
	}

	public void AttackWhenInGroup()
	{
		transform.up = (Player.position - transform.position).normalized;

		Vector3[] points = new Vector3[]
		{
			transform.position,
			Player.position,
			transform.position
		};

		transform.DOPath(points, 5f, PathType.Linear, PathMode.TopDown2D)
			.SetSpeedBased()
			.SetEase(Ease.Linear)
			.SetLookAt(0.01f, transform.forward, Vector3.left)
			.OnComplete(() =>
			{
				transform.up = Vector3.up;
			});
	}
}
