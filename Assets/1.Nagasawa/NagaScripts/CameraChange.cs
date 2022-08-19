using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChange : MonoBehaviour
{
    bool _isFPS = true;
    bool _isTPS = false;
    GameObject _go;
    [SerializeField] CinemachineVirtualCamera _fpsCvc;
    CinemachineVirtualCamera _tpsCvc;
    Possession _possession;
    Stamina _stamina;
    float _inputValueH;
    float _inputValueV;

    public bool IsTPS => _isTPS;

    void Start()
    {
        _possession = GetComponent<Possession>();
        _stamina = GetComponent<Stamina>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire2") && _isFPS && _possession.Hited && _stamina.IsOutOfStamina)
        {
            _inputValueV = _fpsCvc.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value;
            _inputValueH = _fpsCvc.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value;
            _fpsCvc.LookAt = _possession.Hited.transform;
            _tpsCvc = _possession.Hited.transform.Find("CMObject").GetComponent<CinemachineVirtualCamera>();
            CameraChangeMethod(_fpsCvc, _tpsCvc);
            _isFPS = false;
            _isTPS = true;
            _go = _possession.Hited;
        }
        else if(Input.GetButtonDown("Fire2") && !_isFPS)
        {
            ChangeFPS();
        }

        if(!_stamina.IsOutOfStamina && !_isFPS)
        {
            ChangeFPS();
        }
    }

    public void ChangeFPS()
    {
        _fpsCvc.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = _inputValueV;
        _fpsCvc.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value = _inputValueH;
        CameraChangeMethod(_tpsCvc, _fpsCvc);
        _go.GetComponent<PossessedSlider>().ActiveSwitch(true);
        _go = null;
        _possession.ChangeNullHited();
        _tpsCvc = null;
        _isTPS = false;
        _isFPS = true;
    }

    void CameraChangeMethod(CinemachineVirtualCamera oldCvc, CinemachineVirtualCamera currentCvc)
    {
        oldCvc.Priority = 0;
        currentCvc.Priority = 1;
    }
}
