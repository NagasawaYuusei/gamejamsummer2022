using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possessed: MonoBehaviour
{
    [SerializeField]Possession possessionScript;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            possessionScript.enabled = true;
        }
    }
}
