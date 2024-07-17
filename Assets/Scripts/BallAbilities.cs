using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ParticleSystem))]
public class BallAbilities : MonoBehaviour
{
    private bool _dead;
    private float _moveSpeed, _attackBuff, _attackLag, _invincibility = 0;
    private Vector2 _moveDirection;
    private Vector3 _prevLocation;
    private Rigidbody2D _rb;
    private SpriteRenderer _sprRend;
    private FuelManager _fuel;
    private SpeedManager _speed;
    private ParticleSystem _particles;

    [SerializeField] private float _attackBufferTime = 0.1f;
    [SerializeField] private float _attackEndLagTime = 0.75f;
    [SerializeField] private float _invincibilityTime = 0.5f;
    [SerializeField] private int _attackEnergyCost = 4;

    [SerializeField] private float _speedInc;
    [SerializeField] private float[] _speeds;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private Color _noPowerColor;
    [SerializeField] private Color _damagedColor;

    [SerializeField] public InputActionReference move;
    [SerializeField] public InputActionReference fire;

    private void OnEnable()
    {
        fire.action.started += Fire;
        GameManager.Instance.GetFuel.OnFuelEmpty += Die;
        GameManager.Instance.GetSpeed.OnSpeedChange += ChangeSpeed;
    }

    private void OnDisable()
    {
        fire.action.started -= Fire;
        GameManager.Instance.GetFuel.OnFuelEmpty -= Die;
        GameManager.Instance.GetSpeed.OnSpeedChange -= ChangeSpeed;
    }

    private void Start()
    {
        _sprRend = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _particles = GetComponent<ParticleSystem>();
        _fuel = GameManager.Instance.GetFuel;
        _speed = GameManager.Instance.GetSpeed;
        _moveSpeed = (_speeds.Length == 0) ? 5 : _speeds[0];  // initialize player speed with first speed val or 5 (default)
    }

    void Update()
    {
        if (_dead && _particles.isStopped)
        {
            
        }

        _moveDirection = move.action.ReadValue<Vector2>();

        if (_invincibility > 0 && !_dead)
        {
            _invincibility -= Time.deltaTime;
            if (_invincibility <= 0)
            {
                if (_attackLag > 0)
                {
                    _sprRend.color = _noPowerColor;
                }
                else
                {
                    _sprRend.color = Color.white;
                }
            }
        }

        // checking for attack buffer
        if (_attackBuff > 0) {
            _attackBuff -= Time.deltaTime;
            if (_attackLag <= 0 && _fuel.GetFuel() > _attackEnergyCost * 2)
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

        if (!_dead) { _rb.velocity = new Vector2(xVelo, yVelo); }
        
        //
        // Speed increase/decrease calculation for speed manager
        //
        if (_rb.velocity == Vector2.zero || _rb.transform.position == _prevLocation)
        {
            _speed.ChangeSpeedIncrement(-3);
        }
        else if (_rb.velocity == prevVelo)  // if velocity is the same, either player is moving straight
        {
            _speed.ChangeSpeedIncrement(1.5f);
        }
        else  // player is turning, reduce speed minimally
        {
            _speed.ChangeSpeedIncrement(-0.5f);
        }

        _prevLocation = _rb.transform.position;
    }

    private void Fire(InputAction.CallbackContext obj)  // create explosion object
    {
        if (_attackLag <= 0) 
        {
            if (_fuel.GetFuel() > _attackEnergyCost * 2) { Explosion(); }
        }
        else { _attackBuff = _attackBufferTime; }
    }

    private void Explosion()
    {
        if (_invincibility <= 0) {  // can't attack during post-hit invincibility
            Instantiate(_explosion, _rb.transform);
            _attackLag = _attackEndLagTime;  // add end lag that reduces attack spamming
            _attackBuff = 0;
            _sprRend.color = _noPowerColor;
            _fuel.FuelDepletion(_attackEnergyCost);  // remove fuel after attack
        }
    }

    public void ChangeSpeed(object sender, int speedLevelIndex)
    {
        _moveSpeed = _speeds[Math.Max(0, Math.Min(speedLevelIndex, _speeds.Length - 1))];
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        _speed.ChangeSpeedIncrement(-40);
    }

    public void Die(object sender, EventArgs args)
    {
        _particles.Play();
        _sprRend.enabled = false;
        _dead = true;
        _rb.velocity = Vector2.zero;
    }

    public void Damaged(int damage)
    {
        if (_invincibility <= 0 && !_dead)
        {
            _fuel.FuelDepletion(damage);  // remove fuel after taking damage
            _invincibility = _invincibilityTime;
            _sprRend.color = _damagedColor;
        }
    }

    // When player hits enemy with explosion attack, the energy used for the attack is returned
    public void RefundAttackEnergy()
    {
        _fuel.Refuel(_attackEnergyCost);
    }
}
