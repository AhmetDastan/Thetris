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

    public void FillTheGridByBlock(GameObject gameObject)
    {
        int roundedX, roundedY;

        foreach (Transform child in gameObject.transform)
        {
            roundedX = Mathf.RoundToInt(child.transform.position.x);
            roundedY = Mathf.RoundToInt(child.transform.position.y);

            _tiles[new Vector2(roundedX, roundedY)]._isEmpty = false;
            _tiles[new Vector2(roundedX, roundedY)].block = child.gameObject;
        }
    }

    public void ClearLine()
    {
        int totalTranslation = 0, lastDestroyedLine = 0;
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                if (GetTileAtPosition(new Vector2(j, i)) == null || GetTileAtPosition(new Vector2(j, i))._isEmpty)
                {
                    break;
                }
                else
                {
                    if ((j+1) == _width)
                    {
                        for(int k = 0; k < 10; k++)
                        {
                            GetTileAtPosition(new Vector2(k, i)).name = "silinmeli";
                            Destroy(GetTileAtPosition(new Vector2(k, i)).block);
                            GetTileAtPosition(new Vector2(k, i))._isEmpty = true;
                            GetTileAtPosition(new Vector2(k, i)).block = null;
                        }
                        totalTranslation++;
                        lastDestroyedLine = i;

                    }
                    continue;
                }
            }
        }
        if(totalTranslation > 0) RemainBlockTranslational(lastDestroyedLine, totalTranslation);
    }

    void RemainBlockTranslational(int lineY, int howManyTranslation)
    {   
        for (int i = (lineY+1); i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                for (int k = 0; k < howManyTranslation; k++)
                {

                    if (_tiles[new Vector2(j, (i - k))].block == null) 
                        continue;

                    _tiles[new Vector2(j, i-k)].block.gameObject.transform.position -= new Vector3(0, 1, 0);
                    
                    _tiles[new Vector2(j, (i - 1-k))].block = _tiles[new Vector2(j, i-k)].block;
                    _tiles[new Vector2(j, i - k)].block = null;

                    _tiles[new Vector2(j, i - k)]._isEmpty = true;
                    _tiles[new Vector2(j, (i -1-k))]._isEmpty = false;

                }
            }
        }
    }
}
