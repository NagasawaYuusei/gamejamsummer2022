using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour, IObjectPool
{
    GameObject player; //
    Animator _anim = default;

    //位置の基準になるオブジェクトのTransformを収める
    //public Transform central;
    [SerializeField] Vector3 central;
    private NavMeshAgent agent;
    //ランダムで決める数値の最大値
    [SerializeField] float radius = 3;
    //設定した待機時間
    [SerializeField] float waitTime = 2;
    //待機時間を数える
    [SerializeField] float time = 0;

    /// <summary>
    /// 長さ
    /// </summary>
    [SerializeField] float _range = 5f;
    /// <summary>
    /// Layer
    /// </summary>
    [SerializeField] LayerMask _layer;
    [SerializeField] float _damageTime = 1f;
    float _time;


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
        //目標地点に近づいても速度を落とさなくなる
        agent.autoBraking = false;
        //目標地点を決める
        GotoNextPoint();
    }

    void Update()
    {
        Attack();

        //経路探索の準備ができておらず
        //目標地点までの距離が0.5m未満ならNavMeshAgentを止める
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            StopHere();
        //NavMeshAgentのスピードの2乗でアニメーションを切り替える
        //_anim.SetFloat("Blend", agent.velocity.sqrMagnitude);
    }

    void LateUpdate()
    {
        // アニメーションの処理
        if (_anim)
        {
            _anim.SetFloat("Speed", agent.velocity.magnitude);
            //_anim.SetBool("Jump", agent.isOnOffMeshLink);
        }
    }

    void Attack()
    {
        Debug.DrawRay(transform.position, transform.forward * _range);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, _range, _layer);
        if(hit)
        {
            _time += Time.deltaTime;
            if(_time >= _damageTime)
            {
                _time = 0;
                GameManager.Instance.PlayerDamage();
            }
            else
            {
                _time = 0;
            }
        }
    }
    void GotoNextPoint()
    {
        //NavMeshAgentのストップを解除
        agent.isStopped = false;

        //目標地点のX軸、Z軸をランダムで決める
        float posX = Random.Range(-1 * radius, radius);
        float posZ = Random.Range(-1 * radius, radius);

        //CentralPointの位置にPosXとPosZを足す
        Vector3 pos = central;
        pos.x += posX;
        pos.z += posZ;

        //NavMeshAgentに目標地点を設定する
        agent.destination = pos;
    }

    void StopHere()
    {
        //NavMeshAgentを止める
        agent.isStopped = true;
        //待ち時間を数える
        time += Time.deltaTime;

        //待ち時間が設定された数値を超えると発動
        if (time > waitTime)
        {
            //目標地点を設定し直す
            GotoNextPoint();
            time = 0;
        }
    }

    void OnTriggerEnter(Collider collider)
    {

        {
            GetDamage();
        }
    }


    public void GetDamage()
    {
        Debug.Log("勇者死亡");
        agent.isStopped = true;
        _anim.SetTrigger("Death");
        GameManager.Instance.ScoreUp();
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

    public void Deth()//アニメーションで呼ぶ
    {
        gameObject.SetActive(false);
        _isActrive = false;
    }
}
