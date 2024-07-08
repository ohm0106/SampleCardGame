using UnityEngine;
using UnityEngine.SceneManagement;
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

    private async Task LoadSceneAsync()
    {
        await Task.Yield();

        if (loadingSlider == null)
        {
            await Task.Delay(500);
            loadingSlider = FindAnyObjectByType<LoadingSlider>();
        }

        if (loadingSlider != null)
        {
            loadingSlider.SetSlider(0, "Initializing...");
            loadingSlider.SetSlider(0.2f, "Loading Items...");
        }

        await ItemLibrary.Instance.LoadAllItemsAsync(progress =>
        {
            if (loadingSlider != null)
            {
                loadingSlider.SetSlider(progress * 0.2f + 0.2f, "Loading Items...");
            }
        });

        if (loadingSlider != null)
        {
            loadingSlider.SetSlider(0.4f, "Items Loaded.");
        }

        AsyncOperation op = SceneManager.LoadSceneAsync(m_NextScene);
        op.allowSceneActivation = false;

        float timer = 0.0f;

        while (!op.isDone)
        {
            await Task.Yield();
            timer += Time.deltaTime;

            float progress = Mathf.Clamp01(op.progress / 0.9f);
            if (loadingSlider != null)
            {
                loadingSlider.SetSlider(progress * 0.6f + 0.4f, "Loading Scene...");
            }

            if (progress >= 1f)
            {
                if (loadingSlider != null)
                {
                    loadingSlider.SetSlider(1f, "Finishing Up...");
                }
                op.allowSceneActivation = true;
            }
        }

        await Task.Yield();
         
    }
}
