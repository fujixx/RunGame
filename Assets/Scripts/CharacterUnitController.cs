using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UniRx;

public class CharacterUnitController : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private int _attack = 10;
    [SerializeField] private TextMeshProUGUI _label;

    public int Attack => _attack;
    private Rigidbody _rigidbody;
    private Transform _transform;
    private Animator _animator;
    private Quaternion _playerRotation;

    private int _score = 0;
    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            _label.text = _score.ToString();
        }
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();

        _playerRotation = _transform.rotation;

        _animator.SetBool("running", true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("jump");
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 currentPosition = _rigidbody.position;
        if (currentPosition.x <= -2 && horizontal < 0)
        {
            horizontal = 0;
        }
        if (currentPosition.x >= 2 && horizontal > 0)
        {
            horizontal = 0;
        }

        Vector3 velocity = new Vector3(horizontal, 0, 0).normalized;
        _rigidbody.velocity = velocity * _speed;
    }
}