using Assets.Scripts.Enum;
using System.Collections;
using UnityEngine;

public class ShowLaserOpenDimension : MonoBehaviour
{
	[SerializeField] private Transform dimensionPosition;

	IEnumerator ShowFireRay()
	{
		yield return new WaitForSeconds(Random.Range(1f, 2f));

		GameObject laser = PoolingManager.GetObject(EffectID.LIGHTING_22, transform.position, Quaternion.identity);
		laser.GetComponent<HideLaser>().origin = transform;
		laser.GetComponent<HideLaser>().Target = dimensionPosition;

		laser.SetActive(true);
	}

	private void Start()
	{
		StartCoroutine(ShowFireRay());
	}
}
