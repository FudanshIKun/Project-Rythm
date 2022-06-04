using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public enum State{
        MainMenu,
        SelectingChapter,
        Setting,
        InGame
    };
    public State state;

    public float targetScaleZoomIn;
    RectTransform Pos_Size;
    void Start()
    {
        Pos_Size = GetComponent<RectTransform>();
    }

    
    void Update()
    {
        if (GameManager.singleton.GamePlayState.ToString() != state.ToString()){
            Pos_Size.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void MouseEnter(){
        Pos_Size.localScale = new Vector3(targetScaleZoomIn, targetScaleZoomIn, targetScaleZoomIn);
    }

    public void MouseExit(){
        Pos_Size.localScale = new Vector3(1f, 1f, 1f);
    }
}
