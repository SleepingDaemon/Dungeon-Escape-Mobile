using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartScene()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void MenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
