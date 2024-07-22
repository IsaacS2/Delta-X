using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;

//
// Thanks to CodeFriend for JSON with Unity tutorial:
// "How to easily save and load data to JSON in Unity (Complete save system)"
// https://www.youtube.com/watch?v=pVXEUtMy_Hc
//

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] GameObject ScoreList;
    [SerializeField] private TMP_Text _leaderboardText;
    [SerializeField] public InputActionReference fire;
    public Scores scoreboard = new Scores();

    private void Start()
    {
        foreach (KeyValuePair<int, List<string>> scoreItem in ScoreRun.Instance.scores)
        {
            _leaderboardText.text += scoreItem.Key;
            foreach (string name in scoreItem.Value)
            {
                _leaderboardText.text += name + ", ";
            }
            _leaderboardText.text += "/n";
        }
    }

    /*private void OnEnable()
    {
        fire.action.started += SaveToJson;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        fire.action.started -= SaveToJson;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadFromJson();
    }

    public void SaveToJson(InputAction.CallbackContext obj)
    {
        string scoreData = JsonUtility.ToJson(scoreboard);
        string filePath = Application.persistentDataPath
            + "/ScoreData.json";
        Debug.Log(filePath);

        System.IO.File.WriteAllText(filePath, scoreData);
        Debug.Log("GOOD callback save");
    }

    public void SaveToJson()
    {
        string scoreData = JsonUtility.ToJson(scoreboard);
        string filePath = Application.persistentDataPath 
            + "/ScoreData.json";
        Debug.Log(filePath);

        System.IO.File.WriteAllText(filePath, scoreData);
        Debug.Log("GOOD sceneload save");
    }

    public void LoadFromJson(InputAction.CallbackContext obj)
    {
        string filePath = Application.persistentDataPath
            + "/ScoreData.json";
        string scoreData = System.IO.File.ReadAllText(filePath);

        scoreboard = JsonUtility.FromJson<Scores>(scoreData);
        Debug.Log("Paste Good");
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath
            + "/ScoreData.json";
        string scoreData = System.IO.File.ReadAllText(filePath);

        scoreboard = JsonUtility.FromJson<Scores>(scoreData);
        Debug.Log("Paste Good");
    }*/
}

[System.Serializable]
public class Scores
{
    public List<Score> _scores = new List<Score>();
}

[System.Serializable]
public class Score
{
    public int _scoreVal;
    public List<string> _scoreNames = new List<string>();  // names associated with score
}