using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Utilizing singleton pattern
    public static GameManager Instance;
    [SerializeField] private FuelManager _fuelManager;
    [SerializeField] private SpeedManager _speedManager;
    [SerializeField] private TimerManager _timerManager;

    public static event EventHandler OnLevelStart;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        _fuelManager.enabled = true;
        _speedManager.enabled = true;
        _timerManager.enabled = true;
    }

    public FuelManager GetFuel => _fuelManager;
    public SpeedManager GetSpeed => _speedManager;
    public TimerManager GetTimer => _timerManager;
}
