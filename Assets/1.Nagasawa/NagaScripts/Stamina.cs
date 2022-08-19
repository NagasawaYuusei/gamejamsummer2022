using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    CameraChange _cc;
    float _time;
    [SerializeField] float _maxTime;
    [SerializeField] Slider _slider;
    [SerializeField] float _recoveryMultiple = 2f;
    bool _isOutOfStamina = true;

    public bool IsOutOfStamina => _isOutOfStamina;

    void Start()
    {
        _time = _maxTime;
        _cc = GetComponent<CameraChange>();    
    }
    void Update()
    {
        if(_cc.IsTPS)
        {
            _time -= Time.deltaTime;
        }
        else
        {
            _time += Time.deltaTime * _recoveryMultiple;
        }

        if(_time >= _maxTime)
        {
            _isOutOfStamina = true;
            _time = _maxTime;
        }

        if(_time <= 0f)
        {
            _isOutOfStamina = false;
            _time = 0;
        }
        _slider.value = (float)_time / (float)_maxTime;
    }
}
