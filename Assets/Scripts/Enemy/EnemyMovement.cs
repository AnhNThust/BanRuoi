using DG.Tweening;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EnemyMovement : MonoBehaviour
{
    public DOTweenPath path;
    public Transform target;
    public float timeMove;

    public void SetInfo(Transform pTarget)
    {
        target = pTarget;

        if (path != null)
        {
            Move();
        }
    }

    private void Move()
    {
        bool flip = transform.position.x < 0;
        var points = path.wps.ConvertAll(p => new Vector3(p.x *= flip ? -1 : 1, p.y));
        points.Add(target.position);
        transform.DOPath(points.ToArray(), timeMove, PathType.CatmullRom, PathMode.TopDown2D)
            .SetEase(Ease.Linear)
            .SetLookAt(0.01f, transform.forward, Vector3.left)
            .OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        transform.up = Vector3.up;
        if (transform.position == target.position && GetComponent<EnemyAttack>() != null)
        {
            GetComponent<EnemyAttack>().enabled = true;
        }
    }

    public void MoveCompleted()
    {
        Debug.Log("Move done!");
    }
}