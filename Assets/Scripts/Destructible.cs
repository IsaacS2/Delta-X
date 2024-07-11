//
// Thanks to Muddy Wolf at https://www.youtube.com/watch?v=94KWSZBSxIA&t=335s for code tutorial
//
// DestructibleTiles
//
// Keeps track of collisions with player's electric explosion attack to destroy correlating tiles
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Collider2D))]
public class Destructible : MonoBehaviour
{
    //private Tilemap destructibleTilemap;

    //
    // Thanks to user derHugo on Stack Overflow for tile neighbor position confirmation:
    // https://stackoverflow.com/questions/69368479/how-to-get-surrounding-tiles-in-unity
    //
    //public bool setToDestroy { get; set; }

    private readonly Vector3Int[] neighborPositions =
    {
        Vector3Int.up,
        Vector3Int.right,
        Vector3Int.down,
        Vector3Int.left,

        Vector3Int.up + Vector3Int.right,
        Vector3Int.up + Vector3Int.left,
        Vector3Int.down + Vector3Int.right,
        Vector3Int.down + Vector3Int.left
    };

    public LayerMask m_LayerMask;

    void Start()
    {
        //destructibleTilemap = transform.parent.GetComponent<Tilemap>();
        //Debug.Log(destructibleTilemap.size);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerElectricity"))
        {
            Vector3 hitPos = transform.position;
            Debug.Log("Collision");

            // Get first collision contact; Since destructible
            // gates will be fairly apart, there's
            // no worry of the player's explosion hitting
            // multiple gates in the same frame
            /*ContactPoint2D hit = collision.contacts[0];
            hitPos.x = hit.point.x - 0.01f * hit.normal.x;
            hitPos.y = hit.point.y - 0.01f * hit.normal.y;*/

            // get neighbors of destroyed tile gameObject
            //List<Vector3> destroyableTiles = new List<Vector3Int>();

            //Vector3Int firstTileCell = destructibleTilemap.WorldToCell(hitPos);

            //destroyableTiles.Add(firstTileCell);

            // Get all neigboring tiles and set them to null
            /*int i = 0;
            while (destroyableTiles.Count > 0 && i < 30)
            {
                Debug.Log("Destroyed Tile");
                Vector3Int tileCell = destroyableTiles[0];
                destructibleTilemap.SetTile(destructibleTilemap.WorldToCell(tileCell), null);
                destroyableTiles.RemoveAt(0);
                
                foreach (Vector3Int neighborPos in neighborPositions)
                {
                    Vector3Int absoluteNeighborPos = tileCell + neighborPos;

                    //
                    // non-null tile in neighbor position and is not in list (or queue) of destroyable tiles
                    //
                    if (!destroyableTiles.Contains(absoluteNeighborPos) 
                        && destructibleTilemap.HasTile(absoluteNeighborPos))
                    {
                        destroyableTiles.Add(absoluteNeighborPos);
                        destroyableTiles.Add(absoluteNeighborPos);
                    }
                    else
                    {
                        Debug.Log("Contained in list: " + destroyableTiles.Contains(absoluteNeighborPos));
                        Debug.Log("Selectable tile: " + destructibleTilemap.HasTile(absoluteNeighborPos));
                    }
                }
                i++;
            }*/

            //Collider[] neighborColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2);
            List<GameObject> destroyableObjects = new List<GameObject>();

            // disable collider so neighbors won't register this object when finding other neighbors
            GetComponent<Collider2D>().enabled = false;

            this.CheckNeighbors(ref destroyableObjects);

            foreach (GameObject destroyableObject in destroyableObjects)
            {
                Destroy(destroyableObject);
            }

            // finally, destroy this object
            destroyableObjects = null;
            Destroy(gameObject);
            Debug.Log("Just got destroyed :)");
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

                if (!destroyableObjects.Contains(neighbor))
                {  // Gameobject must be marked for destruction
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
