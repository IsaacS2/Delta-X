using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Destructible : MonoBehaviour
{
    [SerializeField] private LayerMask m_LayerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerElectricity"))
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
        }
    }

    public void CheckNeighbors(ref List<GameObject> destroyableObjects)
    {
        Collider2D[] neighborColliders = Physics2D.OverlapBoxAll(gameObject.transform.position, (transform.localScale / 2), 0, m_LayerMask);

        // must check all colliders for neighboring destructible blocks
        int i = 0;
        Debug.Log("Colliders nearby: " + neighborColliders.Length);

        List<GameObject> absoluteNeighbors = new List<GameObject>();

        while (i < neighborColliders.Length)
        {
            if (neighborColliders[i].gameObject.CompareTag("Destructible") &&
                neighborColliders[i].gameObject.GetComponent<Destructible>() != null)
            {
                GameObject neighbor = neighborColliders[i].gameObject;

                if (!destroyableObjects.Contains(neighbor))  // Gameobject must be marked for destruction
                {
                    destroyableObjects.Add(neighbor);
                    absoluteNeighbors.Add(neighbor);
                    neighbor.GetComponent<Collider2D>().enabled = false;
                }
            }

            i++;
        }

        foreach (GameObject absNeighbor in absoluteNeighbors)
        {
            absNeighbor.GetComponent<Destructible>().CheckNeighbors(ref destroyableObjects);
        }

        absoluteNeighbors = null;
    }
}
