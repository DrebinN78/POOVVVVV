using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    #region Singleton

    private static TilesManager _instance;

    public static TilesManager Instance
    {
        get => _instance;
    }

    #endregion

    public Vector2Int levelSize = new Vector2Int(40, 30);
    private Vector2Int mapSize;
    public Vector2Int startingLevelPositionInMap = new Vector2Int(1, 1);

    public float gapBetweenTiles = 1f;

    public LevelScriptableObject Level;
    private int[] map;

    [Header("Tiles")]
    public Sprite[] tiles;
    public Sprite[] tiles2;
    public Sprite[] tiles3;

    public Material unlitMaterial;

    [HideInInspector]
    public List<GameObject> tilesMap = new List<GameObject>();
    [HideInInspector]
    public Vector2Int ActualLevel;
    [HideInInspector]
    public List<BoxCollider2D> solidTiles = new List<BoxCollider2D>();

    private GameObject TilesMap;
    private Vector3 startGenerationPosition;
    private int levelLength;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(this.gameObject);
        }

        TilesMap = new GameObject("TilesMap");
        startGenerationPosition = new Vector3(-levelSize.x / 2f + gapBetweenTiles / 2, levelSize.y / 2f - gapBetweenTiles / 2, 0);
        TilesMap.transform.position = startGenerationPosition;

        levelLength = levelSize.x * levelSize.y;
    }

    void Start()
    {
        // Get Datas
        string[] mapString = Level.mapData.Data.Contents.Remove(Level.mapData.Data.Contents.Length - 1).Split(',');

        map = new int[mapString.Length];
        map = System.Array.ConvertAll<string, int>(mapString, int.Parse);
        mapSize = new Vector2Int(Level.mapData.Data.Mapwidth, Level.mapData.Data.Mapheight);

        // Init Entity Manager
        EntityManager.Instance.Init(mapSize, levelSize);
        EntityManager.Instance.loadPane(new Vector2Int(startingLevelPositionInMap.x, startingLevelPositionInMap.y));

        for (int i = 0; i < levelLength; i++)
        {
            int x = i % levelSize.x;
            int y = Mathf.FloorToInt(i / levelSize.x);

            GameObject newTile = new GameObject("Tile[" + x + "-" + y + "]", typeof(SpriteRenderer));
            newTile.transform.parent = TilesMap.transform;

            // Init new tile
            newTile.transform.localPosition = new Vector3(x * gapBetweenTiles, -y * gapBetweenTiles, 0);
            SpriteRenderer newSpriteRenderer = newTile.GetComponent<SpriteRenderer>();

            int tilePositionInMap = GetTilePositionInMap(new Vector2Int(x, y), startingLevelPositionInMap);
            if (map != null && tilePositionInMap < map.Length && map[tilePositionInMap] < tiles.Length)
            {
                newSpriteRenderer.sprite = tiles[map[tilePositionInMap]];
                newSpriteRenderer.material = unlitMaterial;
                newTile.AddComponent<BoxCollider2D>();

                // Add new tile in map list
                tilesMap.Add(newTile);

                if (map[tilePositionInMap] >= 80 && map[tilePositionInMap] <= 678)
                {
                    solidTiles.Add(tilesMap[i].GetComponent<BoxCollider2D>());
                }
                else if ((map[tilePositionInMap] >= 6 && map[tilePositionInMap] <= 9)
                    || (map[tilePositionInMap] >= 49 && map[tilePositionInMap] <= 50)
                    || (map[tilePositionInMap] >= 1080 && map[tilePositionInMap] <= 1085)
                    || (map[tilePositionInMap] >= 1120 && map[tilePositionInMap] <= 1125)
                    || (map[tilePositionInMap] >= 1160 && map[tilePositionInMap] <= 1165))
                {
                    tilesMap[i].GetComponent<BoxCollider2D>().isTrigger = true;
                    tilesMap[i].tag = "Ennemy";
                }
            }

        }
        // Set actual level
        if (tilesMap.Count == levelLength)
            ActualLevel = new Vector2Int(startingLevelPositionInMap.x, startingLevelPositionInMap.y);
    }

    public int GetTilePositionInMap(Vector2Int tilePositionInLevel, Vector2Int levelPositionInMap)
    {
        int xdecal = tilePositionInLevel.x + levelSize.x * levelPositionInMap.x;
        int ydecal = levelSize.x * mapSize.x * levelPositionInMap.y * levelSize.y + levelSize.x * mapSize.x * tilePositionInLevel.y;
        return (xdecal + ydecal);
    }

    public void LoadNewLevel(Vector2Int levelPositionInMap)
    {
        if (ActualLevel == null)
            return;

        solidTiles.Clear();

        // Set tiles sprite
        for (int i = 0; i < levelLength; i++)
        {
            int x = i % levelSize.x;
            int y = Mathf.FloorToInt(i / levelSize.x);

            SpriteRenderer newSpriteRenderer = tilesMap[i].GetComponent<SpriteRenderer>();

            int tilePositionInMap = GetTilePositionInMap(new Vector2Int(x, y), levelPositionInMap);
            if (map != null && tilePositionInMap < map.Length && map[tilePositionInMap] < tiles.Length)
            {
                newSpriteRenderer.sprite = tiles[map[tilePositionInMap]];
                if (map[tilePositionInMap] >= 80 && map[tilePositionInMap] <= 678)
                {
                    tilesMap[i].GetComponent<BoxCollider2D>().isTrigger = false;
                    tilesMap[i].tag = "Untagged";
                    solidTiles.Add(tilesMap[i].GetComponent<BoxCollider2D>());
                }
                else if ((map[tilePositionInMap] >= 6 && map[tilePositionInMap] <= 9)
                    || (map[tilePositionInMap] >= 49 && map[tilePositionInMap] <= 50)
                    || (map[tilePositionInMap] >= 1080 && map[tilePositionInMap] <= 1085)
                    || (map[tilePositionInMap] >= 1120 && map[tilePositionInMap] <= 1125)
                    || (map[tilePositionInMap] >= 1160 && map[tilePositionInMap] <= 1165))
                {
                    tilesMap[i].GetComponent<BoxCollider2D>().isTrigger = true;
                    tilesMap[i].tag = "Ennemy";
                }
                else
                {
                    tilesMap[i].GetComponent<BoxCollider2D>().isTrigger = false;
                    tilesMap[i].tag = "Untagged";
                }
            }
        }

        EntityManager.Instance.loadPane(levelPositionInMap);

        // Set actual level
        ActualLevel.x = levelPositionInMap.x;
        ActualLevel.y = levelPositionInMap.y;
    }

    public Vector2Int GetUpLevel()
    {
        if (ActualLevel.y == 0)
            return (new Vector2Int(ActualLevel.x, mapSize.y - 1));
        return (new Vector2Int(ActualLevel.x, ActualLevel.y - 1));
    }

    public Vector2Int GetDownLevel()
    {
        if (ActualLevel.y == mapSize.y - 1)
            return (new Vector2Int(ActualLevel.x, 0));
        return (new Vector2Int(ActualLevel.x, ActualLevel.y + 1));
    }

    public Vector2Int GetLeftLevel()
    {
        if (ActualLevel.x == 0)
            return (new Vector2Int(mapSize.x - 1, ActualLevel.y));
        return (new Vector2Int(ActualLevel.x - 1, ActualLevel.y));
    }

    public Vector2Int GetRightLevel()
    {
        if (ActualLevel.x == mapSize.x - 1)
            return (new Vector2Int(0, ActualLevel.y));
        return (new Vector2Int(ActualLevel.x + 1, ActualLevel.y));
    }
}
