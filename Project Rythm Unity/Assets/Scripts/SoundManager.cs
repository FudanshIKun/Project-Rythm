using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager singleton;
    [SerializeField] Slider volumeSlider; // Volume Setting
    public AudioSource mainSpeaker; // main AudioSource
    public AudioClip BG;
    [HideInInspector]
    public GamePlayStage gameplay;

    [Header("Sound Staging")]
    [SerializeField] float startDelayed;

     void soundStageLine(){
        if (SceneManager.GetActiveScene().name == "MainMenu"){
            MainMenuStage();
            
        }else if(SceneManager.GetActiveScene().name == "FirstChapter"){
            FirstChapterStage();
        }
    }

    void MainMenuStage(){
        mainSpeaker.clip = BG;
        mainSpeaker.loop = true;
        mainSpeaker.PlayDelayed(startDelayed);
    }

    async void FirstChapterStage(){
        if (BG == null){return;}
        mainSpeaker.clip = BG;
        mainSpeaker.loop = false;
        float startTime = startDelayed * 1000f;
        await Task.Delay((int)startTime); // wait for delay
        gameplay.SpawnNotes(BG.length, gameplay.BPMcalculate(146)); // spawn before musicPlaying 2sec and repeat each 60/bpm
        await Task.Delay(2000); // wait for 2 sec
        mainSpeaker.Play();
    }

    private void Awake() {
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
        if (volumeSlider != null){
            if(!PlayerPrefs.HasKey("musicVolume")){
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }else{
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        }
        soundStageLine();

        if (GameManager.singleton.mainGamePlay != null){
            gameplay = GetComponent<GamePlayStage>();
        }
    }
    
    public void ChangeVolume(){
        if (volumeSlider == null){return;}
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load(){
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume"); // Load value from PlayerPrefs
    }

    private void Save(){
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value); // Save volumeslider's value as keyname "musicVolume"
    }

    void Update() {
        ChangeVolume();
        //Debug.Log("Playback : " + mainSpeaker.time);
    }
}
