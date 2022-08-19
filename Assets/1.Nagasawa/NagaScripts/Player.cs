using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField, Tooltip("�X�s�[�h")] float _moveSpeed = 10f;
    [SerializeField, Tooltip("�ʏ�搔")] float _movementMultiplier = 10f;
    [SerializeField, Tooltip("�󒆏搔")] float _airMultiplier = 0.1f;

    [Header("Jump")]
    [SerializeField, Tooltip("�W�����v�p���[")] float _jumpPower = 15f;
    [SerializeField, Tooltip("�n�ʃ��C���[")] LayerMask _zimen;
    [SerializeField, Tooltip("Gizmo�\��")] bool _isGizmo = true;
    [Tooltip("�ݒu����̒��_")] Vector3 _centor;
    [Tooltip("�ݒu����̃T�C�Y")] Vector3 _size;
    [SerializeField, Tooltip("���_����")] Vector3 _collisionPoint;
    [SerializeField, Tooltip("�T�C�Y����")] Vector3 _collisionSize;

    [Header("Drag")]
    [SerializeField, Tooltip("�n�ʎ��̏d��")] float _groundDrag = 6f;
    [SerializeField, Tooltip("�󒆎��̏d��")] float _airDrag = 1f;

    [Header("Input")]
    [Tooltip("�C���v�b�g�V�X�e���W�����v")] bool _isJump;
    [Tooltip("�C���v�b�g�V�X�e���ړ�")] Vector3 _moveDir;

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

    /// <summary>�ŏ��̃Z�b�g�A�b�v</summary>  
    void FirstSetUp()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    /// <summary>�A�b�v�f�[�g���Ƃ̏��</summary>
    void State()
    {
        //Gizmo����
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

    /// <summary>�d�͑���</summary>
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

    /// <summary>�W�����v</summary>
    void Jump()
    {
        if (_isJump && IsGround())
        {
            _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            _isJump = false;
        }
    }

    /// <summary>�ړ�</summary>
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
    /// �ݒu����
    /// </summary>
    /// <returns>
    /// �ڒn true 
    /// �� false
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
    /// �ݒu�����Gizmo�\��
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
