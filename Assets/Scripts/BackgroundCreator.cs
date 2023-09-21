using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundCreator : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase waterTiles;

    private void Start()
    {
        for (int x = 0; x < GridContainter.Instance.gridSize.x; x++)
        {
            for (int y = 0; y < GridContainter.Instance.gridSize.y; y++)
            {
                //Create the background
                tilemap.SetTile(new Vector3Int(x, y), waterTiles);
            }
        }
    }
}
