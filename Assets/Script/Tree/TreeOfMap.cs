using UnityEngine;

public class TreeOfMap : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Map")
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}