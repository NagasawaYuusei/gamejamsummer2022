using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField, Tooltip("スピード")] float _moveSpeed = 10f;
    [SerializeField, Tooltip("通常乗数")] float _movementMultiplier = 10f;
    [SerializeField, Tooltip("空中乗数")] float _airMultiplier = 0.1f;

    [Header("Jump")]
    [SerializeField, Tooltip("ジャンプパワー")] float _jumpPower = 15f;
    [SerializeField, Tooltip("地面レイヤー")] LayerMask _zimen;
    [SerializeField, Tooltip("Gizmo表示")] bool _isGizmo = true;
    [Tooltip("設置判定の中点")] Vector3 _centor;
    [Tooltip("設置判定のサイズ")] Vector3 _size;
    [SerializeField, Tooltip("中点差分")] Vector3 _collisionPoint;
    [SerializeField, Tooltip("サイズ差分")] Vector3 _collisionSize;

    [Header("Drag")]
    [SerializeField, Tooltip("地面時の重力")] float _groundDrag = 6f;
    [SerializeField, Tooltip("空中時の重力")] float _airDrag = 1f;

    [Header("Input")]
    [Tooltip("インプットシステムジャンプ")] bool _isJump;
    [Tooltip("インプットシステム移動")] Vector3 _moveDir;

    [Header("Ather")]
    [Tooltip("Rigidbody")] Rigidbody _rb;

    bool m_on;

    void Start()
    {
        FirstSetUp();
    }

    void Update()
    {
        State();
        PlayerInput();
        ControlDrag();
    }

    void FixedUpdate()
    {
        Move();
    }

    /// <summary>最初のセットアップ</summary>  
    void FirstSetUp()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    /// <summary>アップデートごとの状態</summary>
    void State()
    {
        //Gizmo差分
        _centor = transform.position + _collisionPoint;
        _size = transform.localScale + _collisionSize;
    }

    void PlayerInput()
    {
        if(Input.GetButtonDown("Jump"))
        {
            _isJump = true;
        }

        _moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    /// <summary>重力操作</summary>
    void ControlDrag()
    {
        if (IsGround())
        {
            _rb.drag = _groundDrag;
        }
        else
        {
            _rb.drag = _airDrag;
        }
    }

    /// <summary>ジャンプ</summary>
    void Jump()
    {
        if (_isJump && IsGround())
        {
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            _isJump = false;
        }
    }

    /// <summary>移動</summary>
    void Move()
    {
        Vector3 dir = Camera.main.transform.TransformDirection(_moveDir);
        dir.y = 0;
        if (IsGround())
        {
            _rb.AddForce((dir.normalized * _moveSpeed * _movementMultiplier) + _rb.velocity.y * Vector3.up, ForceMode.Acceleration);
        }
        else
        {
            _rb.AddForce((dir.normalized * _moveSpeed * _movementMultiplier * _airMultiplier) + (_rb.velocity.y * Vector3.up), ForceMode.Acceleration);
        }
    }

    /// <summary>
    /// 設置判定
    /// </summary>
    /// <returns>
    /// 接地 true 
    /// 空中 false
    /// </returns>
    public bool IsGround()
    {
        Collider[] collision = Physics.OverlapBox(_centor, _size, Quaternion.identity, _zimen);
        if (collision.Length != 0)
        {
            if (m_on)
            {
                m_on = false;
            }

            return true;
        }
        else
        {
            m_on = true;
            _isJump = false;
            return false;
        }
    }

    /// <summary>
    /// 設置判定のGizmo表示
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (_isGizmo)
        {
            Gizmos.DrawCube(_centor, _size);
        }
    }
}
