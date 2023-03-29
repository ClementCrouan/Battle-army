using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    GameObject difficulte;
    public Slider sliderDifficulté;
    public Text textDifficulté;
    DifficulteSauvegarde difficulteSauvegarde;

    // Start is called before the first frame update
    void Start()
    {
        difficulte = GameObject.Find("Difficulté Sauvegarde");
        
        if (difficulte == null)
        {
            StartCoroutine(LoadAsync(3));
        }
        else
        {
            difficulteSauvegarde = difficulte.GetComponent<DifficulteSauvegarde>();
            difficulteSauvegarde.slider = sliderDifficulté;
            difficulteSauvegarde.textDifficulté = textDifficulté;
        }
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
