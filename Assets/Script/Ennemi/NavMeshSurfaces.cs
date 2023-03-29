using UnityEngine;
using UnityEngine.AI;

public class NavMeshSurfaces : MonoBehaviour
{
    [Header("Nav Mesh Agent")]
    public NavMeshAgent target;

    [Header("Transform")]
    public Transform playerPosition;

    float time;

    void Start()
    {
        target = this.GetComponent<NavMeshAgent>();
        if (tag == ("Ennemi"))
        {
            target.enabled = false;
        }
    }

    void Update()
    {
        if (time != 10 && tag == ("Ennemi"))
        {
            time += Time.deltaTime;
        }
        if (time >= 10 && tag == ("Ennemi"))
        {
            target.enabled = true;
            target.SetDestination(playerPosition.position);
        }

        if (tag != ("Ennemi"))
        {
            target.SetDestination(playerPosition.position);
        }
    }        
}