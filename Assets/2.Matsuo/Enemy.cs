using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour, IObjectPool
{
    GameObject player; //
    Animator _anim = default;

    //�ʒu�̊�ɂȂ�I�u�W�F�N�g��Transform�����߂�
    //public Transform central;
    [SerializeField] Vector3 central;
    private NavMeshAgent agent;
    //�����_���Ō��߂鐔�l�̍ő�l
    [SerializeField] float radius = 3;
    //�ݒ肵���ҋ@����
    [SerializeField] float waitTime = 2;
    //�ҋ@���Ԃ𐔂���
    [SerializeField] float time = 0;


    //ObjectPool
    bool _isActrive = false;
    public bool IsActive => _isActrive;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        //�ڕW�n�_�ɋ߂Â��Ă����x�𗎂Ƃ��Ȃ��Ȃ�
        agent.autoBraking = false;
        //�ڕW�n�_�����߂�
        GotoNextPoint();
    }

    void Update()
    {
        //�o�H�T���̏������ł��Ă��炸
        //�ڕW�n�_�܂ł̋�����0.5m�����Ȃ�NavMeshAgent���~�߂�
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            StopHere();
        //NavMeshAgent�̃X�s�[�h��2��ŃA�j���[�V������؂�ւ���
        _anim.SetFloat("Blend", agent.velocity.sqrMagnitude);
    }

    void LateUpdate()
    {
        // �A�j���[�V�����̏���
        if (_anim)
        {
            _anim.SetFloat("Speed", agent.velocity.magnitude);
            //_anim.SetBool("Jump", agent.isOnOffMeshLink);
        }
    }

    void GotoNextPoint()
    {
        //NavMeshAgent�̃X�g�b�v������
        agent.isStopped = false;

        //�ڕW�n�_��X���AZ���������_���Ō��߂�
        float posX = Random.Range(-1 * radius, radius);
        float posZ = Random.Range(-1 * radius, radius);

        //CentralPoint�̈ʒu��PosX��PosZ�𑫂�
        Vector3 pos = central;
        pos.x += posX;
        pos.z += posZ;

        //NavMeshAgent�ɖڕW�n�_��ݒ肷��
        agent.destination = pos;
    }

    void StopHere()
    {
        //NavMeshAgent���~�߂�
        agent.isStopped = true;
        //�҂����Ԃ𐔂���
        time += Time.deltaTime;

        //�҂����Ԃ��ݒ肳�ꂽ���l�𒴂���Ɣ���
        if (time > waitTime)
        {
            //�ڕW�n�_��ݒ肵����
            GotoNextPoint();
            time = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetDamage();

        }
    }


    public void GetDamage()
    {
        Debug.Log("�E�Ҏ��S");
        agent.isStopped = true;
        _anim.SetTrigger("Death");
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

    public void Deth()//�A�j���[�V�����ŌĂ�
    {
        gameObject.SetActive(false);

    }
}
