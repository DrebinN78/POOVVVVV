using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entity
{
    public Vector2Int pos;
    public int type;

}

public class EntityManager : MonoBehaviour
{
    public EntityData data;
    public LevelScriptableObject levelData;

    private List<Edentity>[,] panes;
    private List<GameObject> gameObjects = new List<GameObject>();
    private GameObject parent = null;

    #region Singleton

    private static EntityManager _instance = null;

    public static EntityManager Instance
    {
        get => _instance;
    }

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
    }

    #endregion

    public void Init(Vector2Int mapSize,Vector2Int paneSixe)
    {
        panes = new List<Edentity>[mapSize.x, mapSize.y];
        foreach (var entity in levelData.mapData.Data.EdEntities.Edentity)
        {
            int x = Mathf.FloorToInt(entity.X / paneSixe.x);
            int y = Mathf.FloorToInt(entity.Y / paneSixe.y);
            if (panes[x, y] == null)
                panes[x, y] = new List<Edentity>();
            panes[x,y].Add(entity);
            entity.localX = entity.X % paneSixe.x;
            entity.localY = entity.Y % paneSixe.y;
        }

        parent = new GameObject();
        parent.name = "EntityContainer";
        parent.transform.position = new Vector3(-paneSixe.x / 2, paneSixe.y / 2);
    }

    public void loadPane(Vector2Int panePosition)
    {
        foreach (var gameObject in gameObjects)
        {
            Destroy(gameObject);
        }
        if(panes[panePosition.x, panePosition.y]==null)
            return;
        foreach (var entity in panes[panePosition.x,panePosition.y])
        {
            LoadEntity(entity);
        }
    }

    private void LoadEntity(Edentity entity)
    {
        GameObject gameObject;
        //if (entity.type == 1)
        //    gameObject = data.EnemyPrefab[entity.p1];
        //else
            gameObject = data.Prefab[int.Parse(entity.T)];
        GameObject instance = Instantiate(gameObject, parent.transform);
        Vector3 vector = new Vector3(entity.localX, -entity.localY);
        instance.transform.localPosition = vector;
        gameObjects.Add(instance);
    }
}
