using UnityEngine;
using UnityEngine.UI;

public class DifficulteSauvegarde : MonoBehaviour
{
    public Slider slider;
    public ReturnStartScene returnStartScene;
    public float difficultéDépart;
    public Text textDifficulté;

    void Start()
    {
        returnStartScene.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (textDifficulté != null)
        {
            difficultéDépart = slider.value;
            textDifficulté.text = difficultéDépart + "";
        }
    }
}
