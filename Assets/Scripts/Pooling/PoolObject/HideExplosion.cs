using UnityEngine;

public class HideExplosion : MonoBehaviour
{
	private void OnEnable()
	{
		Invoke(nameof(Hiding), 1.5f);
	}

	private void Hiding()
	{
		PoolingManager.PoolObject(gameObject);
	}
}
