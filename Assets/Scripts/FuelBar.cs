using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBar : MonoBehaviour
{
    private FuelManager fuelManager;
    // Start is called before the first frame update
    void Start()
    {
        fuelManager = GameManager.Instance.GetFuel;
    }

    // Update is called once per frame
    void Update()
    {
        // rescaling fuel bar to match percent of fuel player has left
        transform.localScale = new Vector3(transform.localScale.x, fuelManager.GetFuelPercent(), transform.localScale.z);
    }
}
