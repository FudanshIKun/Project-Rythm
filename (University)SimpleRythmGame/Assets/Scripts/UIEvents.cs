using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    [Header("Transition Setting")]
    public GameObject transitionCanvas;
    public Image TransitionPanel;
    [SerializeField] Color startTransition;
    [SerializeField] Color endTransition;
    float elapsedTime;

    [Header("Canvas Setting")]
    public GameObject firstChapterSettingCanvas;
    public GameObject firstChapterCanvas;
    public GameObject mainCanvas;

    public void PlayButton_Click(){
        mainCanvas.SetActive(false);
        firstChapterCanvas.SetActive(true);
    }
    
    public void ExitButton_Click(){
        Application.Quit();
    }

    public void BackButton_Click(){
        mainCanvas.SetActive(true);
        firstChapterCanvas.SetActive(false);        
    }

    public void EnteringFirstChapter(){
        TransitionScene("GameStage");
        
    }

    async void TransitionScene(string sceneName){
        transitionCanvas.SetActive(true);
        while(TransitionPanel.color != endTransition){
            elapsedTime += Time.deltaTime;
            TransitionPanel.color = Color.Lerp(startTransition, endTransition, elapsedTime/1.5f);
            await Task.Yield();
        }
        await Task.Delay(4000);
        SceneManager.LoadScene(sceneName);
    }

    void Start()
    {
        if (mainCanvas != null){ mainCanvas.SetActive(true);}

        if (firstChapterCanvas != null){firstChapterCanvas.SetActive(false);}
        
        if (firstChapterSettingCanvas != null){firstChapterSettingCanvas.SetActive(false);}

        if (TransitionPanel != null){
            TransitionPanel.color = startTransition;
            TransitionPanel.raycastTarget = false;
            transitionCanvas.SetActive(false);
        }
        
    }

    
    void Update()
    {
        
    }
}
