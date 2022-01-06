using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int _width = 10;
    private int _height = 20;


    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private Transform mainCamTransform;
    public static Dictionary<Vector2, Tile> _tiles;


    public bool findAvailableCell = false;

    void Awake()
    {
        mainCamTransform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
        GenerateGrid();
    }

    private void Update()
    {
        if (findAvailableCell)
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (GetTileAtPosition(new Vector2(i, j))._isEmpty)
                    {
                        GetTileAtPosition(new Vector2(i, j)).name = "doru";
                    }
                    else
                    {
                        GetTileAtPosition(new Vector2(i, j)).name = "yanlis";
                    }
                }
            }
        }
    }

    void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                var spawnTile = Instantiate(_tilePrefab, new Vector3(i, j), Quaternion.identity);
                spawnTile.transform.parent = gameObject.transform;
                spawnTile.name = i.ToString() + " " + j.ToString();

                _tiles[new Vector2(i, j)] = spawnTile;
            }
        }
    }

    public static Tile GetTileAtPosition(Vector2 pos)
    {
        if (_tiles.TryGetValue(pos, out Tile tile))
        {
            return tile;
        }
        return null;
    }

}
