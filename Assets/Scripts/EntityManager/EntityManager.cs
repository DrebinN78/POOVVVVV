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
    public List<Entity> entities = new List<Entity>();
    public EntityData data;

    private List<Entity>[,] panes;
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
        panes = new List<Entity>[mapSize.x, mapSize.y];
        foreach (var entity in entities)
        {
            int x = Mathf.FloorToInt(entity.pos.x / paneSixe.x);
            int y = Mathf.FloorToInt(entity.pos.y / paneSixe.y);
            if (panes[x, y] == null)
                panes[x, y] = new List<Entity>();
            panes[x,y].Add(entity);
            entity.pos.x %= paneSixe.x;
            entity.pos.y %= paneSixe.y;
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

    private void LoadEntity(Entity entity)
    {
        GameObject instance = Instantiate(data.Prefab[entity.type], parent.transform);
        Vector3 vector = new Vector3(entity.pos.x, -entity.pos.y);
        instance.transform.localPosition = vector;
        gameObjects.Add(instance);
    }
}
