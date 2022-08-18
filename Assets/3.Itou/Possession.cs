using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{
    [SerializeField][Tooltip("当たり判定")] private LayerMask canHitMask;
    [SerializeField][Tooltip("目線当たったらものを入れる")]GameObject hited;
    [SerializeField][Tooltip("目線の距離")]private float _rayDistance;
   
    RaycastHit hit;

    void Update()
    {
        Debug.DrawRay(gameObject.transform.position, Vector3.forward *_rayDistance, Color.blue, 0.1f);
        if (Physics.Raycast(gameObject.transform.position, Vector3.forward, out hit, _rayDistance,canHitMask))
        {
            hited = hit.collider.gameObject;
        }
        else 
        {
            hited = null;
        }
    }
}
