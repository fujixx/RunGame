using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnitController : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private int _maxRotationSpeed = 600;
    [SerializeField] private int _attack = 10;
    public int Attack => _attack;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Animator _animator;
    private Quaternion _playerRotation;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();

        _playerRotation = _transform.rotation;
    }

    void FixedUpdate()
    {
        // 移動処理
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 aim = new Vector3(horizontal, 0, vertical).normalized;
        if (aim.magnitude > 0.5f)
        {
            _playerRotation = Quaternion.LookRotation(aim, Vector3.up);
        }
        // 目標の回転角度を計算
        Quaternion deltaRotation = Quaternion.Inverse(_transform.rotation) * _playerRotation;
        // 回転速度を計算
        float maxDegreesDelta = _maxRotationSpeed * Time.deltaTime;
        // 目標の回転角度を回転速度で制限
        Quaternion limitedRotation = Quaternion.RotateTowards(Quaternion.identity, deltaRotation, maxDegreesDelta);
        // 1フレームごとの回転量を計算
        Quaternion rotationThisFrame = _transform.rotation * limitedRotation;
        // 回転を適用
        _transform.rotation = rotationThisFrame;

        Vector3 velocity = new Vector3(horizontal, _rigidbody.velocity.y, vertical).normalized;
        if (velocity.magnitude > 0.1f)
        {
            _animator.SetBool("walking", true);
        }
        else
        {
            _animator.SetBool("walking", false);
        }
        _rigidbody.velocity = velocity * _speed;
    }
}