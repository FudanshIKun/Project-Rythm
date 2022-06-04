using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public GameObject FirstChapterSettingCanvas;
    public GameObject FirstChapterCanvas;
    public GameObject SettingCanvas;
    public GameObject ChapterCanvas;
    public GameObject mainCanvas;

    public void PlayButton_Click(){
        mainCanvas.SetActive(false);
        ChapterCanvas.SetActive(true);
        GameManager.singleton.GamePlayState = GameManager.State.SelectingChapter;
    }
    
    public void ExitButton_Click(){
        Application.Quit();
    }

    public void BackButton_Click(){
        mainCanvas.SetActive(true);
        ChapterCanvas.SetActive(false);
        GameManager.singleton.GamePlayState = GameManager.State.MainMenu;
    }

    public void EnteringFirstChapter(){
        ChapterCanvas.SetActive(false);
        FirstChapterCanvas.SetActive(true);
    }

    public void FirstChapterBack_Click(){
        ChapterCanvas.SetActive(true);
        FirstChapterCanvas.SetActive(false);
        GameManager.singleton.GamePlayState = GameManager.State.SelectingChapter;
    }

    public void SettingBack_Click(){
        mainCanvas.SetActive(true);
        SettingCanvas.SetActive(false);
        GameManager.singleton.GamePlayState = GameManager.State.MainMenu;
    }

    public void SettingButton_Click(){
        mainCanvas.SetActive(false);
        SettingCanvas.SetActive(true);
        GameManager.singleton.GamePlayState = GameManager.State.Setting;
    }

    void Start()
    {
        GameManager.singleton.GamePlayState = GameManager.State.MainMenu;
        if (mainCanvas == null){return;}
        mainCanvas.SetActive(true);

        if (ChapterCanvas == null){return;}
        ChapterCanvas.SetActive(false);

        if (SettingCanvas == null){return;}
        SettingCanvas.SetActive(false);

        if (FirstChapterCanvas == null){return;}
        FirstChapterCanvas.SetActive(false);

        if (FirstChapterSettingCanvas == null){return;}
        FirstChapterSettingCanvas.SetActive(false);
    }

    
    void Update()
    {
        
    }
}
