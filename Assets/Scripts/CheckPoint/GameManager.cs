using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance
    {
        get => _instance;
    }

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    private CheckPoint _currentCheckpoint;

    public void SetCheckPoint(CheckPoint point)
    {
        if(_currentCheckpoint != null)
            _currentCheckpoint.DisableCheckpoint();
        _currentCheckpoint = point;
    }

    public Vector2 Respawn()
    {
        return _currentCheckpoint.transform.position;
    }

}
