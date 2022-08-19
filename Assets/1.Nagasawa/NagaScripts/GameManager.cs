using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game��S�̊Ǘ�����Manager
/// </summary>
public class GameManager : MonoBehaviour
{
    //Singlton��
    public static GameManager Instance;

    int _score;
    [SerializeField] int _currentScore = 100;

    int _life;
    [SerializeField] GameObject[] _lifeGOs;
    void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        _life = _lifeGOs.Length;
    }

    public void PlayerDamage()
    {
        _lifeGOs[_life - 1].SetActive(false);
        _life--;
        if(_life == 0)
        {
            GameOver();
        }
    }

    public void ScoreUp()
    {
        _score += _currentScore;
    }

    public void GameOver()
    {

    }
}
