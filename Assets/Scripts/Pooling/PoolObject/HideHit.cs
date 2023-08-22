using UnityEngine;

public class HideHit : MonoBehaviour
{
	public float timeShow;

	private void OnEnable()
	{
		Invoke(nameof(Hiding), timeShow);
	}

	public void Hiding()
	{
		PoolingManager.PoolObject(gameObject);
	}
}
