using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SwapTileSets : MonoBehaviour
{
    [SerializeField] private TileBase currentGroundTile;
    [SerializeField] private TileBase currentWallTile;
    [SerializeField] private TileBase[] groundTiles;
    [SerializeField] private TileBase[] wallTiles;
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap wallTilemap;

    private void Start()
    {
        int randomGround = Random.Range(0, groundTiles.Length -1);
        groundTilemap.SwapTile(currentGroundTile, groundTiles[randomGround]);
        currentGroundTile = groundTiles[randomGround];

        int randomWall = Random.Range(0, wallTiles.Length -1);
        wallTilemap.SwapTile(currentWallTile, wallTiles[randomWall]);
        currentWallTile = wallTiles[randomWall];
    }

}
