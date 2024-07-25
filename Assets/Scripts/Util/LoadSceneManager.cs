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
            loadingSlider = FindFirstObjectByType<LoadingSlider>();
        }

        if (loadingSlider != null)
        {
            loadingSlider.SetSlider(0, "Initializing...");
        }

        if (loadingSlider != null)
        {
            loadingSlider.SetSlider(0.2f, "Loading Items...");
        }

        float itemLoadProgress = 0f;
        await ResourceLibrary.Instance.ItemLibrary.LoadAllItemsAsync(progress =>
        {
            itemLoadProgress = progress;
            if (loadingSlider != null)
            {
                loadingSlider.SetSlider(itemLoadProgress * 0.2f + 0.2f, "Loading Items...");
            }
        });

        if (loadingSlider != null)
        {
            loadingSlider.SetSlider(0.4f, "Items Loaded.");
        }

        if (loadingSlider != null)
        {
            loadingSlider.SetSlider(0.6f, "Loading Characters...");
        }

        float characterLoadProgress = 0f;
        await ResourceLibrary.Instance.CharacterLibrary.LoadAllItemsAsync(progress =>
        {
            characterLoadProgress = progress;
            if (loadingSlider != null)
            {
                loadingSlider.SetSlider(characterLoadProgress * 0.2f + 0.6f, "Loading Characters...");
            }
        });

        if (loadingSlider != null)
        {
            loadingSlider.SetSlider(0.8f, "Characters Loaded.");
        }

        // Load scene
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
                loadingSlider.SetSlider(progress * 0.2f + 0.8f, "Loading Scene...");
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
