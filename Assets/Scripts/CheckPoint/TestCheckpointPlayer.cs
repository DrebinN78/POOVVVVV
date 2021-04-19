using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCheckpointPlayer : MonoBehaviour
{

    public bool kill;

    // Update is called once per frame
    void Update()
    {
        if (kill)
        {
            Vector2 pos = GameManager.Instance.Respawn();
            transform.position = pos;
            kill = false;
        }
    }
}
