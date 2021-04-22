using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class EntityData : ScriptableObject
{
    public GameObject[] Prefab;
    public IEntity[] EntitiesPrefabs;
}
