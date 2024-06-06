using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadingScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsynchronously(sceneName));
    }

    IEnumerator LoadSceneAsynchronously(string sceneName)
    {
        loadingScreen.SetActive(true);

        float progress = 0f;

        // Incrementa gradualmente el valor del slider hasta 0.9
        while (progress < 1.0f)
        {
            progress += 0.002f; // Ajusta la velocidad de carga según tu preferencia
            loadingBar.value = progress;
            yield return null;
        }

        // Espera a que la operación esté completada
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
