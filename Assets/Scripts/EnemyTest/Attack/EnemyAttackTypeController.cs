using Assets.Scripts.Enum;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTypeController : MonoBehaviour
{
    private static EnemyAttackTypeController instance;
	//private Dictionary<EnemyAttackType, >

	public static EnemyAttackTypeController Instance { get => instance; set => instance = value; }

	private void Awake()
	{
		if (instance != null) Debug.Log("Only 1 EnemyAttackTypeController exist");
		instance = this;
	}
}
