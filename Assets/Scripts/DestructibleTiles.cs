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

public class DestructibleTiles : MonoBehaviour
{
    private Tilemap destructibleTilemap;

    //
    // Thanks to user derHugo on Stack Overflow for tile neighbor position confirmation:
    // https://stackoverflow.com/questions/69368479/how-to-get-surrounding-tiles-in-unity
    //
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

    void Start()
    {
        destructibleTilemap = GetComponent<Tilemap>();
        Debug.Log(destructibleTilemap.size);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerElectricity"))
        {
            Vector3 hitPos = Vector3.zero;
            Debug.Log("Collision");

            // Get first collision contact; Since destructible
            // gates will be fairly apart, there's
            // no worry of the player's explosion hitting
            // multiple gates in the same frame
            /*ContactPoint2D hit = collision.contacts[0];
            hitPos.x = hit.point.x - 0.01f * hit.normal.x;
            hitPos.y = hit.point.y - 0.01f * hit.normal.y;

            // get neighbors of destroyed tile
            List<Vector3Int> destroyableTiles = new List<Vector3Int>();

            Vector3Int firstTileCell = destructibleTilemap.WorldToCell(hitPos);

            destroyableTiles.Add(firstTileCell);

            // Get all neigboring tiles and set them to null
            int i = 0;
            while (destroyableTiles.Count > 0 && i < 10)
            {
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
                    }
                }
                i++;
            }*/
        }
    }
}
