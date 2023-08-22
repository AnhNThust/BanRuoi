using Assets.Scripts.Enum;
using System.Collections;
using UnityEngine;

public class EnemyCallGate : MonoBehaviour
{
	[SerializeField] private float timeCloseGate;

	IEnumerator OpenGate()
	{
		yield return new WaitForSeconds(1.5f);

		GameObject gate = PoolingManager.GetObject(EffectID.GREEN_HOLE, transform.position, Quaternion.identity);
		gate.SetActive(true);

		yield return new WaitForSeconds(timeCloseGate);
		PoolingManager.PoolObject(gate);
	}

	private void Start()
	{
		StartCoroutine(OpenGate());
	}
}
