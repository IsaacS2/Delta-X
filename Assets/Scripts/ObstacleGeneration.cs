using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleGeneration : MonoBehaviour
{
    [SerializeField] private LayerMask m_LayerMask;
    private Tilemap obstacles;
    private Tile[] activeTiles;
    private Camera _mainCam;

    private void Start()
    {
        _mainCam = Camera.main;
        obstacles = GetComponent<Tilemap>();

    }

    private void LateUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.CompareTag("PlayerElectricity"))
        {
            List<GameObject> destroyableObjects = new List<GameObject>();

            // disable collider so neighbors won't register this object when finding other neighbors
            GetComponent<Collider2D>().enabled = false;

            this.CheckNeighbors(ref destroyableObjects);

            foreach (GameObject destroyableObject in destroyableObjects)
            {
                Destroy(destroyableObject);
            }

            // destroy this object
            destroyableObjects = null;
            Destroy(gameObject);
        }*/
    }
}
