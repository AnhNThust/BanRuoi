using System.Collections;
using UnityEngine;

public class SpawnerSpecialBulletController : MonoBehaviour
{
    [SerializeField] private Transform[] spawners;
    [SerializeField] private float timeSpawn;

	public Transform[] Spawners { get => spawners; set => spawners = value; }
	public float TimeSpawn { get => timeSpawn; set => timeSpawn = value; }

	[ContextMenu("Reload")]
    private void Reload()
    {
        spawners = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            spawners[i] = transform.GetChild(i);
        }
    }

    private IEnumerator RandomAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeSpawn);

			int rand = GetSpawnPosition();

            for (int i = 0; i < spawners.Length; i++)
            {
				if (rand % 2 == 0 && i % 2 == 0)
				{
                    spawners[i].gameObject.SetActive(true);
				}
				else if (rand % 2 != 0 && i % 2 != 0)
				{
                    spawners[i].gameObject.SetActive(true);
				}
			}
        }
    }

	private void Start()
	{
		StartCoroutine(RandomAttack());
	}

    private int GetSpawnPosition()
    {
        return Random.Range(0, 2);
    }
}
