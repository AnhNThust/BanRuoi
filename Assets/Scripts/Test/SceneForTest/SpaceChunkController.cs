using UnityEngine;

public class SpaceChunkController : MonoBehaviour
{
    [SerializeField] private Transform[] rocks;

	private void Start()
	{
		rocks = new Transform[transform.childCount];
		for (int i = 0; i < rocks.Length; i++)
		{
			rocks[i] = transform.GetChild(i);
		}
	}

	private void Update()
	{
		for (int i = 0; i < rocks.Length; i++)
		{
			if (rocks[i].localPosition.y > -0.6) continue;

			ReMove(rocks[i]);
		}
	}

	private void ReMove(Transform pObject)
	{
		float xRand = Random.Range(-0.46f, 0.46f);
		float yRand = Random.Range(0.6f, 1.2f);

		pObject.transform.localPosition = new(xRand, yRand, 0);
	}
}
