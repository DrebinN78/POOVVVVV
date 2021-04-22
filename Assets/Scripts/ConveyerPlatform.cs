using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerPlatform : MonoBehaviour, IEntity
{
    public bool movingRight = true;
    public float movementSpeed = 1.0f;
    private Vector2 dir;

    private void Awake()
    {
        if (movingRight)
            dir = Vector2.right;
        else
            dir = Vector2.left;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().TapisRoulant(dir.x * movementSpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().TapisRoulant(0);
        }
    }
}
