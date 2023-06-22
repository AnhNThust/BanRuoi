using Assets.Scripts.Enum;
using Spine.Unity;
using UnityEngine;

public class ShipTakeItem : MonoBehaviour
{
	[SerializeField] private Transform playerWing;
	[SerializeField] private SkeletonDataAsset[] skeletonDataAssets;
	[SerializeField] private Transform[] shipTrailFlames;

	private ShipController shipController;

	private void Start()
	{
		shipController = GetComponent<ShipController>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Item"))
		{
			int itemID = collision.gameObject.GetComponent<ObjectPool>().GetID();
			switch (itemID)
			{
				case ItemID.ITEM_UPGRADE:
					shipController.ShipAttackBase.UpBulletLevel();
					UIManager.ShowNextBulletLevel();
					break;

				case ItemID.ITEM_POW:
					shipController.ShipAttackBase.PowerUp();
					playerWing.gameObject.SetActive(true);
					break;

				case ItemID.ITEM_CHANGE_SHIP_1:
					ChangeShip(0, BulletID.SHIP1_BULLET);
					break;

				case ItemID.ITEM_CHANGE_SHIP_2:
					ChangeShip(1, BulletID.SHIP2_BULLET);
					break;

				case ItemID.ITEM_CHANGE_SHIP_3:
					ChangeShip(2, BulletID.SHIP3_BULLET);
					break;

				case ItemID.ITEM_CHANGE_SHIP_4:
					ChangeShip(3, BulletID.SHIP4_BULLET);
					break;

				case ItemID.ITEM_ADD_A_LIFE:
					UIManager.UpdateLife(1);
					break;

				default:
					break;
			}
			UIManager.AddScore(300);
			PoolingManager.PoolObject(collision.gameObject);
		}
	}

	public void ChangeShip(int index, int bulletId)
	{
		SkeletonAnimation skeletonAnimation = GetComponent<SkeletonAnimation>();
		skeletonAnimation.skeletonDataAsset = skeletonDataAssets[index];
		skeletonAnimation.Initialize(true);

		for (int i = 0; i < shipTrailFlames.Length; i++) // Thay duoi lua cho tau
		{
			if (i != index)
			{
				shipTrailFlames[i].gameObject.SetActive(false);
			}
		}
		shipTrailFlames[index].gameObject.SetActive(true);

		shipController.ShipAttackBase.ActiveBarrel(index);
	}
}
