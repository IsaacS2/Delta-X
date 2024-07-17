using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private int _pointVal = 2;
    [SerializeField] private int _fuelAmount = 10;

    private FuelManager _fuel;
    private ScoreManager _score;

    private void Start()
    {
        _fuel = GameManager.Instance.GetFuel;
        _score = GameManager.Instance.GetScore;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _fuel.Refuel(_fuelAmount);
            _score.AddScore(_pointVal);
            Destroy(gameObject);
        }
    }
}
