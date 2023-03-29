
using UnityEngine;

public class Sauvegarde : MonoBehaviour
{
    public MissileDirection missileDirection;
    public DayNightController dayNightController;
    public int score;
    public int niveaux;
    public string heure;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = missileDirection.score;
        niveaux = missileDirection.niveaux;
        heure = dayNightController.heure.text;
    }
}
