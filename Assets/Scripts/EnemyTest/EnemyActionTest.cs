using System.Collections;
using UnityEngine;

public class EnemyActionTest : MonoBehaviour
{
	[SerializeField] private float radius;
	[SerializeField] private float timeCheck;
	[SerializeField] private ParticleSystem ps;
	[SerializeField] private float hpHeal;

	private void Start()
	{
		StartCoroutine(CheckEnemyInRadius());
	}

	IEnumerator CheckEnemyInRadius()
	{
		while (true)
		{
			yield return new WaitForSeconds(timeCheck);
			ps.Play();
			var collision = Physics2D.OverlapCircleAll(transform.position, radius);

			foreach (var item in collision)
			{
				var eDR = item.GetComponent<EnemyDamageReceiverTest>();
				if (eDR != null && eDR.CurrentHp < eDR.TotalHp)
				{
					eDR.CurrentHp += hpHeal;

					if (eDR.CurrentHp <= eDR.TotalHp) continue;
					eDR.CurrentHp = eDR.TotalHp;
				}
			}

			yield return new WaitForSeconds(timeCheck);
			ps.Stop();
		}
	}
}
