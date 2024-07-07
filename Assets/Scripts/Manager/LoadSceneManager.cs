using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using System.Threading.Tasks;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] private LoadingSlider loadingSlider;
    private string m_NextScene;

    public async Task LoadScene(string sceneName)
    {
        SceneManager.LoadScene("Loading");
        m_NextScene = sceneName;
        await LoadSceneAsync();
    }

    public async Task LoadSceneCallback(string sceneName, Action doComplete)
    {
        SceneManager.LoadScene("Loading");
        m_NextScene = sceneName;
        await LoadSceneAsync(doComplete);
    }

    private async Task LoadSceneAsync(Action doComplete = null)
    {
        // Ensure the current frame has ended before starting the async operation
        await Task.Yield();

        AsyncOperation op = SceneManager.LoadSceneAsync(m_NextScene);
        op.allowSceneActivation = false;

        // Initialize the slider
        if (loadingSlider != null)
        {
            loadingSlider.SetSlider(0);
        }

        float timer = 0.0f;

        while (!op.isDone)
        {
            await Task.Yield();
            timer += Time.deltaTime;

            float progress = Mathf.Clamp01(op.progress / 0.9f);
            if (loadingSlider != null)
            {
                loadingSlider.SetSlider(progress);
            }

            if (progress >= 1f)
            {
                op.allowSceneActivation = true;
            }
        }

        // Ensure that the scene has been fully loaded
        await Task.Yield();

        doComplete?.Invoke();
    }
}
