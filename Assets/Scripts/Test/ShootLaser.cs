using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public Material material;

    private LaserBeam laserBeam;

	private void Update()
	{
		Destroy(GameObject.Find("Laser_Beam"));
		laserBeam = new LaserBeam(transform.position, -transform.up, material);
	}
}
