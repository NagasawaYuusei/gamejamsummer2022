using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChange : MonoBehaviour
{
    bool _isFPS = true;
    [SerializeField] CinemachineVirtualCamera _fpsCvc;
    CinemachineVirtualCamera _tpsCvc;
    Possession _possession;
    float _inputValueH;
    float _inputValueV;

    void Start()
    {
        _possession = GetComponent<Possession>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire2") && _isFPS && _possession.Hited)
        {
            _inputValueV = _fpsCvc.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value;
            _inputValueH = _fpsCvc.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value;
            _fpsCvc.LookAt = _possession.Hited.transform;
            _tpsCvc = _possession.Hited.transform.Find("CMObject").GetComponent<CinemachineVirtualCamera>();
            CameraChangeMethod(_fpsCvc, _tpsCvc);
            _isFPS = false;
        }
        else if(Input.GetButtonDown("Fire2") && !_isFPS)
        {
            _fpsCvc.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = _inputValueV;
            _fpsCvc.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value = _inputValueH;
            CameraChangeMethod(_tpsCvc, _fpsCvc);
            _tpsCvc = null;
            _isFPS = true;
        }
    }

    void CameraChangeMethod(CinemachineVirtualCamera oldCvc, CinemachineVirtualCamera currentCvc)
    {
        oldCvc.Priority = 0;
        currentCvc.Priority = 1;
    }
}
