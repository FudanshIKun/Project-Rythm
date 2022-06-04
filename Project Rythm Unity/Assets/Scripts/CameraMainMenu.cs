using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMainMenu : MonoBehaviour
{
    // Control camera to choose the chapter to play in 3d view
    [SerializeField] LayerMask mask;
    enum chapters{
        FirstPoint
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        #region MouseSelection
        if(GameManager.singleton.GamePlayState == GameManager.State.SelectingChapter){
            if(Input.GetMouseButtonDown(0)){
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask)){
                    if(hit.collider.name == chapters.FirstPoint.ToString()){
                        GameManager.singleton.mainUI.EnteringFirstChapter();
                        GameManager.singleton.GamePlayState = GameManager.State.FirstChapter;
                        
                    }
                }
            }
        }
        #endregion

    }



    void CameraMovement(){

    }

    void CameraZoomIn(){

    }

    public void CameraZoomOut(){

    }
}
