using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private bool _gameStarted;
    private float _gameStartTime;
    [SerializeField] private float _gameStartTimer = 0;

    public event EventHandler<int> OnGameplayStart;

    void Start()
    {
        _gameStartTime = _gameStartTimer;
    }

    void Update()
    {
        if (_gameStartTime >= 0)
        {
            _gameStartTime -= Time.deltaTime;
            if (_gameStartTime <= 0)
            {
                _gameStarted = true;
                OnGameplayStart?.Invoke(this, 1000);
            }
        }
        else
        {
            
        }
    }
}
