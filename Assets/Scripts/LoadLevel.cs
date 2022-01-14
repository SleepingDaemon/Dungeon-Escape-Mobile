using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private Image _barIMG = null;

    private void Start()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        StartCoroutine(LoadLevelAsyncRoutine());
    }

    private void OnSceneUnloaded(Scene current)
    {

    }

    IEnumerator LoadLevelAsyncRoutine()
    {
        AsyncOperation _asyncOperation = SceneManager.LoadSceneAsync(2);

        while (!_asyncOperation.isDone)
        {
            _barIMG.fillAmount = _asyncOperation.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
