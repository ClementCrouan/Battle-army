using UnityEngine;

public class Bow : MonoBehaviour
{
    public float launchForce;
    public GameObject arrow;
    AudioSource aS;
    public AudioClip tireArc;
    Transform playerBattle;
    MissileDirection missileDirection;

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;
    Vector3 direction;

    private void Start()
    {
        playerBattle = GameObject.Find("Player Battle").transform;
        aS = GetComponent<AudioSource>();
        missileDirection = GetComponent<MissileDirection>();
        points = new GameObject[numberOfPoints];

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(playerBattle.rotation.x, playerBattle.rotation.y, playerBattle.rotation.z);

        if (Input.GetMouseButtonDown(0))
        {
            missileDirection.tiré = true;
            aS.PlayOneShot(tireArc);
            Shoot();
        }

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].transform.position = PointPosition(i * spaceBetweenPoints);
        }
    }
    
    void Shoot()
    {
        GameObject newArrow = Instantiate(arrow, transform.position, transform.rotation);
        newArrow.GetComponent<Rigidbody>().velocity = playerBattle.forward * launchForce;
    }

    Vector3 PointPosition(float t)
    {
        Vector3 position = (Vector3)transform.position + (direction.normalized * launchForce * t) + 0.5f * Physics.gravity;
        return position;
    }
}
