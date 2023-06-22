using UnityEngine;
using Random = UnityEngine.Random;

public class EffectPowerUp : MonoBehaviour
{
	private new SpriteRenderer renderer;
	private float timer = 0;
	private readonly float delay = 5f;

	private float timeLerp = 0.25f;
	private Color targetColor;

	private void Start()
	{
		renderer = GetComponent<SpriteRenderer>();
		targetColor = new(Random.value, Random.value, Random.value);
	}

	private void Update()
	{
		ShowWing();
		timer += Time.deltaTime;
		if (timer < delay) return;
		timer = 0;
		gameObject.SetActive(false);
	}

	private void ShowWing()
	{
		if (timeLerp <= Time.deltaTime)
		{
			renderer.color = targetColor;
			targetColor = new(Random.value, Random.value, Random.value);
			timeLerp = 0.5f;
		}
		else
		{
			renderer.color = Color.Lerp(renderer.color, targetColor, Time.deltaTime / timeLerp);
			timeLerp -= Time.deltaTime;
		}
	}
}
