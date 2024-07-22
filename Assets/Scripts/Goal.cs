using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] private int _scoreBonus = 1000;
    [SerializeField] private int _timeBonus = 100;

    private ScoreManager _score;
    private TimerManager _time;
    private FuelManager _fuel;

    private void OnEnable()
    {
        _score = GameManager.Instance.GetScore;
        _time = GameManager.Instance.GetTimer;
        _fuel = GameManager.Instance.GetFuel;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _score.AddScore(_scoreBonus + (int)(_timeBonus - _time.GetTime()) + (int)_fuel.GetFuel());
            GameManager.Instance.GetNewScore(_score.GetScore());
        }
    }
}
