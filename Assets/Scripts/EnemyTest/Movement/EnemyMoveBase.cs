using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

public class EnemyMoveBase : MonoBehaviour
{
	[SerializeField] private EnemyControllerTest controller;
	[SerializeField] private Transform body;

	public EnemyControllerTest Controller { get => controller; set => controller = value; }
	public Transform Body { get => body; set => body = value; }
	public TweenerCore<Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> TweenerCore { get; set; }

	protected virtual void OnEnable()
	{
		Controller = GetComponent<EnemyControllerTest>();
		Body = Controller.EnemyProperties.Body;
	}

	protected virtual void Update()
	{
		Body.localRotation = transform.rotation;
		transform.rotation = Quaternion.identity;
	}

	protected virtual void OnDisable()
	{
		TweenerCore.Kill();
		Destroy(GetComponent<EnemyMoveBase>());
	}
}
