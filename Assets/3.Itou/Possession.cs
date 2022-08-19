using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possession : MonoBehaviour
{
    [SerializeField][Tooltip("プレイヤーが憑依できる物のレイヤー")] private LayerMask _canHitMask;
    [SerializeField][Tooltip("Rayが当たったらこの中に入れる")] GameObject _hited;
    [SerializeField][Tooltip("Rayの距離")] private float _rayDistance;
    [SerializeField][Tooltip("Rayが当たったものに変わってほしい色")] Color _color = new Color(210, 210, 210);
    MeshRenderer _mr;
    RaycastHit hit;

    public GameObject Hited => _hited;

    void Update()
    {
        Vector3 dir = Camera.main.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(gameObject.transform.position + new Vector3(0,0.5f,0), dir * _rayDistance, Color.blue, 0.1f);
        if (Physics.Raycast(gameObject.transform.position + new Vector3(0, 0.5f, 0), dir, out hit, _rayDistance, _canHitMask))
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
