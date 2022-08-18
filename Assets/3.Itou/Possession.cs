using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{
    [SerializeField][Tooltip("�����蔻��")] private LayerMask canHitMask;
    [SerializeField][Tooltip("�ڐ�������������̂�����")]GameObject hited;
    [SerializeField][Tooltip("�ڐ��̋���")]private float _rayDistance;
   
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
