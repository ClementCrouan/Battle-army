using UnityEngine;
using UnityEngine.UI;

public class ScoreHeureUI : MonoBehaviour
{
    public GameObject sauvegardes;
    public Sauvegarde sauvegarde;
    public Text score;
    public Text heure;
    public Text niveaux;

    // Start is called before the first frame update
    void Start()
    {
        sauvegardes = GameObject.Find("Sauvegarde");
        sauvegarde = (Sauvegarde)FindObjectOfType(typeof(Sauvegarde));
        score.text = "Score : " + sauvegarde.score;
        niveaux.text = "Niveaux : " + sauvegarde.niveaux;
        heure.text = sauvegarde.heure;
        Destroy(sauvegardes);
    }
}
