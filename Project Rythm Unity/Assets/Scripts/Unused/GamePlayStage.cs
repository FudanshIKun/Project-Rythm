using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GamePlayStage : MonoBehaviour
{
    [Header("Note Management")]
    public int BeatPerMinute;
    [SerializeField] GameObject redNote;
    [SerializeField] GameObject blueNote;
    public Transform spawnPoint;
    public Transform targetPoint;

    [HideInInspector]
    #region Note&Time

    public float timeStamp;
    public static List<Note> notesInScene = new List<Note>();

    #endregion
    [HideInInspector]
    #region GamePlay

    public int combo;

    #endregion


    public int BPMcalculate(int BPM){
        BPM = 60/BPM;
        return BPM;
    }

    void RandomGenerateNote(){ // Random NoteColor and Instantiate // Untested
        int rnd = Random.Range(0,2);
        if (rnd == 0){
            Instantiate(redNote, spawnPoint, true);
            GameManager.singleton.nextTimeStamp = timeStamp;
        }else{
            Instantiate(blueNote, spawnPoint, true);
            GameManager.singleton.nextTimeStamp = timeStamp;
        }
    }

    public async void SpawnNotes(float songDuration, float frequency){ // Untested
        var endTime = Time.time + songDuration;
        float delayTime = frequency * 1000f;
        while (Time.time <= songDuration){
            RandomGenerateNote();
            timeStamp = timeStamp + BPMcalculate(146); // Plus TimeStamp
            Debug.Log("TimeStamp : " + timeStamp);
            await Task.Delay((int)delayTime);
        }
    }

    void Start()
    {
        
    }

    
    void Update()
    {
    }
}
