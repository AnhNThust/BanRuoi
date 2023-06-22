using UnityEngine;

public class ElectricLineController : MonoBehaviour
{
	public Texture[] textures;
	public float fps = 30f;

	private LineRenderer lineRenderer;
	private int animationStep;
	private float fpsCounter;

	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
	}

	private void Update()
	{
		fpsCounter += Time.deltaTime;

		if (fpsCounter >= 1f / fps)
		{
			animationStep++;
			if (animationStep == textures.Length)
			{
				animationStep = 0;
			}

			lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);

			fpsCounter = 0;
		}
	}
}
