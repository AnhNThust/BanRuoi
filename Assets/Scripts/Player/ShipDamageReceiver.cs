using Assets.Scripts.Enum;
using Lean.Touch;
using UnityEngine;

public class ShipDamageReceiver : MonoBehaviour
{
	private LeanDragTranslate leanDrag;
	private LeanSelectByFinger leanSelect;
	private ShipAttackBase attack;
	private Vector3 returnPosition;

	private int explosionId;
	private int hitId;

	private float timer = 0;
	private readonly float delay = 4;
	private int count = 1;

	private void Start()
	{
		leanDrag = GetComponent<LeanDragTranslate>();
		leanSelect = GetComponent<LeanSelectByFinger>();
		attack = GetComponent<ShipAttackBase>();
		returnPosition = new Vector3(0, -6.8f, 0);

		explosionId = EffectID.EXPLOSION_2;
		hitId = EffectID.HIT_01;
	}

	private void FixedUpdate()
	{
		if (UIManager.GetLife() <= 0)
		{
			if (count > 0)
			{
				count--;
				ShowExplo();
				// Return Ship to begin position
				ShipStop();
			}

			timer += Time.fixedDeltaTime;
			if (timer < delay) return;
			timer = 0;

			UIManager.ShowDefPanel();
			Time.timeScale = 0;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("EnemyBullet") || collision.CompareTag("Enemy"))
		{
			if (UIManager.GetLife() > 0)
			{
				ShowHit(collision);
				PoolingManager.PoolObject(collision.gameObject);
				UIManager.UpdateLife(-1);
			}
		}
	}

	public void ShowExplo()
	{
		GameObject explo = PoolingManager.
			GetObject(explosionId, transform.position, Quaternion.identity);
		explo.SetActive(true);
	}

	public void ShowHit(Collider2D collision)
	{
		Vector3 pos = collision.ClosestPoint(collision.transform.position);
		GameObject hitClone = PoolingManager.GetObject(hitId, pos, 
			Quaternion.identity);
		hitClone.SetActive(true);
	}

	public void ShipStop()
	{
		transform.position = returnPosition;
		leanDrag.enabled = false;
		leanSelect.enabled = false;
		for (int i = 0; i < attack.BarrelParents.Length; i++)
		{
			attack.BarrelParents[i].gameObject.SetActive(false);
		}
	}
}
