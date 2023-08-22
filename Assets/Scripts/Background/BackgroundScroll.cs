using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private new MeshRenderer renderer;

	private void Start()
	{
		renderer = GetComponent<MeshRenderer>();
	}

	private void Update()
	{
		renderer.material.mainTextureOffset = new Vector2(0, Time.time * speed);
	}
}
