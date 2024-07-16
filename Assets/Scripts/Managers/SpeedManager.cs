using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _stars;
    private SpriteRenderer[] _starFill;

    private BallAbilities player;
    private SpriteRenderer _currentStar;
    private float _currentSpeedIncrement = 0;
    private float _currentSpeedMultiple;
    private float _maxSpriteWidth;
    private int _currentStarNumber = 0;

    public event EventHandler<int> OnSpeedChange;  // listen to player's speed changes with its event handler

    public void OnEnable()
    {
        _starFill = new SpriteRenderer[_stars.Length];

        // get fill for each star sprite
        for (int i = 0; i < _stars.Length; i++)
        {
            _starFill[i] = _stars[i].transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        }

        // no need for parent gameObjects
        _stars = null;
    }

    private void Start()
    {
        _currentStar = _starFill[0];
        _maxSpriteWidth = _currentStar.size.y;  // getting height for width since the sprite is 1:1 ratio
        _currentSpeedMultiple = _maxSpriteWidth / 5;
    }

    void Update()
    {
        _currentStar.size = new Vector2(Math.Min(_maxSpriteWidth, _currentStar.size.x + (_currentSpeedIncrement * _currentSpeedMultiple * Time.deltaTime)),
            _maxSpriteWidth);
        if (_currentStar.size.x == _maxSpriteWidth)
        {
            if (_currentStarNumber < _starFill.Length - 1)  // time to increase player speed
            {
                ++_currentStarNumber;
                _currentStar = _starFill[_currentStarNumber];
                OnSpeedChange?.Invoke(this, _currentStarNumber);
            }
        }
        else if (_currentStar.size.x < 0)
        {
            _currentStar.size = Vector2.zero;
            if (_currentStarNumber > 0)  // time to decrease player speed
            {
                --_currentStarNumber;
                _currentStar = _starFill[_currentStarNumber];
                OnSpeedChange?.Invoke(this, _currentStarNumber);
            }
        }
    }

    public void ChangeSpeedIncrement(float newSpeed)
    {
        _currentSpeedIncrement = newSpeed;
    }
}
