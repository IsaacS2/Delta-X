using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreRun : MonoBehaviour
{
    public static ScoreRun Instance;
    public SortedDictionary<int, List<string>> scores;
    private int _scoreRun;
    private string _name;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _scoreRun = 0;
        scores = new SortedDictionary<int, List<string>>();
    }

    public void newScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        int index = scene.buildIndex;
        Debug.Log(index);
        Debug.Log("Total scenes: " + SceneManager.sceneCountInBuildSettings);

        if (index >= SceneManager.sceneCountInBuildSettings - 1) { index = 0; }
        else { index++; }

        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    public void GetNewScore(int newScore)
    {
        _scoreRun = newScore;
        newScene();
    }

    public void ReadName(string s)
    {
        _name = s;
        
        if (!scores.ContainsKey(_scoreRun))
        {
            List<string> nameList = new List<string>();
            nameList.Add(_name);
            scores[_scoreRun] = nameList;
        }
        else
        {
            List<string> nameList = scores[_scoreRun];
            nameList.Add(_name);
            scores[_scoreRun] = nameList;
        }

        newScene();
    }

    public int CurrentScore()
    {
        return _scoreRun;
    }
}
