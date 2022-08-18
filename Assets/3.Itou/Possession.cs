using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{
    [SerializeField][Tooltip("�v���C���[���߈˂ł��镨�̃��C���[")] private LayerMask _canHitMask;
    [SerializeField][Tooltip("Ray�����������炱�̒��ɓ����")] GameObject _hited;
    [SerializeField][Tooltip("Ray�̋���")] private float _rayDistance;
    [SerializeField][Tooltip("Ray�������������̂ɕς���Ăق����F")] Color _color = new Color(210, 210, 210);
    MeshRenderer _mr;
    RaycastHit hit;

    void Update()
    {
        Debug.DrawRay(gameObject.transform.position, Vector3.forward * _rayDistance, Color.blue, 0.1f);
        if (Physics.Raycast(gameObject.transform.position, Vector3.forward, out hit, _rayDistance, _canHitMask))
        {
            _hited = hit.collider.gameObject;
            ColorChange(_hited, _color);
        }
        else if (_hited)
        {
            ColorChange(_hited, Color.white);
            _hited = null;
        }
    }


    public void ColorChange(GameObject gameObject, Color color)
    {
        gameObject.GetComponent<MeshRenderer>().material.color = color;
    }

}
