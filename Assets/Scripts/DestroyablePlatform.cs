using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyablePlatform : MonoBehaviour, IEntity
{
    public float destroySpeed = 1.0f;
    private Animation _anim;
    private bool gotTriggered = false;

    private void Awake()
    {
        _anim = GetComponent<Animation>();
        _anim["DestroyPlatform"].speed = destroySpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player") && !gotTriggered)
        {
            _anim.Play();
            gotTriggered = true;
        }
    }
}
