using UnityEngine;

public class ShipController : MonoBehaviour
{
    private ShipAttackBase shipAttackBase;
	public ShipAttackBase ShipAttackBase { get => shipAttackBase; 
		set => shipAttackBase = value; }

	private ShipTakeItem shipTakeItem;
	public ShipTakeItem ShipTakeItem { get => shipTakeItem;
		set => shipTakeItem = value; }

	private void Start()
	{
		shipAttackBase = GetComponent<ShipAttackBase>();
		shipTakeItem = GetComponent<ShipTakeItem>();
	}
}
