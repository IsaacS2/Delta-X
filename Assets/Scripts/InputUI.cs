using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class InputUI : UI
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _name;

    private void OnEnable()
    {
        fire.action.started += NextScene;
    }

    private void OnDisable()
    {
        fire.action.started -= NextScene;
    }

    private void Start()
    {
        _score.text += ScoreRun.Instance.CurrentScore().ToString();
    }

    public void GetString (string s)
    {
        ScoreRun.Instance.ReadName(s);
    }

    private void NextScene(InputAction.CallbackContext obj)
    {
        GetString(_name.text);
    }
}
