using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PossessedSlider : MonoBehaviour
{
    public Canvas canvas;
    bool _isActive;
    float _value;
    [SerializeField] float _maxValue = 10f;
    [SerializeField] Slider _slider;

    public bool IsActive => _isActive;

    void Start()
    {
        ActiveSwitch(_isActive);
    }

    void Update()
    {
        Rotate();
        if(_isActive)
        {
            _value += Time.deltaTime;
            if(_maxValue < _value)
            {
                ActiveSwitch(false);
                _value = 0;
            }
            _slider.value = (float)_value / (float)_maxValue;
        }
    }

    void Rotate()
    {
        canvas.transform.rotation =
            Camera.main.transform.rotation;
    }

    public void ActiveSwitch(bool on)
    {
        _isActive = on;
        canvas.gameObject.SetActive(on);
    }
}


