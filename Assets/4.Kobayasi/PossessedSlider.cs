using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PossessedSlider : MonoBehaviour
{
    public Canvas canvas;
    void Update()
    {
        //EnemyCanvas‚ðMain Camera‚ÉŒü‚©‚¹‚é
        canvas.transform.rotation =
            Camera.main.transform.rotation;
    }
}


