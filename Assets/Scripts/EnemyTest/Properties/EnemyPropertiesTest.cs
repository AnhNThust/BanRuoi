using UnityEngine;

public class EnemyPropertiesTest : MonoBehaviour
{
	[SerializeField] private Transform body;
	[SerializeField] private float currentHp;
    [SerializeField] private float totalHp;
	[SerializeField] private Transform[] effects;

	public float CurrentHp { get => currentHp; set => currentHp = value; }
	public float TotalHp { get => totalHp; set => totalHp = value; }
	public Transform Body { get => body; set => body = value; }
	public Transform[] Effects { get => effects; set => effects = value; }
}
