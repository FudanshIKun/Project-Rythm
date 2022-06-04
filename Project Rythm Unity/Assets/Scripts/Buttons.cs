using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public float targetScaleZoomIn;
    RectTransform Pos_Size;
    void Start()
    {
        Pos_Size = GetComponent<RectTransform>();
    }

    
    void Update()
    {
        
    }

    public void MouseEnter(){
        Pos_Size.localScale = new Vector3(targetScaleZoomIn, targetScaleZoomIn, targetScaleZoomIn);
    }

    public void MouseExit(){
        Pos_Size.localScale = new Vector3(1f, 1f, 1f);
    }
}
