using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnStartScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    
    public void Play(int scene)
    {
        StartCoroutine(LoadAsync(scene));
    }

    IEnumerator LoadAsync(int scene)
    {
        AsyncOperation opperation = SceneManager.LoadSceneAsync(scene);

        loadingScreen.SetActive(true);
        while (!opperation.isDone)
        {
            float progress = Mathf.Clamp01(opperation.progress / 0.9f);
            slider.value = progress;
            progressText.text = Mathf.Round(progress * 100) + "%";
            yield return null;
        }
    }
}
