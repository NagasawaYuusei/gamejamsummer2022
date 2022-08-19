using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    CameraChange _cc;

    void Start()
    {
        _cc = GetComponent<CameraChange>();
    }
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && _cc.IsTPS)
        {
            Debug.Log("Fire");
            //アニメーション再生処理
            _cc.ChangeFPS();
        }
    }
}
