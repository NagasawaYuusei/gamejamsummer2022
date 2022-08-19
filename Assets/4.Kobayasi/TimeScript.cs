using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    [SerializeField] float _countdown = 60f;
    [SerializeField] Text _timeText;
    [SerializeField] Text _timemiddleText;
    [SerializeField] Text _timeupText;
    public Slider slider;
    //�|�[�Y���Ă��邩�ǂ���
    private bool isPose = false;
    float _max = 0;
    
    public void ChangeTimer()
    {
        //�|�[�Y���ɃN���b�N���ꂽ�Ƃ�
        if (isPose)
        {
            Debug.Log("Start");
            //�|�[�Y��Ԃ���������
            isPose = false;
        }
        //�i�s���ɃN���b�N���ꂽ�Ƃ�
        else
        {
            Debug.Log("Pause");
            //�|�[�Y��Ԃɂ���
            isPose = true;
        }
    }

    void Start()
    {
        ChangeTimer();
        _max = _countdown;
    }

    void Update()
    {
        //�|�[�Y�����ǂ���
        if (isPose)
        {

            //�J�E���g�_�E�����Ȃ�
            return;
        }
        //���Ԃ��J�E���g�_�E������
        _countdown -= Time.deltaTime;

      

        //countdown���ȉ��ɂȂ����Ƃ�
        if (_countdown <= 30 && _countdown >= 28)
        {
            //_timeText.text = _countdown.ToString("f1");
            _timemiddleText.text = "�c�莞��30�b�ł�";
        }
        else if (_countdown < 28 && _countdown > 0)
        {
            //_timeText.text = _countdown.ToString("f1");
            _timemiddleText.text = "";
        }
        else if (_countdown <= 0)
        {
            //_timeText.text = "0.0";
            _timeupText.text = "���ԂɂȂ�܂���";
            _countdown = 0;
        }
        slider.value = (float)_countdown / (float)_max; ;
        Debug.Log("slider.value : " + slider.value);
    }
    
}