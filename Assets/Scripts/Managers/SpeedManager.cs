using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _stars;
    private SpriteRenderer[] _starFill;

    private BallAbilities player;
    private SpriteRenderer currentStar;
    private float currentSpeedIncrement = 0;
    private float currentSpeedMultiple;
    private float maxSpriteWidth;
    private int currentStarNumber = 0;

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

        // listen to player's speed changes with its event handler
        /*if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<BallAbilities>();
        }*/
    }

    private void Start()
    {
        currentStar = _starFill[0];
        maxSpriteWidth = currentStar.size.y;  // getting height for width since the sprite is 1:1 ratio
        currentSpeedMultiple = maxSpriteWidth / 6;
    }

    void Update()
    {
        currentStar.size = new Vector2(Math.Min(maxSpriteWidth, currentStar.size.x + (currentSpeedIncrement * currentSpeedMultiple * Time.deltaTime)),
            maxSpriteWidth);
        if (currentStar.size.x == maxSpriteWidth)
        {
            if (currentStarNumber < _starFill.Length - 1)  // time to increase player speed
            {
                ++currentStarNumber;
                currentStar = _starFill[currentStarNumber];
            }
        }
        else if (currentStar.size.x < 0)
        {
            currentStar.size = Vector2.zero;
            if (currentStarNumber > 0)  // time to decrease player speed
            {
                --currentStarNumber;
                currentStar = _starFill[currentStarNumber];
            }
        }
    }

    public void ChangeSpeedIncrement(float newSpeed)
    {
        currentSpeedIncrement = newSpeed;
    }


}
