using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    //public MissileDirection missileDirection;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
