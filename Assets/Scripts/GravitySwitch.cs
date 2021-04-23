using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GravitySwitch : MonoBehaviour, IEntity
{
    private PlayerMovement player;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        rb = player.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.ySpeed = 0;
            player.ChangeGravity();
        }
    }

    public void InitEntity(int p1, int p2, int p3, int p4, int p5, int p6)
    {

    }
}
