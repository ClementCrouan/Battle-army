using UnityEngine;

public class CameraChargement : MonoBehaviour
{
    public GameObject cameraChargement1;
    GameObject cameraChargement2;

    // Start is called before the first frame update
    void Start()
    {
        cameraChargement2 = GameObject.Find("Camera Chargement");
        Destroy(cameraChargement2);
    }

    public void CameraChargementAppariton()
    {
        cameraChargement1.SetActive(true);
        //chargement.enabled = true;
    }
}
