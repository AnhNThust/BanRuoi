using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float moveSpeed;

    private float distanceTravelled;

	private void Start()
	{
		pathCreator = FindObjectOfType<PathCreator>();
	}

	private void Update()
	{
		distanceTravelled += moveSpeed * Time.deltaTime;
		transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);

		//if (pathCreator.path.)
		//{

		//}
	}
}
