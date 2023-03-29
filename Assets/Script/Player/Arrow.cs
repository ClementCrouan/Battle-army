using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rb;
    bool hasHit;
	MissileDirection missileDirection;

	// Start is called before the first frame update
	void Start()
    {
        rb = GetComponent<Rigidbody>();
		missileDirection = GameObject.Find("Shot Point").GetComponent<MissileDirection>();
	}

    // Update is called once per frame
    void Update()
    {
		if (transform.position.y < -50)
		{
			missileDirection.tiré = false;
			Destroy(gameObject);
		}

		if (!hasHit)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.left);
        }
	}

	private void OnCollisionEnter(Collision collision)
	{
		hasHit = true;
		rb.velocity = Vector3.zero;
		rb.isKinematic = true;
		missileDirection.tiré = false;

		if (collision.gameObject.tag == "Ennemi" | collision.gameObject.tag == "Clone")
		{
			missileDirection.score++;
			missileDirection.nbEnnemi--;

			if (collision.gameObject.name != "Ennemi")
			{
				Destroy(collision.gameObject, 0.05f);
			}

			if (collision.gameObject.name == "Ennemi")
			{
				collision.gameObject.SetActive(false);
			}

			if (missileDirection.nbEnnemi == 0)
			{
				missileDirection.respawn();
			}
		}

		Destroy(gameObject);
	}
}
