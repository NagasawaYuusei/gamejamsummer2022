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
    //ポーズしているかどうか
    private bool isPose = false;
    
    public void ChangeTimer()
    {
        //ポーズ中にクリックされたとき
        if (isPose)
        {
            Debug.Log("Start");
            //ポーズ状態を解除する
            isPose = false;
        }
        //進行中にクリックされたとき
        else
        {
            Debug.Log("Pause");
            //ポーズ状態にする
            isPose = true;
        }
    }

    void Start()
    {
        ChangeTimer();
    }

    void Update()
    {
        //ポーズ中かどうか
        if (isPose)
        {
            //ポーズ中であることを表示
            _timeText.text = _countdown.ToString("f1");

            //カウントダウンしない
            return;
        }
        //時間をカウントダウンする
        _countdown -= Time.deltaTime;

        //時間を表示する
        _timeText.text = _countdown.ToString("f1");

        //countdownが以下になったとき
        if (_countdown <= 30 && _countdown >= 28)
        {
            _timeText.text = _countdown.ToString("f1");
            _timemiddleText.text = "残り時間30秒です";
        }
        else if (_countdown < 28 && _countdown > 0)
        {
            _timeText.text = _countdown.ToString("f1");
            _timemiddleText.text = "";
        }
        else if (_countdown <= 0)
        {
            _timeText.text = "0.0";
            _timeupText.text = "時間になりました";
            _countdown = 0;
        }

    }
    
}