using Assets.Scripts.Enum;
using System.Collections;
using UnityEngine;

public class EnemyAttackType1 : MonoBehaviour
{
    [SerializeField] private int bulletId;
    [SerializeField] private float coolDown;
    [SerializeField] private GameObject barrelAttack;
    [SerializeField] private int numberBullet;
    [SerializeField] private int numbelBarrel;

    private IEnumerator Attack1()
    {
        while (true)
        {
            yield return new WaitForSeconds(coolDown);

            for (int i = 0; i < numbelBarrel; i++)
            {
                for (int j = 0; j < numberBullet; j++)
                {
					GameObject bullet = PoolingManager.GetObject(bulletId,
                        barrelAttack.transform.GetChild(i).position, 
                        barrelAttack.transform.GetChild(i).rotation);
					BulletSpawnBullet bSB = bullet.AddComponent(typeof(BulletSpawnBullet)) as BulletSpawnBullet;
					bSB.SetInfo(BulletID.ENEMY2_BULLET, 3, Random.Range(1f, 2f), 90);
					bullet.SetActive(true);

					yield return new WaitForSeconds(coolDown / numberBullet);
				}
			}
		}
    }

	private void Start()
	{
        CreateBarrel();
		StartCoroutine(Attack1());
	}

	private void OnDisable()
	{
		Destroy(GetComponent<EnemyAttackType1>());
	}

	public void SetInfo(EnemyBulletType pBulletId, int pNumberBulletInAttack, float pCoolDown, int pNumberBarrel)
    {
        bulletId = (int)pBulletId;
        numberBullet = pNumberBulletInAttack;
        coolDown = pCoolDown;
        numbelBarrel = pNumberBarrel;
    }

    private void CreateBarrel()
    {
        barrelAttack = new("BarrelAttack");
        barrelAttack.transform.SetLocalPositionAndRotation(transform.position, Quaternion.identity);
        barrelAttack.transform.SetParent(transform, false);
        for (int i = 0; i < numbelBarrel; i++)
        {
            GameObject barrel = new($"Barrel{i + 1}");
            barrel.transform.SetLocalPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 180));
            barrel.transform.SetParent(barrelAttack.transform);
        }
    }
}
