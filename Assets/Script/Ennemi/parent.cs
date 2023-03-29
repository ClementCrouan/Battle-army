using UnityEngine;

public class parent : MonoBehaviour
{
	private float adjacent;
	private float difX;
	private float difY;
	private float difZ;
	private float opposé;
	private float rotationArc;
	const int z = 0;
	
	[Header("Transform")]
	public Transform arcPosition;
	public Transform ennemiPosition;
	public Transform playerPosition;

    // Update is called once per frame
    void Update()
    {
		difX =  playerPosition.position.x - ennemiPosition.position.x;
		difY =  playerPosition.position.y - ennemiPosition.position.y;
		difZ =  playerPosition.position.z - ennemiPosition.position.z;
		
		adjacent = difX + difZ;
		opposé = difY;
		
		if(adjacent < z)
		{
			adjacent *= -1f;
		}

		if(adjacent == z)
        {
			adjacent = 1;
        }
		
		rotationArc = Mathf.Atan(opposé / adjacent);
		rotationArc = -((rotationArc * 180f) / Mathf.PI);
		arcPosition.localRotation = Quaternion.Euler(rotationArc,z,z);
    }
	
	public void enfant()
	{
		this.transform.GetChild(1).parent = null;
	}
}
