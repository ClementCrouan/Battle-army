using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissileDirection : MonoBehaviour
{
	GameObject ennemi;
	GameObject ennemiEpéïste;
	GameObject image;
	GameObject pauseMenu;
	GameObject playAgainButton;
	GameObject uINbEnnemi;
	GameObject uINiveaux;
	GameObject uIScore;

	[HideInInspector] public int nbEnnemi = 1;
	[HideInInspector] public int niveaux = 1;
	[HideInInspector] public bool pause;
	[HideInInspector] public int score;
	[HideInInspector] public bool tiré = false;
	[Header("Info")]
	public float positionMaxRespawn;
	public float positionMinRespawn;

	duplication duplication;
	MissileTiré missileTiré;

	Transform ennemiPosition;
	Transform playerPosition;

	const int z = 0;

	// Start is called before the first frame update
	void Start()
	{
		ennemi = GameObject.Find("Ennemi");
		ennemiEpéïste = GameObject.Find("Ennemi Epéïste");
		image = GameObject.Find("Image");
		pauseMenu = GameObject.Find("Pause Menu");
		playAgainButton = GameObject.Find("Play Again Button");
		uINbEnnemi = GameObject.Find("Nb Ennemis (Chiffre)");
		uINiveaux = GameObject.Find("Niveaux (Chiffre)");
		uIScore = GameObject.Find("Score (Chiffre)");
		duplication = GameObject.Find("Player").GetComponent<duplication>();
		missileTiré = GameObject.Find("Player").GetComponent<MissileTiré>();
		playerPosition = GameObject.Find("Player").transform;
		ennemiPosition = ennemi.transform;
		
		pauseMenu.SetActive(false);
		playAgainButton.SetActive(false);
		image.SetActive(false);

		playerPosition.position = new Vector3(Random.Range(positionMinRespawn, positionMaxRespawn), 80, Random.Range(positionMinRespawn, positionMaxRespawn));
		ennemiPosition.position = new Vector3(Random.Range(positionMinRespawn, positionMaxRespawn), 50, Random.Range(positionMinRespawn, positionMaxRespawn));
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Cursor.visible)
			{
				Cursor.visible = false;
			}
			else
			{
				Cursor.visible = true;
			}
		}

		if (!pause)
		{
			if (playerPosition.position.y < -50)
			{
				SceneManager.LoadScene("Play Again");
			}

			uINiveaux.GetComponent<Text>().text = niveaux + "";
			uINbEnnemi.GetComponent<Text>().text = nbEnnemi + "";
			uIScore.GetComponent<Text>().text = score + "";
		}
	}

	public void respawn()
	{
		ennemi.SetActive(true);
		playAgainButton.SetActive(false);
		image.SetActive(false);
		playerPosition.position = new Vector3(Random.Range(positionMinRespawn, positionMaxRespawn), 80, Random.Range(positionMinRespawn, positionMaxRespawn));
		ennemiPosition.position = new Vector3(Random.Range(positionMinRespawn, positionMaxRespawn), 50, Random.Range(positionMinRespawn, positionMaxRespawn));
		pause = false;
		nbEnnemi++;

		if (niveaux >= 10)
		{
			ennemiEpéïste.SetActive(true);
			ennemiEpéïste.GetComponent<Transform>().position = new Vector3(Random.Range(positionMinRespawn, positionMaxRespawn), 50, Random.Range(positionMinRespawn, positionMaxRespawn));
			ennemiEpéïste.GetComponent<duplication>().chrono = z;
			ennemiEpéïste.GetComponent<duplication>().stop = false;

			if (niveaux > 10)
			{
				nbEnnemi++;
			}
		}

		niveaux++;
		duplication.chrono = z;
		duplication.stop = false;
		missileTiré.dupliquer = false;
	}

	public void Pause()
	{
		if (!Cursor.visible)
			Cursor.visible = true;

		playAgainButton.SetActive(true);
		image.SetActive(true);
		pause = true;
		pauseMenu.SetActive(true);
	}

	public void Marche()
	{
		playAgainButton.SetActive(false);
		image.SetActive(false);
		pause = false;
		pauseMenu.SetActive(false);
	}
}