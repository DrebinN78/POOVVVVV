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
            if (x >= mapSize.x || y >= mapSize.y)
                continue;
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
        GameObject gameObject = null;
        if (entity.T == 1.ToString())
        {
            if (data.EnemyPrefab.Length > int.Parse(entity.P1))
                gameObject = data.EnemyPrefab[int.Parse(entity.P1)];
        }
        else
        {
            if(data.Prefab.Length> int.Parse(entity.T))
                gameObject = data.Prefab[int.Parse(entity.T)];
        }

        if(gameObject==null)
            return;
        GameObject instance = Instantiate(gameObject, parent.transform);
        Vector3 vector = new Vector3(entity.localX, -entity.localY);
        instance.transform.localPosition = vector;
        instance.GetComponent<IEntity>().InitEntity(int.Parse(entity.P1), int.Parse(entity.P2), int.Parse(entity.P3), int.Parse(entity.P4), int.Parse(entity.P5), int.Parse(entity.P6));
        gameObjects.Add(instance);
    }
}
