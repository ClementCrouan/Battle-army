using UnityEngine;

public class BarDeCourse : MonoBehaviour
{
	[Header("Rect Transform")]
	public RectTransform barreDeCourse;
    
	[Header("Script")]
	public PlayerDéplacement playerDéplacement;

    private float scale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scale = playerDéplacement.stopSprint / 100f;
        barreDeCourse.localScale = new Vector3(scale, 1, 1);
    }
}
