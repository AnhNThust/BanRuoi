using System.Collections;
using UnityEngine;

public class EnemyEffectControllerTest : MonoBehaviour
{
	[SerializeField] private Transform[] effects;
	[SerializeField] private EnemyControllerTest controller;

	public Transform[] Effects { get => effects; set => effects = value; }
	public EnemyControllerTest Controller { get => controller; set => controller = value; }

	private IEnumerator ShowEffects()
	{
		yield return new WaitUntil(() => controller.IsReady);

		for (int i = 0; i < controller.EnemyProperties.Effects.Length; i++)
		{
			controller.EnemyProperties.Effects[i].gameObject.SetActive(true);
		}
	}

	private void Start()
	{
		Controller = GetComponent<EnemyControllerTest>();
		StartCoroutine(ShowEffects());
	}
}
