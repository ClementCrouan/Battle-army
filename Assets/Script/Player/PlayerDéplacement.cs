using UnityEngine;

public class PlayerDéplacement : MonoBehaviour
{
	[Header("Audio")]
	public AudioSource aS;
	public AudioClip bruitPas;
	public AudioSource bS;
	public AudioClip courir;

	[Header("Déplacement")]
	public float speed;
	public float jumpSpeed;
	public float vitesseAvancer;
	public float vitesseDéplacementLattéral;
	public float vitesseReculer;
	public float vitesseSprint;
	public float vitesseWater;
	
	[Header("GameObject")]
	public GameObject caméra1;
	public GameObject caméra2;

	[Header("Info")]
	public int stopSprint = 100;
	public bool isGrounded = false;
	public bool onWater = false;

	[Header("Script")]
	public cameraRotation cameraRotation;
	public MissileDirection missileDirection;

	private int continueSprint;
	private bool shift;
	private bool run;
	private bool switche;
	
	// Start is called before the first frame update
    void Start()
    {
		caméra2.SetActive(false);
		aS.clip = bruitPas;
		bS.clip = courir;
    }

    // Update is called once per frame
    void Update()
    {
		if (!missileDirection.pause)
		{
			if (Input.GetKey(KeyCode.RightShift) | Input.GetKey(KeyCode.LeftShift))
			{
				shift = true;
			}
			else
			{
				shift = false;
				run = false;
			}

			if (Input.GetKeyDown(KeyCode.Mouse1))
			{
				if (switche == false)
				{
					caméra1.SetActive(false);
					caméra2.SetActive(true);
					switche = true;
				}
				else
				{
					caméra2.SetActive(false);
					caméra1.SetActive(true);
					switche = false;
				}
			}

			float xMov = Input.GetAxisRaw("Horizontal");
			float zMovAvancer = Input.GetAxisRaw("Vertical Avancer");
			float zMovReculer = Input.GetAxisRaw("Vertical Reculer");
			float yMov = Input.GetAxisRaw("Jump");
			
			Vector3 moveHorizontal = transform.right * xMov * vitesseDéplacementLattéral;
			Vector3 moveJump = transform.up * yMov * jumpSpeed;
			if (!isGrounded)
				moveJump = Vector3.zero;
			Vector3 moveVertical = transform.forward * 0;
			if (zMovAvancer != 0 | zMovReculer == 0)
			{
				moveVertical = transform.forward * zMovAvancer * vitesseAvancer;
			}
			else if (zMovAvancer == 0 | zMovReculer != 0)
			{
				moveVertical = transform.forward * zMovReculer * vitesseReculer;
			}
			else if (zMovAvancer != 0 | zMovReculer == 0 && shift)
			{
				moveVertical = transform.forward * zMovAvancer * vitesseSprint;
			}

			Vector3 velocity = (moveHorizontal + moveVertical + moveJump).normalized * speed;

			if (onWater)
            {
				velocity *= vitesseWater;
            }

			if (isGrounded && velocity.x != 0 || velocity.z != 0)
			{
				if (!aS.isPlaying)
					aS.Play();
			}
			if (isGrounded && zMovAvancer != 0 && shift)
			{
				stopSprint--;
				run = true;
				if (!bS.isPlaying)
					bS.Play();
			}

			cameraRotation.Move(velocity);
			
			if (stopSprint < 100 && !run)
			{
				continueSprint++;
				if (continueSprint >= 5)
				{
					continueSprint = 0;
					stopSprint++;
				}
			}
		}
    }

	void OnCollisionEnter(Collision col)
    {
		if (col.gameObject.tag == "Map")
        {
			isGrounded = true;
        }

		if (col.gameObject.tag == "Water")
		{
			onWater = true;
		}
	}

	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "Map")
		{
			isGrounded = false;
		}

		if (col.gameObject.tag == "Water")
		{
			onWater = false;
		}
	}
}