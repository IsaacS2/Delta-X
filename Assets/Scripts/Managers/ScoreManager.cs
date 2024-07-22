using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    private int _totalScore = 0;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _totalScore = 0;
    }

    public void AddScore(int pointVal)
    {
        _totalScore += pointVal;
        _scoreText.text = _totalScore.ToString();
    }

    public int GetScore()
    {
        return _totalScore;
    }
}
