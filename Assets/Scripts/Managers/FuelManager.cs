using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelManager : MonoBehaviour
{
    //public static event EventHandler OnFuelSegmentDepleted;
    [SerializeField] private float _maxFuel = 60;
        
    private float _fuel;
    private bool _fuelCountdown;
    public event EventHandler OnFuelEmpty;

    private void OnEnable()
    {
        GameManager.Instance.GetTimer.OnGameplayStart += Refuel;
    }

    private void OnDisable()
    {
        GameManager.Instance.GetTimer.OnGameplayStart -= Refuel;
    }

    // Start is called before the first frame update
    void Start()
    {
        _fuel = _maxFuel;
    }

    // Update is called once per frame
    void Update()
    {
        if (_fuelCountdown)
        {
            _fuel -= Time.deltaTime * 2;
            if (_fuel <= 0)
            {
                OnEmptyingFuel();
            }
        }
    }

    public float GetFuel()
    {
        return _fuel;
    }

    public float GetFuelPercent()
    {
        return _fuel / _maxFuel;
    }

    public void FuelDepletion (int depletedFuel) 
    {
        _fuel -= depletedFuel;
        if (_fuel <= 0)
        {
            OnEmptyingFuel();
        }
    }

    public void Refuel(object sender, int fuelAmount)
    {
        _fuel += fuelAmount;
        if (fuelAmount > _maxFuel)
        {
            setCountdown(true);
            _fuel = _maxFuel;
        }
    }

    public void Refuel(int fuelAmount)
    {
        _fuel += fuelAmount;
        if (_fuel >= _maxFuel)
        {
            int overflow = (int)Mathf.Round(_fuel - _maxFuel);
            GameManager.Instance.GetScore.AddScore(overflow);
            _fuel = _maxFuel;
        }
    }

    private void setCountdown(bool counting)
    {
        _fuelCountdown = counting;
    }

    private void OnEmptyingFuel()
    {
        _fuel = Math.Max(_fuel, 0);
        _fuelCountdown = false;
        OnFuelEmpty?.Invoke(this, null);
    }
}
