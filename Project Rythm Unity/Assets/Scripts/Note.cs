using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    AudioSource sources;

    public enum noteColor{
        Red,
        Blue
    }
    public noteColor Color;
    
    float myTimeStamp;

    public void NoteInteraction(){ // Seperate each perfect, bad, neutral, good, or missed, Check with PlayBackTime & myTimeStamp

    }

    void NoteHitted(){
        GamePlayStage.notesInScene.RemoveAt(0);
        GameManager.singleton.mainGamePlay.combo += 1;
        Destroy(gameObject);
    }

    void NoteMissed(){
        GamePlayStage.notesInScene.RemoveAt(0);
        GameManager.singleton.mainGamePlay.combo = 0;
        Destroy(gameObject);
    }

    void NoteMovement(){ // Bug
        Vector3 origin = GameManager.singleton.mainGamePlay.spawnPoint.transform.position;
        Vector3 target = GameManager.singleton.mainGamePlay.targetPoint.transform.position;
        transform.position = Vector3.Lerp(origin, target, Vector3.Distance(origin, target));
    }

    void Start()
    {
        sources = SoundManager.singleton.mainSpeaker;
        myTimeStamp = GameManager.singleton.nextTimeStamp;
        GamePlayStage.notesInScene.Add(GetComponent<Note>());
    }

    
    void Update()
    {
        NoteMovement();
    }
}
