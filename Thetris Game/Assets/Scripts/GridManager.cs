using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int _width = 10;
    private int _height = 20;


    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private GameObject ghostPiecePrefab;

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

                GameObject temp = Instantiate(ghostPiecePrefab, new Vector3(i, j), Quaternion.identity);
                temp.transform.parent = gameObject.transform;

                _tiles[new Vector2(i, j)].ghostBlock = temp;


                _tiles[new Vector2(i, j)].ghostBlock.SetActive(false) ;
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
        int lastDestroyedLine = 0;
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
                            Destroy(GetTileAtPosition(new Vector2(k, i)).block);
                            GetTileAtPosition(new Vector2(k, i))._isEmpty = true;
                            GetTileAtPosition(new Vector2(k, i)).block = null;
                        }
                        lastDestroyedLine = i;
                        RemainBlockTranslational(lastDestroyedLine);
                        i--;
                    }
                    continue;
                }
            }
        }
    }

    void RemainBlockTranslational(int lineY)
    {
        for (int i = (lineY+1); i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                    if (_tiles[new Vector2(j, (i))].block == null) 
                        continue;

                    _tiles[new Vector2(j, i)].block.gameObject.transform.position -= new Vector3(0, 1, 0);
                    
                    _tiles[new Vector2(j, (i - 1))].block = _tiles[new Vector2(j, i)].block;
                    _tiles[new Vector2(j, i)].block = null;

                    _tiles[new Vector2(j, i)]._isEmpty = true;
                    _tiles[new Vector2(j, (i -1))]._isEmpty = false;

            }
        }
    }

  /*  public void GosthPiece(GameObject gameObject)
    {
        int roundedX, roundedY;
        Tile tile;
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                _tiles[new Vector2(i, (j))].ghostBlock.SetActive(false);
            }
        }

        //butun childlarin oturabilecegi en alt satiri bul ve o childlarin oldugu yeri beyaz bula
        foreach (Transform child in gameObject.transform)
        {
            roundedX = Mathf.RoundToInt(child.transform.position.x);
            roundedY = Mathf.RoundToInt(child.transform.position.y);

            tile = GridManager.GetTileAtPosition(new Vector2(roundedX, roundedY));

            if (tile == null || !tile._isEmpty)
            {
                //iste burasi
            }
            else
            {

            }
        }
    }*/
}


/*
 * foreach (Transform child in gameObject.transform)
        {
            roundedX = Mathf.RoundToInt(child.transform.position.x);
            roundedY = Mathf.RoundToInt(child.transform.position.y);

            for (int j = roundedY; j >= 0; j--)
            {

                if (j == 0 && GetTileAtPosition(new Vector2(roundedX, j))._isEmpty == true)
                {
                    _tiles[new Vector2(roundedX, (j))].ghostBlock.SetActive(true);
                    break;
                }
                else if (GetTileAtPosition(new Vector2(roundedX, j))._isEmpty == false)
                {
                    Debug.Log("burda ne var x ve y " + roundedX + " " + (j + 1));
                    _tiles[new Vector2(roundedX, (j+1))].ghostBlock.SetActive(true);
                    break;
                }
                else
                {
                    continue;
                }
            }
        }*/
