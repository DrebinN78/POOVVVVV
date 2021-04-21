using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
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
            player.ySpeed = player.ySpeed / 2;
            player.ChangeGravity();
        }
    }
}
