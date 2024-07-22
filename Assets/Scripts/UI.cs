using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] public InputActionReference fire;

    private void OnEnable()
    {
        fire.action.started += NextScene;
    }

    private void OnDisable()
    {
        fire.action.started -= NextScene;
    }

    private void NextScene(InputAction.CallbackContext obj)
    {
        ScoreRun.Instance.newScene();
    }
}