using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{ 
    public GameObject player;
    public GameObject lights;
    public GameObject missileFeu;
    
    public void Destroy()
    {
        Destroy(lights);
        Destroy(player);
        Destroy(missileFeu);
    }
}
