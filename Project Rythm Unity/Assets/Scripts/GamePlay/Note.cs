using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    //Graphic Properties
    [SerializeField] Color redColor, blueColor, purpleColor;
    Image image;
    Color noteColor;
    //Note Properties
    public int colorCode = 0;
    public float hitTimeStamp = 0;
    public int noteNumber = 0;
    public float tShow = 0;
    public float timeTohit = 0;
    //Component
    public StageManager stageM; 
    void Start()
    {
        image = GetComponent<Image>();
        //Random Color
        colorCode = Random.Range(0, 3);
        if (colorCode == 0)
        {
            noteColor = redColor;
        }
        else if (colorCode == 1)
        {
            noteColor = blueColor;
        }
        else if (colorCode == 2)
        {
            noteColor = purpleColor;
        }
        image.color = noteColor;
    }
    void Update()
    {
        railPositionUpdate();
        noteStateUpdate();
    }
    void railPositionUpdate()
    {
        timeTohit = (hitTimeStamp - stageM.gameElasTime);

        //fade in animation
        float fadeT = Mathf.Clamp(1 - (timeTohit / stageM.showTime - 0.9f) / 0.1f, 0f, 1f);
        noteColor.a = fadeT;
        image.color = noteColor;

        transform.position = learpWithOutClamp(stageM.hitPoint.position, stageM.spawnPoint.position, timeTohit / stageM.showTime);
    }
    Vector2 learpWithOutClamp(Vector2 a, Vector2 b, float t)
    {
        return a + (b - a) * t;
    }
    void noteStateUpdate()
    {
        if (-timeTohit > stageM.missTime)
        {
            Destroy(gameObject);
        }
        if (Input.GetMouseButtonDown(colorCode) & timeTohit < stageM.goodInterval)
        {
            Destroy(gameObject);
        }
        //Double Input Checker
        if (Input.GetMouseButton(0) & Input.GetMouseButton(1) & timeTohit < stageM.goodInterval & colorCode == 2)
        {
            Destroy(gameObject);
        }
    }
}
