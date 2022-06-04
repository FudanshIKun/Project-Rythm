using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI Management")]
    public static GameManager singleton;

    [HideInInspector]
    public CameraMainMenu mainCam;
    public UIEvents mainUI;
    public GamePlayStage mainGamePlay;

    #region Gameplay Management

    [HideInInspector]
    public float nextTimeStamp; // Untested
    [HideInInspector]
    public Note nextNote; // Untested

    #endregion

    public enum State{
        MainMenu,
        SelectingChapter,
        FirstChapter,
        Setting,
        InGame
    }
    [HideInInspector]
    public State GamePlayState;

    private void Awake()
    {
        #region singleton

        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);

        }
        #endregion
        
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
