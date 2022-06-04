using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("SoundStage Management")]
    public AudioSource mainSpeaker;
    public AudioClip mainBG;
    public float delayTime;

    [Header("UI Management")]
    public static GameManager singleton;
    public UIEvents mainUI;

    public enum State{
        MainMenu,
        SelectingChapter,
        InGame
    }
    [HideInInspector]
    public State GamePlayState;

    void SoundStage(){
        mainSpeaker.clip = mainBG;
        mainSpeaker.PlayDelayed(delayTime);
    }

    void Start()
    {
        SoundStage();
    }

    
    void Update()
    {
        
    }
}
