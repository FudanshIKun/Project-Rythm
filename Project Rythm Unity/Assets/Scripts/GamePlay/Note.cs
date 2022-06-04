using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    //Game Component
    public Image image;
    //Graphic Properties
    [SerializeField] Color redColor, blueColor, purpleColor;
    Color noteColor;
    //Note Properties
    public int colorCode = 0;
    public float hitTimeStamp = 0;
    public int noteNumber = 0;
    public float tShow = 0;
    public float timeTohit = 0;
    //Component
    public StageManager stageM;
    Transform spawnPoint;
    void Start()
    {
        set_color();
        set_spawnPoint();
    }
    void Update()
    {
        railPositionUpdate();
        noteStateUpdate();
    }
    void set_color()
    {
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
    void set_spawnPoint()
    {
        //Set Spawn Point
        spawnPoint = stageM.spawnPointRed;
        if (colorCode == 1)
        {
            spawnPoint = stageM.spawnPointBlue;
        }
        if (colorCode == 2)
        {
            int random = Random.Range(0, 2);
            spawnPoint = random == 0 ? stageM.spawnPointBlue : stageM.spawnPointRed;
        }
        transform.position = spawnPoint.position;
    }
    void railPositionUpdate()
    {
        timeTohit = (hitTimeStamp - stageM.gameElasTime);

        //fade in animation
        float fadeT = Mathf.Clamp(1 - (timeTohit / stageM.showTime - stageM.noteFadeinTime) / 0.1f, 0f, 1f);
        noteColor.a = fadeT;
        image.color = noteColor;

        //Miss
        if (-timeTohit > stageM.missTime)
        {
            stageM.note_miss();
            Destroy(gameObject);
        }
        transform.position = learpWithOutClamp(stageM.hitPoint.position, spawnPoint.position, timeTohit / stageM.showTime);

    }
    Vector2 learpWithOutClamp(Vector2 a, Vector2 b, float t)
    {
        return a + (b - a) * t;
    }
    void noteStateUpdate()
    {
        if (stageM.curInput == colorCode + 1 & timeTohit < stageM.goodInterval)
        {
            stageM.note_hit(colorCode);
            Destroy(gameObject);
        }
    }
}
