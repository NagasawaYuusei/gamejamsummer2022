using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessedSlider : MonoBehaviour
{
    public Canvas canvas;
    void Update()
    {
        //EnemyCanvas��Main Camera�Ɍ�������
        canvas.transform.rotation =
            Camera.main.transform.rotation;
    }
}


