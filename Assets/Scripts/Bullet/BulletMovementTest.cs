using UnityEngine;

public class BulletMovementTest : MonoBehaviour
{
    private Vector3 direction;
    private float moveSpeed;

	private void FixedUpdate()
	{
        transform.Translate(moveSpeed * Time.fixedDeltaTime * direction);
	}

	public void SetInfo(Vector3 pDirection, float pMoveSpeed)
    {
        direction = pDirection;
        moveSpeed = pMoveSpeed;
    }
}
