using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public void PlayerInput(){ // Tested
        bool redColor = GamePlayStage.notesInScene[0].Color == Note.noteColor.Red;
        bool blueColor = GamePlayStage.notesInScene[0].Color == Note.noteColor.Blue;
        if (Input.GetMouseButtonDown(0) && redColor){
            Debug.Log("RedColor Interacted");
        }else if (Input.GetMouseButtonDown(1) && blueColor){
            Debug.Log("BlueColor Interacted");
        }
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        PlayerInput();
    }
}
