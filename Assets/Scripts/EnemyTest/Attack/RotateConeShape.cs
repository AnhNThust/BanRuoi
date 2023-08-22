using DG.Tweening;
using UnityEngine;

public class RotateConeShape : MonoBehaviour
{
    [SerializeField] private float endAngle;
	[SerializeField] private float duration;

	private void Start()
	{
		transform.DORotate(new Vector3(0, 0, endAngle), duration, RotateMode.Fast)
			.SetLoops(-1, LoopType.Yoyo);
	}

	public void SetInfo(float pEndAngle, float pDuration)
    {
        endAngle = pEndAngle;
		duration = pDuration;
    }
}
