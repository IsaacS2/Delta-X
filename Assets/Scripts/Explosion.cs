using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Explosion : MonoBehaviour
{
    [SerializeField] private int timeActive = 1;
    private BoxCollider2D _bc;


    // Start is called before the first frame update
    void Start()
    {
        _bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
