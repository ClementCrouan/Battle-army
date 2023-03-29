using UnityEngine;
using UnityEngine.SceneManagement;

public class MissileTiré : MonoBehaviour
{
	[Header("Audio")]
	public AudioSource aS;
	public AudioClip tireArc;
	
	[Header("GameObject")]
	public GameObject arc;

	[Header("Info")]
	public bool dupliquer;
	public bool tiré = false;
	public float vitesse;
	public bool children;
	
	[Header("Rigidbody")]
	public Rigidbody rb;
	
	[Header("Script")]
	public duplication duplication;
	public MissileDirection missileDirection;
	public parent parent;
	
	[Header("Transform")]
	public Transform degreRotate;
	
	private float avancerX;
	private float avancerY;
	const int z = 0;
    
    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
	{
		if (!missileDirection.pause)
		{
			if (!tiré)
			{
				if (!dupliquer)
				{
					parent.enfant();
					aS.PlayOneShot(tireArc);
					children = true;
				}
			}

			do
			{
				tiré = true;
				rb.detectCollisions = true;
				rb.isKinematic = false;
				avancerX = Mathf.Sin(degreRotate.rotation.y / 57.3f) * vitesse;
				avancerY = Mathf.Cos(degreRotate.rotation.y / 57.3f) * vitesse;
				degreRotate.Translate(avancerX, z, avancerY);

				if (degreRotate.position.y < -50)
				{
					rb.isKinematic = true;
					tiré = false;

					if (duplication.stop)
					{
						dupliquer = true;
					}
					
					if (children)
					{
						this.transform.parent = arc.transform;
						children = false;
					}
					
					degreRotate.localPosition = new Vector3(0.00002f, 0.0008f, 0.0015f);
					degreRotate.localRotation = Quaternion.Euler(z, z, z);
				}
			}
			while (!tiré);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (tiré && !missileDirection.pause)
		{
			rb.isKinematic = true;
			tiré = false;

			if (duplication.stop)
			{
				dupliquer = true;
			}

			this.transform.parent = arc.transform;
			degreRotate.localPosition = new Vector3(0.00002f, 0.0008f, 0.0015f);
			degreRotate.localRotation = Quaternion.Euler(z, z, z);

			if (col.gameObject.tag == "Player")
			{
				SceneManager.LoadScene("Play Again");
			}
		}
	}	
}
