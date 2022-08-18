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
    [SerializeField]Possessed possessedScript;
    bool isPossession;
    void Update()
    {
        Debug.DrawRay(gameObject.transform.position, Vector3.forward * _rayDistance, Color.blue, 0.1f);
        if (Physics.Raycast(gameObject.transform.position, Vector3.forward, out hit, _rayDistance, _canHitMask))
        {
            _hited = hit.collider.gameObject;
            possessedScript = _hited.GetComponent<Possessed>();
            ColorChange(_hited, _color);
            if ((Input.GetKeyDown(KeyCode.Z)) && isPossession == false)
            {
                possessedScript.enabled = true;
                this.gameObject.transform.position = _hited.transform.position;
                this.transform.rotation = _hited.transform.rotation;
            }
            else if ((Input.GetKeyDown(KeyCode.Z)) && isPossession == true)
            {
                possessedScript.enabled = false;
            }
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
