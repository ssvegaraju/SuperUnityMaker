using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapCollisions : MonoBehaviour {

    public LayerMask playerLayer;
    public float boxWidth = 1.5f;

    private BoundsInt bounds;
    private Tilemap map;

    private void Start() {
        bounds = new BoundsInt();
        if (map == null) {
            map = GetComponent<Tilemap>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (playerLayer == (playerLayer | (1 << collision.gameObject.layer))) {
            Collider2D player = collision.collider;
            Vector3Int topRight = map.WorldToCell(player.bounds.max + (Vector3.one * boxWidth));
            Vector3Int bottLeft = map.WorldToCell(player.bounds.min - (Vector3.one * boxWidth));
            bounds.SetMinMax(bottLeft + new Vector3Int(0, 0, -1), topRight + new Vector3Int(0, 0, 1));

            TileBase closestTile = null;
            Vector3Int closestPos = Vector3Int.zero;
            float distance = float.PositiveInfinity;
            for (int x = bottLeft.x; x <= topRight.x; x++) {
                for (int y = bottLeft.y; y <= topRight.y; y++) {
                    for (int z = bottLeft.z; z <= topRight.z; z++) {
                        Vector3Int pos = new Vector3Int(x, y, 0);
                        TileBase tile = map.GetTile(pos);
                        if (tile == null)
                            continue;
                        float dist = Vector3.Distance(pos, player.transform.position);
                        if (dist < distance) {
                            distance = dist;
                            closestPos = pos;
                            closestTile = tile;
                        }
                    }
                }
            }
            Debug.DrawLine(closestPos, player.transform.position, Color.magenta);
            Block block = Resources.Load("Blocks/" + closestTile.name, typeof(Block)) as Block;
            block.Activate(closestPos);
        }
    }
}
