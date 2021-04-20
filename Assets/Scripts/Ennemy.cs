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
        EnnemyMovement();
    }

    private void EnnemyMovement()
    {
        Vector3 newPos = transform.position;

        if (moveVertical)
            newPos.y += (firstDir ? Speed : -Speed) * Time.deltaTime; //Deplacement axe vertical
        else
            newPos.x += (firstDir ? Speed : -Speed) * Time.deltaTime; //Déplacement axe horizontal

        transform.position = newPos;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        firstDir = !firstDir; //Change de direction s'il touche quelque chose/n'importe quoi
    }
}
