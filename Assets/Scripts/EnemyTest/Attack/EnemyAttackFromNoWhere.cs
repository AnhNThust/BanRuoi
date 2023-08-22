using Assets.Scripts.Enum;
using System.Collections;
using UnityEngine;

public class EnemyAttackFromNoWhere : MonoBehaviour
{
	[SerializeField] private EnemyBulletType bulletId;
	[SerializeField] private int attackAlertId;
	[SerializeField] private float timeBeforeAttack;
	[SerializeField] private float coolDownAlert;

	public EnemyBulletType BulletId { get => bulletId; set => bulletId = value; }
	public float TimeBeforeAttack { get => timeBeforeAttack; set => timeBeforeAttack = value; }
	public int AttackAlertId { get => attackAlertId; set => attackAlertId = value; }
	public float CoolDownAlert { get => coolDownAlert; set => coolDownAlert = value; }

	private IEnumerator Spawn()
	{
		yield return new WaitForSeconds(timeBeforeAttack);

		Vector3 posAlert = new(transform.position.x, 0, 0);
		GameObject alert = PoolingManager.GetObject(attackAlertId, posAlert, Quaternion.identity);
		HideObject ho = alert.AddComponent(typeof(HideObject)) as HideObject;
		ho.TimeHide = coolDownAlert;
		alert.SetActive(true);

		yield return new WaitForSeconds(coolDownAlert);

		GameObject bullet = PoolingManager.GetObject((int)bulletId, transform.position, transform.rotation);
		bullet.SetActive(true);

		yield return new WaitForSeconds(0.7f);
		gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		attackAlertId = EffectID.ALERT_ATTACK;

		StartCoroutine(Spawn());
	}
}
