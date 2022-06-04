using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    //Game Component
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] Note note;
    [SerializeField] GameObject noteRail;
    [SerializeField] TextMeshProUGUI comboText;
    public Animator chaAnimator;
    public Animator effectanimator;
    public Transform hitPoint;
    public Transform spawnPointBlue;
    public Transform spawnPointRed;
    [Header("Song Settings")]
    public float showTime = 2.5f;
    public float songBPM = 146;
    public float timePerBeat;
    [Header("Game Settings")]
    public float perfectInterval = 0.075f;
    public float goodInterval = 0.2f;
    public float missTime = 0.1f;
    //Game System
    public float gameElasTime = 0;
    public int curentNoteNumber = 0;
    //Input System
    public int curInput = 0;
    public bool haveInput = false;
    //Scoring System
    public float score = 0;
    public int combo = 0;
    //Animation System
    [SerializeField] float hitPointMaxScale = 1.2f;
    [SerializeField] float hitPointGrowSpeed = 5f;
    public float noteFadeinTime = 0.75f;
    float curhitPointScale = 1f;
    void Start()
    {
        timePerBeat = 60f / songBPM;
        StartCoroutine(gameEvent());
    }
    void Update()
    {
        inputUpdate();
        //Animation
        hitPointgrow();
    }
    IEnumerator gameEvent()
    {
        gameElasTime = -showTime;
        while (gameElasTime < 0)
        {
            gameElasTime += Time.deltaTime;
            if (gameElasTime > (curentNoteNumber * timePerBeat) - showTime)
            {
                createNotes();
            }
            yield return null;
        }
        //Start Song
        musicPlayer.Play();
        gameElasTime = musicPlayer.time;
        while (true)
        {
            gameElasTime += Time.deltaTime;
            if (gameElasTime > (curentNoteNumber * timePerBeat) - showTime)
            {
                createNotes();
            }
            if (curentNoteNumber % 4 == 0)
            {
                //gameElasTime = musicPlayer.time;
                //Debug.Log("Reset Time");
            }
            yield return null;
        }
    }
    void createNotes()
    {
        Note instNote = Instantiate(note, noteRail.transform);
        instNote.stageM = this;
        instNote.colorCode = 0;
        instNote.noteNumber = curentNoteNumber;
        instNote.hitTimeStamp = timePerBeat * curentNoteNumber;
        curentNoteNumber += 1;
    }
    void inputUpdate()
    {
        //Check animationInput
        haveInput = false;
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) )
        {
            haveInput = true;
        }
        //Check gameInput
        curInput = 0;
        if (Input.GetMouseButton(0) & Input.GetMouseButton(1))
        {
            curInput = 3;
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            curInput = 1;
        }
        if (Input.GetMouseButtonDown(1))
        {
            curInput = 2;
        }
    }
    #region scoring System
    public void note_hit(int hitID)
    {
        combo += 1;
        comboText.text = "COMBO " + combo.ToString();
        if(hitID == 0)
        {
            chaAnimator.Play("Attacked");
            effectanimator.Play("Slash");

        }
        if (hitID == 1)
        {
            chaAnimator.Play("Attack1");
            effectanimator.Play("Shield");
        }
        if (hitID == 2)
        {
            chaAnimator.Play("Attack1");
            effectanimator.Play("Slash");
        }

    }
    public void note_miss()
    {
        combo = 0;
        comboText.text = "";
    }
    #endregion 
    #region Animaiton
    void hitPointgrow()
    {
        float dir = 1;
        if (!haveInput)
        {
            dir = -1;
        }
        curhitPointScale = Mathf.Clamp(curhitPointScale + hitPointGrowSpeed * Time.deltaTime * dir, 1, hitPointMaxScale);
        hitPoint.transform.localScale = new Vector2(curhitPointScale, curhitPointScale);
    }
    #endregion
}
