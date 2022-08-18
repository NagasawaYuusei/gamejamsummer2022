using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{
    [SerializeField][Tooltip("�v���C���[���߈˂ł��镨�̃��C���[")] private LayerMask canHitMask;
    [SerializeField][Tooltip("Ray�����������炱�̒��ɓ����")] GameObject hited;
    [SerializeField][Tooltip("Ray�̋���")] private float _rayDistance;

    RaycastHit hit;

    void Update()
    {
        Debug.DrawRay(gameObject.transform.position, Vector3.forward * _rayDistance, Color.blue, 0.1f);
        if (Physics.Raycast(gameObject.transform.position, Vector3.forward, out hit, _rayDistance, canHitMask))
        {
            hited = hit.collider.gameObject;
        }
        else
        {
            hited = null;
        }
    }
}
