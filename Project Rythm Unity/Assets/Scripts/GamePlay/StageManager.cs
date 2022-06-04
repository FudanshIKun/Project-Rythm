using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    //Game Component
    [SerializeField] AudioSource musicPlayer;
    [SerializeField] Note note;
    [SerializeField] GameObject noteRail;
    public Transform hitPoint;
    public Transform spawnPoint;
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
    void Start()
    {
        timePerBeat = 60f / songBPM;
        StartCoroutine(gameEvent());
    }
    void Update()
    {
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
                Debug.Log("Reset Time");
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
}
