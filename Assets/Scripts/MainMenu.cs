using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartScene()
    {
        Time.timeScale = 1;
        StartCoroutine(DelayLoadingSceneRoutine());
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void MenuScene()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator DelayLoadingSceneRoutine()
    {
        yield return new WaitForSeconds(1F);
        SceneManager.LoadScene(1);
    }
}
