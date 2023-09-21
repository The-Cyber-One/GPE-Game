using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    GameObject backgroundSquare;

    [SerializeField]
    List<Sprite> possibleTiles;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < GridContainter.Instance.gridSize.x; x++)
        {
            for (int y = 0; y < GridContainter.Instance.gridSize.y; y++)
            {
                //Create the background
                GameObject newBackground = Instantiate(backgroundSquare);
                newBackground.transform.SetParent(this.transform);
                Sprite sprite = possibleTiles[Random.Range(0, possibleTiles.Count - 1)];
                newBackground.GetComponent<SpriteRenderer>().sprite = sprite;
                newBackground.transform.localScale = GridContainter.Instance.Grid.cellSize;
                newBackground.transform.position = (new Vector2(x, y) + GridContainter.Instance._gridMin) * GridContainter.Instance.Grid.cellSize + new Vector2(GridContainter.Instance.Grid.transform.position.x, GridContainter.Instance.Grid.transform.position.y);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
