using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class BallAbilities : MonoBehaviour
{
    private Rigidbody2D _rb;
    private SpriteRenderer _sprRend;

    private bool _attackInput;
    [SerializeField] private float _attackBufferTime = 0.1f;
    [SerializeField] private float _attackEndLagTime = 0.75f;
    [SerializeField] private int _attackEnergyCost = 4;
    [SerializeField] private int _damageEnergyCost = 8;
    [SerializeField] private float _moveSpeed = 3;

    private float _attackBuff;
    private float _attackLag;
    private Vector2 _moveDirection;

    [SerializeField] private GameObject _explosion;
    [SerializeField] private Color _noPowerColor;
    [SerializeField] private Color _damagedColor;

    [SerializeField] public InputActionReference move;
    [SerializeField] public InputActionReference fire;
    
    private FuelManager _fuel;
    private SpeedManager _speed;

    private void Start()
    {
        _sprRend = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _fuel = GameManager.Instance.GetFuel;
        _speed = GameManager.Instance.GetSpeed;
    }

    void Update()
    {
        _moveDirection = move.action.ReadValue<Vector2>();

        // checking for attack buffer
        if (_attackBuff > 0) {
            _attackBuff -= Time.deltaTime;
            if (_attackLag <= 0 && _fuel.GetFuel() + _attackEnergyCost > _attackEnergyCost)
            {
                Explosion();
            }
        }

        // checking for lag after attack to reduce attack spamming
        if (_attackLag > 0)
        {
            _attackLag -= Time.deltaTime;
            if (_attackLag <= 0)
            {
                _sprRend.color = Color.white;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 prevVelo = _rb.velocity;
        float xVelo = _moveDirection.x * _moveSpeed;
        float yVelo = _moveDirection.y * _moveSpeed;
        
        _rb.velocity = new Vector2(xVelo, yVelo);

        //
        // Speed increase/decrease calculation for speed manager
        //
        if (_rb.velocity == Vector2.zero)
        {
            _speed.ChangeSpeedIncrement(-1);
        }
        else if (_rb.velocity == prevVelo)  // if velocity is the same, either player is moving straight
        {
            _speed.ChangeSpeedIncrement(1);
        }
        else  // player is turning, reduce speed minimally
        {
            _speed.ChangeSpeedIncrement(-0.1f);
        }

        /*if ((xVeloSquared >= Math.Pow(_moveSpeed, 2)) || (yVeloSquared >= Math.Pow(_moveSpeed, 2)))  // player going straight forward, increase speed
        {
            
        }*/
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
        if (_attackLag <= 0) 
        {
            if (_fuel.GetFuel() + _attackEnergyCost > _attackEnergyCost) { Explosion(); }
        }
        else { _attackBuff = _attackBufferTime; }
    }

    private void Explosion()
    {
        Instantiate(_explosion, _rb.transform);
        _attackLag = _attackEndLagTime;
        _attackBuff = 0;
        _sprRend.color = _noPowerColor;
        _fuel.FuelDepletion(_attackEnergyCost);
    }
}
