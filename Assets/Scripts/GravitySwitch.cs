using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    public GameObject player; //change to type "Player"
    private Rigidbody2D rb;

    void Start()
    {
        //player = GetComponent<Player>();
        rb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Change input mode for no errors
       /* if (Input.GetKeyDown(KeyCode.Space) && player.isOnGround)
        {
            SwitchGravity();
        }*/
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("SwitchString"))
        {
            SwitchGravity();
        }
    }

    void SwitchGravity()
    {
        if (rb.gravityScale == 1)
        {
            rb.gravityScale = -1;

        }
        else
        {
            rb.gravityScale = 1;
        }
    }
}
