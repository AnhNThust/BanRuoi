using UnityEngine;
public interface IPooling
{
	/// <summary>
	/// Get Pool ID
	/// </summary>
	/// <returns></returns>
	int GetID ();
	float InstanceID { set; get; }
}
