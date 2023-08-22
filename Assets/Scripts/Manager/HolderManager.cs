using UnityEngine;

public class HolderManager : MonoBehaviour
{
    public Transform[] holders;

	[ContextMenu("Reload")]
	private void Reload()
	{
		holders = new Transform[transform.childCount];
		for (int i = 0; i < holders.Length; i++)
		{
			holders[i] = transform.GetChild(i);
		}
	}
}
