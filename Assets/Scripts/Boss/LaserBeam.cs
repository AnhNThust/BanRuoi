using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour 
{
    public Vector3 position;
    public Vector3 direction;
    public GameObject laserObj;
    public LineRenderer laser;
	private readonly List<Vector3> laserIndices = new();

    public LaserBeam(Vector3 pPosition, Vector3 pDirection, Material pMaterial)
    {
        laser = new();
		laserObj = new()
		{
			name = "Laser_Beam"
		};

		position = pPosition;
        direction = pDirection;

        laser = laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        laser.startWidth = 0.1f;
        laser.endWidth = 0.1f;
        laser.material = pMaterial;
        laser.startColor = Color.green;
        laser.endColor = Color.green;

        CastRay(pPosition, pDirection, laser);
    }

	private void CastRay(Vector3 pPosition, Vector3 pDirection, LineRenderer pLaser)
	{
        laserIndices.Add(pPosition);

        Ray ray = new(pPosition, pDirection);

		if (Physics.Raycast(ray, out RaycastHit hit, 30, 1))
		{
			CheckHit(hit, pDirection, pLaser);
		}
		else
		{
			laserIndices.Add(ray.GetPoint(30));
			UpdateLaser();
		}
	}

	private void CheckHit(RaycastHit pHitInfo, Vector3 pDirection, LineRenderer pLaser)
	{
        if (pHitInfo.collider.gameObject.CompareTag("Mirror"))
        {
            Vector3 pos = pHitInfo.point;
            Vector3 dir = Vector3.Reflect(pDirection, pHitInfo.normal);

            CastRay(pos, dir, pLaser);
        }
        else
        {
            laserIndices.Add(pHitInfo.point);
            UpdateLaser();
        }
	}

	private void UpdateLaser()
    {
        int count = 0;

        laser.positionCount = laserIndices.Count;

        foreach (Vector3 idx in laserIndices)
        {
            laser.SetPosition(count, idx);
            count++;
        }
    }
}
