using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    private bool firstDir;

    [SerializeField]
    private bool moveVertical;
    [SerializeField]
    private float Speed;

    // Update is called once per frame
    void Update()
    {
        if (moveVertical) VerticalMovement();
        else HorizontalMovement();
    }

    private void VerticalMovement()
    {
        transform.Translate(0, firstDir ? Speed : -Speed, 0);
    }

    private void HorizontalMovement()
    {
        transform.Translate(firstDir ? Speed : -Speed, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Decor") || collision.gameObject.CompareTag("Ennemy"))
            firstDir = !firstDir;
    }
}
