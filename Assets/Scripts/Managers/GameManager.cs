using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Utilizing singleton pattern
    public static GameManager Instance;
    [SerializeField] private FuelManager _fuelManager;
    [SerializeField] private SpeedManager _speedManager;
    [SerializeField] private TimerManager _timerManager;
    [SerializeField] private ScoreManager _scoreManager;

    //public static event EventHandler OnLevelStart;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }

        Instance = this;
    }

    public FuelManager GetFuel => _fuelManager;
    public SpeedManager GetSpeed => _speedManager;
    public TimerManager GetTimer => _timerManager;
    public ScoreManager GetScore => _scoreManager;

    public void GetNewScore(int newScore)
    {
        ScoreRun.Instance.GetNewScore(newScore);
    }
}
