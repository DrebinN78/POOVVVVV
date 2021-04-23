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

    public void InitEntity(int p1, int p2, int p3, int p4, int p5, int p6)
    {

    }
}
