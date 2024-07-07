using UnityEngine;

public class LoadSceneBtn : MonoBehaviour
{
    public void LooadSecne(string sceneName)
    {
        SingletonManager.Instance.LoadSceneManager.LoadScene(sceneName);
    }
}
