using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionPlayer : MonoBehaviour
{
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Player")
		{
			SceneManager.LoadScene("Play Again");
		}
	}
}
