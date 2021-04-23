using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class LevelScriptableObject : ScriptableObject
{
    public int levelID;
    [Tooltip("Case Sensitive !\nthe parse get this value to get the right xml file and set these object variables")]
    public string levelName;
    public MapData mapData;
}
