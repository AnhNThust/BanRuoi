using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LaserBeamTest : MonoBehaviour
{
	public Vector3 position;
	public Vector3 direction;
	public LineRenderer laser;
	private readonly List<Vector3> laserIndices = new();

	public LayerMask layerMask;
	private readonly List<Vector3> dirs = new();
	public EdgeCollider2D edgeCollider;

	public float timeHide;

	IEnumerator HideLaser()
	{
		yield return new WaitForSeconds(timeHide);
		Destroy(gameObject);
	}

	private void OnEnable()
	{
		transform.localPosition = Vector3.zero;
		laser = GetComponent<LineRenderer>();
		edgeCollider = GetComponent<EdgeCollider2D>();
		DrawRay(position, direction);
		StartCoroutine(HideLaser());
	}

	private void DrawRay(Vector3 pos, Vector3 dir)
	{
		dirs.Add(dir);
		if (laserIndices.Count > 0 && laserIndices[^1] == pos) return;

		laserIndices.Add(pos);
		RaycastHit2D hit = Physics2D.Raycast(pos, dir, 10, layerMask);

		laser.positionCount = laserIndices.Count;
		for (int i = 0; i < laserIndices.Count; i++)
		{
			laser.SetPosition(i, laserIndices[i]);
		}

		if (hit.collider != null && hit.collider.CompareTag("Mirror"))
		{
			Vector2 newPos = hit.point.x < 0 ?
				hit.point + new Vector2(+0.14f, +0.14f) :
				hit.point + new Vector2(-0.14f, +0.14f);
			Vector2 mirrorNormal = hit.normal;
			Vector3 newDirection = Vector2.Reflect(dir, mirrorNormal);

			DrawRay(newPos, newDirection);
		}
		else
		{
			Vector3 newPos = pos + 10f * dir;
			laserIndices.Add(newPos);
			laser.positionCount = laserIndices.Count;

			var points = new List<Vector2>();
			edgeCollider.points = new Vector2[laserIndices.Count];

			for (int i = 0; i < laserIndices.Count; i++)
			{
				laser.SetPosition(i, laserIndices[i]);
				points.Add(laserIndices[i]);
			}
			edgeCollider.SetPoints(points);
		}
	}

	public void SetInfo(Vector3 pos, Vector3 dir, float time)
	{
		position = pos;
		direction = dir;
		timeHide = time;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			UIManager.UpdateLife(-1);
			Destroy(gameObject);
		}
	}
}
