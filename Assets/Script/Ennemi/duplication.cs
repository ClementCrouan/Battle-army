using UnityEngine;

public class duplication : MonoBehaviour
{
	[Header("GameObject")]
	public GameObject ennemi;
	public GameObject autre;

	[Header("Info")]
	public string arme;
	public float chrono;	
	[Range(1f,10f)]
	public float difficultéDépart;
	public bool stop;

	[Header("Script")]
	public MissileDirection missileDirection;
	public MissileTiré missileTiré;

	GameObject difficulteSauvegarde;

	const int z = 0;
	
    // Start is called before the first frame update
    void Start()
    {
		chrono = 0;
		stop = false;
		missileDirection.nbEnnemi++;
		difficulteSauvegarde = GameObject.Find("Difficulté Sauvegarde");
		difficultéDépart = difficulteSauvegarde.GetComponent<DifficulteSauvegarde>().difficultéDépart;
	}

    // Update is called once per frame
    void Update()
    {		
		if(!missileDirection.pause)
		{
			if (chrono > 6000)
			{
				stop = true;

				if (missileDirection.nbEnnemi < difficultéDépart * missileDirection.niveaux)
				{
					if (missileTiré.dupliquer && arme == "Arc")
                    {
						Duplique();
						missileTiré.dupliquer = false;
					}

					if (arme == "Epée")
					{
						Duplique();
					}

				}
			}

			if (missileDirection.nbEnnemi == difficultéDépart * missileDirection.niveaux)
			{
				chrono = z;
			}

			if (!stop)
			{
				chrono += missileDirection.niveaux * difficultéDépart;
			}
		}
	}

	void Duplique()
    {
		chrono = z;
		autre = Instantiate(ennemi, transform.position, Quaternion.identity) as GameObject;
		autre.tag = ("Clone");
		stop = false;
	}
}