using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;

	public Transform player;
	public Transform wave1;

	private void Awake()
	{
		if (instance != null) Debug.LogError("Only 1 GameManager allow exist");
		instance = this;
	}

	private void Start()
	{
		StartCoroutine(StartWave());
	}

	IEnumerator StartWave()
	{
		yield return new WaitForSeconds(2f);
		UIManager.ShowWaveText(1);

		yield return new WaitForSeconds(3f);
		wave1.gameObject.SetActive(true);
	}

	private void CallWaveBase(Transform _wave)
	{
		_wave.gameObject.SetActive(true);
	}

	public static void CallWave(Transform _wave)
	{
		instance.CallWaveBase(_wave);
	}
}
