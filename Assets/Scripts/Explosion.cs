using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Explosion : MonoBehaviour
{
    [SerializeField] private float timeActive = 1;

    private BoxCollider2D _bc;
    private GameObject _player;

    //public static event EventHandler<> OnTileContact;

    private void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        _bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _player.transform.position;

        timeActive -= Time.deltaTime;
        if (timeActive <= 0)
        {
            Destroy(gameObject);
        }
    }
}
