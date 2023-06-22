using System.Collections;
using UnityEngine;

public class CheckHolder : MonoBehaviour
{
	public Transform holder;
	public Transform nextWave;
	public int waveNumber;

	private void Start()
	{
		StartCoroutine(CallNextWave());
	}

	IEnumerator CallNextWave()
	{
		while (true)
		{
			yield return new WaitForSeconds(3f);

			if (holder.childCount <= 0)
			{
				if (waveNumber > 0)
				{
					UIManager.ShowWaveText(waveNumber);
					yield return new WaitForSeconds(2f);
					GameManager.CallWave(nextWave);
				}
				else
				{
					UIManager.ShowWarningUI();
					GameManager.CallWave(nextWave);
				}

				yield return new WaitForSeconds(1f);
				gameObject.SetActive(false);
			}
		}
	}
}
