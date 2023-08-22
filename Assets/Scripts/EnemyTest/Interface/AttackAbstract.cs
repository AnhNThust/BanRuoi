using UnityEngine;

namespace EnemyTest.Interface
{
    public abstract class AttackAbstract : MonoBehaviour
    {
        private int attackTypeNumber;
        private int bulletTypeNumber;
        private float coolDown;

        protected virtual void Start()
        {
            for (var i = 0; i < attackTypeNumber; i++)
            {
                // StartCoroutine();
            }
        }
        
        protected virtual void SetInfo(int pAttackTypeNumber, int pBulletTypeNumber, float pCoolDown)
        {
            attackTypeNumber = pAttackTypeNumber;
            bulletTypeNumber = pBulletTypeNumber;
            coolDown = pCoolDown;
        }
    }
}