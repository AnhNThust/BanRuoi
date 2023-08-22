using UnityEngine;

public class EnemyControllerTest : MonoBehaviour
{
    [SerializeField] private EnemyPropertiesTest enemyProperties;
	[SerializeField] private bool isReady;

	public EnemyPropertiesTest EnemyProperties { get => enemyProperties; set => enemyProperties = value; }
	public bool IsReady { get => isReady; set => isReady = value; }

	private void OnEnable()
	{
		EnemyProperties = GetComponent<EnemyPropertiesTest>();
	}

	private void OnDisable()
	{
		isReady = false;
	}
}
