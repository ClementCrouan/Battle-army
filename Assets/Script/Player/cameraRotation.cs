using UnityEngine;
using UnityEngine.UI;

public class cameraRotation : MonoBehaviour
{
	[Header("Rotation")]
	public float rotationX;
	public float rotationY;
	public float sensitivityY = 5f;
	public float sensitivityX = 5f;
	public Rigidbody rb;

	[Header("UI")]
	public Slider sliderX;
	public Slider sliderY;
	public Text textSensitivityX;
	public Text textSensitivityY;

	private Vector3 velocity;
	private Vector3 rotation;
	private float rotationXPlayerImage;
	const int z = 0;

	// Update is called once per frame
	void Update()
	{
		sensitivityX = sliderX.value;
		sensitivityY = sliderY.value;
		textSensitivityX.text = sensitivityX + "";
		textSensitivityY.text = sensitivityY + "";
		rotationXPlayerImage = transform.localRotation.x;

		if (!isMouseOffScreen())
		{
			rotationX = Input.GetAxisRaw("Mouse Y") * sensitivityX;
			rotationY = Input.GetAxisRaw("Mouse X") * sensitivityY;

			if (rotationXPlayerImage > 0.7071068f)
			{
				transform.Rotate(-10, z, z);
			}
			else if(rotationXPlayerImage < -0.7071068f)
			{
				transform.Rotate(10, z, z);
			}
			rotation = new Vector3(z, rotationY, z);
			transform.Rotate(-rotationX, z, z);
		}
	}
	private bool isMouseOffScreen()
	{
		if (Input.mousePosition.y <= 2)
			return true;
		return false;
	}

	public void Move(Vector3 _velocity)
	{
		velocity = _velocity;
	}

	private void FixedUpdate()
	{
		PerformMovement();
		PerformRotation();
	}

	private void PerformMovement()
	{
		if (velocity != Vector3.zero)
		{
			rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
		}
	}

	private void PerformRotation()
    {
		rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
    }
}
