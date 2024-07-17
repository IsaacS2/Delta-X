using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Destructible
{
    [SerializeField] private float _movementSpeed, _sensorRadius = 2;
    [SerializeField] private int _damageEnergyCost = 8;
    private Rigidbody2D _target, _rb;

    private void Start()
    {
        base.SetScore();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // pursue player if found
        if (_target != null)
        {
            Vector2 direction = (_target.transform.position - _rb.transform.position);
            direction.Normalize();
            _rb.velocity = direction * _movementSpeed * Time.fixedDeltaTime;
        }

        // look for target (player)
        else
        {
            Collider2D[] contacts = Physics2D.OverlapCircleAll(_rb.transform.position, _sensorRadius);

            foreach (Collider2D contact in contacts)
            {
                if (contact.gameObject.CompareTag("Player"))
                {
                    Debug.Log("target found");
                    _target = contact.gameObject.GetComponent<Rigidbody2D>();
                    break;
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // player hit!
        {
            collision.gameObject.GetComponent<BallAbilities>().Damaged(_damageEnergyCost);
        }
    }
}
