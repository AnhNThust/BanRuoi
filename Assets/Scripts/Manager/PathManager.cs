using DG.Tweening;
using UnityEngine;

namespace Manager
{
	public class PathManager : MonoBehaviour
	{
		private static PathManager Instance { get; set; }

		private void Awake()
		{
			if (Instance != null) Debug.LogError("Only 1 PathManager allow exist");
			Instance = this;
		}

		public static DOTweenPath GetPathByIndex(int index)
		{
			return Instance.transform.GetChild(index).GetComponent<DOTweenPath>();
		}
	}
}
