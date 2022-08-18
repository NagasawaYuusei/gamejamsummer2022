using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IObjectPool
{
    [SerializeField, Tooltip(""), Min(0)] int maxHp = 100;
    [SerializeField, Tooltip(""), Min(0)] int hp = 100;
    [SerializeField, Tooltip(""), Min(0)] int atk = 5;
    [SerializeField, Tooltip(""), Min(0)] float speed = 1;
    GameObject player; //
    [SerializeField, Tooltip("")] GameObject bullet; //�e
    [SerializeField, Tooltip("")] GameObject[] death; //����
    [SerializeField, Tooltip("")] GameObject hitef; //�q�b�g�G�t�F�N�g
    Animator _anim = default;
    [SerializeField] bool _isboss = false;


    //ObjectPool
    bool _isActrive = false;
    public bool IsActive => _isActrive;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<PlayerState>().GetDamage(atk);
        }

    }


    public void GetDamage(int damage)
    {
        hp -= damage;
        Debug.Log(damage + " �_���[�W���󂯂ăv���C���[��HP�� " + hp + " �ɂȂ����I");
        if (hp <= 0)
        {

            Deth();
        }
    }

    public void DisactiveForInstantiate()
    {
        gameObject.SetActive(false);
        _isActrive = false;
    }
    public void Create()
    {
        gameObject.SetActive(true);
        _isActrive = true;
    }
    public void Deth()
    {

    }
}
