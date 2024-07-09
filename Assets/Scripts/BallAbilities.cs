using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class BallAbilities : MonoBehaviour
{
    public Rigidbody2D _rb;


    [SerializeField] private int _attackBuffer = 5;
    [SerializeField] private float _moveSpeed = 3;

    [SerializeField] private Vector2 _moveDirection;

    [SerializeField] private GameObject _explosion;

    [SerializeField] public InputActionReference move;
    [SerializeField] public InputActionReference fire;

    public static event EventHandler OnEndZoneEmpty;

    void Update()
    {
        _moveDirection = move.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_moveDirection.x * _moveSpeed, 
            _moveDirection.y * _moveSpeed);
        Debug.Log(_rb.velocity);
    }

    private void OnEnable()
    {
        fire.action.started += Fire;
    }

    private void OnDisable()
    {
        fire.action.started -= Fire;
    }

    private void Fire(InputAction.CallbackContext obj)  // create explosion object
    {
        Instantiate(_explosion, _rb.transform);
    }
}
