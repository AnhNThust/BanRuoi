using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed;

    private Renderer rd;

	private void Start()
	{
		rd = GetComponent<Renderer>();
	}

	private void Update()
	{
		float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
		Vector2 offset = new(x, 0);
		rd.material.mainTextureOffset = offset;
	}
}
