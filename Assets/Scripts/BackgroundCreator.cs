using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundCreator : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase[] waterTiles;

    private void Start()
    {
        for (int x = 0; x < GridContainter.Instance.GridSize.x; x++)
        {
            for (int y = 0; y < GridContainter.Instance.GridSize.y; y++)
            {
                //Create the background
                Vector3Int position = new(x + GridContainter.Instance.GridMin.x, y + GridContainter.Instance.GridMin.y);
                tilemap.SetTile(position, waterTiles[Random.Range(0, waterTiles.Length)]);
            }
        }
    }
}
