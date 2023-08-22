using DG.Tweening;
using UnityEngine;

public class Enemy9AMovement : EnemyMoveBase
{
	[Header("In Runtime")]
	[SerializeField] private DOTweenPath path;
	[SerializeField] private float timeMove;

	[Header("In Editor")]
	[SerializeField] private Transform ringEfx;
	[SerializeField] private Transform healEfx;
	[SerializeField] private Enemy9AAttack attack;

	public DOTweenPath Path { get => path; set => path = value; }
	public float TimeMove { get => timeMove; set => timeMove = value; }

	public void SetInfo(DOTweenPath pPath)
	{
		Path = pPath;
		Move();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		attack = GetComponent<Enemy9AAttack>();
	}

	protected override void Update()
	{
		base.Update();
	}

	private void Move()
	{
		var points = Path.wps;
		TweenerCore = transform.DOPath(points.ToArray(), TimeMove, PathType.CatmullRom, PathMode.TopDown2D)
			.SetEase(Ease.Linear)
			.SetLookAt(0.01f, transform.forward, Vector3.left)
			.OnComplete(() =>
			{
				transform.up = Vector3.up;

				ringEfx.gameObject.SetActive(true);
				healEfx.gameObject.SetActive(true);
				attack.enabled = true;
			});
	}
}
