using UnityEngine;
using UnityEngine.AI;

public class BakingNavMesh : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface[] navMeshSurface;

    // Start is called before the first frame update
    void Start()
    {
        Remove();
    }

    public void Remove()
    {
        for (int i = 0; i < navMeshSurface.Length; i++)
        {
            navMeshSurface[i].BuildNavMesh();
        }
    }
}
