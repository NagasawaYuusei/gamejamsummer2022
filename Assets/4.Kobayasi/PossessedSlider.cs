using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessedSlider : MonoBehaviour
{
    public Canvas canvas;
    void Update()
    {
        //EnemyCanvasをMain Cameraに向かせる
        canvas.transform.rotation =
            Camera.main.transform.rotation;
    }
}


