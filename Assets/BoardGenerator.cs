using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public int width = 8;
    public int height = 8;

    void Start()
    {
        GenerateBoard();
    }

    void GenerateBoard()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Vector2 pos = new Vector2(x, y);
                var tileObj = Instantiate(tilePrefab, pos, Quaternion.identity, transform);

                Tile tile = tileObj.AddComponent<Tile>();
                tile.x = x;
                tile.y = y;
            }
        }
    }
}
