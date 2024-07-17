using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float timeActive = 0.5f;

    private BoxCollider2D _bc;
    private GameObject _player;

    private void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        _bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null) { transform.position = _player.transform.position; }

        timeActive -= Time.deltaTime;
        if (timeActive <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ReturnEnergy()
    {
        if (_player.GetComponent<BallAbilities>() != null)
        {
            _player.GetComponent<BallAbilities>().RefundAttackEnergy();
        }
    }
}
