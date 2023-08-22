using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuManager : MonoBehaviour
{
    public void LoadMap(int mapId)
    {
        SceneManager.LoadScene(mapId);
        Time.timeScale = 1.0f;
    }
}
