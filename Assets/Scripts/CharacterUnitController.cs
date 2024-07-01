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

        _animator.SetBool("walking", true);
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 aim = new Vector3(horizontal, 0, 0).normalized;
        Vector3 velocity = new Vector3(horizontal, _rigidbody.velocity.y, 0).normalized;
        _rigidbody.velocity = velocity * _speed;
    }
}