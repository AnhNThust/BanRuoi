using UnityEngine;
using System.Collections.Generic;

public class PoolingManager : MonoBehaviour
{
	//==================== Configs				====================

	#region Configs
	static PoolingManager instance = null;
	static Dictionary<string, GameObject> prefabLookup = null;

	const string resourcesPaths = "PoolingObjects";
	private Vector3 prefPos = new Vector3(-99, -99, -99);
	private Dictionary<string, List<GameObject>> instanceLookup = null;

	private Transform parentOff;
	public float currentInstanceID = 0;
	#endregion

	//==================== Unity methods		====================

	#region Unity methods
	void Awake()
	{
		//Singleton.
		instance = this;

		//Cache.
		if (prefabLookup == null)
		{
			prefabLookup = new Dictionary<string, GameObject>();

			GameObject[] objPrefabs = Resources.LoadAll<GameObject>(resourcesPaths);

			foreach (var obj in objPrefabs)
			{
				IPooling iPooling = obj.GetComponent<IPooling>();

				if (iPooling != null)
				{
					prefabLookup.Add(iPooling.GetID().ToString(), obj);
				}
			}
		}

		if (instanceLookup != null)
		{
			instanceLookup.Clear();
		}
		else
		{
			instanceLookup = new Dictionary<string, List<GameObject>>();
		}

		parentOff = new GameObject("ParentOff").transform;
		parentOff.SetParent(instance.transform);
		parentOff.gameObject.SetActive(false);
	}
	#endregion

	//==================== Public methods		====================

	#region Public methods
	public GameObject GetObjectBase(int _index, Vector3 _position, Quaternion _rotation)
	{
		string key = _index.ToString();

		if (prefabLookup.ContainsKey(key))
		{
			GameObject objPool;
			if (instanceLookup.ContainsKey(key) && instanceLookup[key].Count > 0)
			{
				//get object instance.
				objPool = instanceLookup[key][0];
				objPool.transform.position = prefPos;
				instanceLookup[key].RemoveAt(0);
			}
			else
			{
				//get new instance prefabs.
				GameObject objPrefabs = prefabLookup[key];
				objPool = Instantiate(objPrefabs, prefPos, Quaternion.identity, parentOff);
				objPool.SetActive(false);
				objPool.transform.SetParent(null);
			}

			objPool.transform.SetPositionAndRotation(_position, _rotation);
			objPool.GetComponent<IPooling>().InstanceID = currentInstanceID;
			currentInstanceID++;
			return objPool;
		}
		else
		{
			Debug.LogWarning(string.Format("No object prefabs. [{0}]", key));
			return null;
		}
	}

	public GameObject GetObjectInstanceId(GameObject objPool)
	{
		objPool.GetComponent<IPooling>().InstanceID = currentInstanceID;
		currentInstanceID++;
		return objPool;
	}

	public void PoolObjectBase(GameObject _clone)
	{
		if (_clone == null)
			return;
		_clone.SetActive(false);
		IPooling objPool = _clone.GetComponent<IPooling>();

		if (objPool.GetID() > 0)
		{
			string key = objPool.GetID().ToString();

			if (!instanceLookup.ContainsKey(key))
			{
				instanceLookup.Add(key, new List<GameObject>());
			}

			if (!instanceLookup[key].Contains(_clone))
			{
				if (transform != null && _clone != null)
				{
					_clone.transform.SetParent(this.transform);
					instanceLookup[key].Add(_clone);
				}
			}
			//else
			//{
			//	Debug.LogWarning("What the f**k, pool many time.");
			//}
		}
	}
	#endregion

	//==================== Static methods		====================
	#region Static API
	public static GameObject GetObject(int index)
	{
		return instance.GetObjectBase(index, new Vector3(-255, -255, -255), Quaternion.identity);
	}

	public static GameObject GetObject(int index, Vector3 position)
	{
		return instance.GetObjectBase(index, position, Quaternion.identity);
	}

	public static GameObject GetObject(int index, Vector3 position, Quaternion rotation)
	{
		return instance.GetObjectBase(index, position, rotation);
	}

	public static GameObject GetObject(int index, Transform lookAt)
	{
		return instance.GetObjectBase(index, lookAt.position, lookAt.rotation);
	}

	public static GameObject GetObject(GameObject obj)
	{
		return instance.GetObjectInstanceId(obj);
	}

	public static void PoolObject(GameObject clone)
	{
		instance.PoolObjectBase(clone);
	}
	#endregion
	//==================== Ended				====================
}
