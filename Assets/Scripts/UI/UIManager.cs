using Assets.Scripts.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	private static UIManager instance;

	[Header("Icon and Text")]
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI waveText;
	public Transform warningAlert;
	public TextMeshProUGUI lifeText;
	public Transform[] bulletLevelUI;
	[SerializeField] private Transform[] buttons;

	[Header("Panel")]
	public Transform vicPanel;
	public Transform defPanel;
	[SerializeField] private Transform pausePanel;

	[Header("Scene")]
	public string nameCurrentScene;
	public string nameNextScene;
	[SerializeField] private string nameMenuScene;

	public ShipController shipController;
	private int blLevel;
	private int score;
	private int life;

	private void Awake()
	{
		if (instance != null) Debug.LogError("Only 1 UIManager allow exist");
		instance = this;
	}

	private void Start()
	{
		life = 3;
		score = 0;

		lifeText.text = $"x {life}";
		scoreText.text = "0";
	}

	private void AddScoreBase(int pScore)
	{
		score += pScore;
		scoreText.text = score.ToString();
	}

	private void ShowWaveTextBase(int pWaveIndex)
	{
		waveText.text = $"Wave {pWaveIndex}";
		waveText.gameObject.SetActive(true);
	}

	private void ShowWarningUIBase()
	{
		warningAlert.gameObject.SetActive(true);
	}

	private void UpdateLifeBase(int pLife)
	{
		life += pLife;
		lifeText.text = $"x {life}";
	}

	private void ShowVicPanelBase()
	{
		ActiveButton(false);
		vicPanel.gameObject.SetActive(true);
	}

	private void ShowDefPanelBase()
	{
		ActiveButton(false);
		defPanel.gameObject.SetActive(true);
	}

	private void ShowPausePanelBase()
	{
		ActiveButton(false);
		Time.timeScale = 0;
		pausePanel.gameObject.SetActive(true);
	}

	public void ReplayGame()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(nameCurrentScene);
	}

	public void ContinueGame()
	{
		Time.timeScale = 1.0f;
		pausePanel.gameObject.SetActive(false);
		ActiveButton(true);
	}

	public void NextMap()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(nameNextScene);
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene(nameMenuScene);
	}

	private void NextBulletLevel()
	{
		if (shipController.ShipAttackBase.BulletLevel >
			shipController.ShipAttackBase.MaxBulletLevel) return;
		blLevel = shipController.ShipAttackBase.BulletLevel;
		Image image = bulletLevelUI[blLevel - 1].GetComponent<Image>();
		image.color = Color.white;
	}

	public void ChangeShip1(int id)
	{
		shipController.ShipTakeItem.ChangeShip(id, BulletID.SHIP1_BULLET);
	}

	public void ChangeShip2(int id)
	{
		shipController.ShipTakeItem.ChangeShip(id, BulletID.SHIP2_BULLET);
	}

	public void ChangeShip3(int id)
	{
		shipController.ShipTakeItem.ChangeShip(id, BulletID.SHIP3_BULLET);
	}

	public void ChangeShip4(int id)
	{
		shipController.ShipTakeItem.ChangeShip(id, BulletID.SHIP4_BULLET);
	}

	private void ActiveButton(bool flag)
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].gameObject.SetActive(flag);
		}
	}

	// ===================================
	public static void AddScore(int pScore)
	{
		instance.AddScoreBase(pScore);
	}

	public static void ShowWaveText(int pWaveIndex)
	{
		instance.ShowWaveTextBase(pWaveIndex);
	}

	public static void ShowWarningUI()
	{
		instance.ShowWarningUIBase();
	}

	public static void UpdateLife(int pLife)
	{
		instance.UpdateLifeBase(pLife);
	}

	public static void ShowVicPanel()
	{
		instance.ShowVicPanelBase();
	}

	public static void ShowDefPanel()
	{
		instance.ShowDefPanelBase();
	}

	public static void ShowPausePanel()
	{
		instance.ShowPausePanelBase();
	}

	public static int GetLife()
	{
		return instance.life;
	}

	public static void ShowNextBulletLevel()
	{
		instance.NextBulletLevel();
	}
}
