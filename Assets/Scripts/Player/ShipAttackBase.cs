using UnityEngine;

public class ShipAttackBase : MonoBehaviour
{
	[SerializeField] protected Transform[] barrelParents;
	public Transform[] BarrelParents { get => barrelParents; }

	[SerializeField] protected int barrelParentId;

	[SerializeField] private int bulletLevel = 0;
	public int BulletLevel { get => bulletLevel; set => bulletLevel = value; }

	[SerializeField] private int maxBulletLevel = 9;
	public int MaxBulletLevel { get => maxBulletLevel; set => maxBulletLevel = value; }

	[SerializeField] private bool isUpgrade = false;
	public bool IsUpgrade { get => isUpgrade; set => isUpgrade = value; }

	[SerializeField] private bool isPowerUp = false;
	public bool IsPowerUp { get => isPowerUp; set => isPowerUp = value; }

	[SerializeField] private bool isCreated = false; // flag Create Barrel when ship in Power State
	public bool IsCreated { get => isCreated; set => isCreated = value; }

	private void Start()
	{
		ActiveBarrel(0);
	}

	public void ActiveBarrel(int id)
	{
		for (int i = 0; i < barrelParents.Length; i++)
		{
			if (i == id) continue;
			barrelParents[i].gameObject.SetActive(false);
		}
		barrelParents[id].gameObject.SetActive(true);
	}

	public void UpBulletLevel()
	{
		if (bulletLevel >= maxBulletLevel) return;
		bulletLevel++;
		isUpgrade = true;
	}

	public void PowerUp()
	{
		isPowerUp = true;
		isCreated = true;
	}

	public void StopAttack()
	{
		for (int i = 0; i < barrelParents.Length; i++)
		{
			if (!barrelParents[i].gameObject.activeSelf) continue;

			barrelParents[i].gameObject.SetActive(false);
		}
	}
}