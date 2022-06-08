using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrailAbility : MonoBehaviour
{
    [SerializeField] private Tilemap trailTilemap;
    [SerializeField] private Tile trailTile;
    [SerializeField] private float trailTimer = 0.5f;
    private float elapsedTime = 0;

    void Update()
    {
        if (elapsedTime < trailTimer) {
            elapsedTime += Time.deltaTime;
        } else {
            elapsedTime = 0f;
            LeaveTrail();
        }
    }

    private void LeaveTrail()
    {
        Vector3Int gridPosition = trailTilemap.WorldToCell(transform.position);
        trailTilemap.SetTile(gridPosition, trailTile);
    }
}
