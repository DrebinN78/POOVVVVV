using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour, IEntity
{
    private bool _isActive;
    private SpriteRenderer _renderer;

    public Color disableColor;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!_isActive)
        {
            GameManager.Instance.SetCheckPoint(this);
            _isActive = true;
            _renderer.color = Color.white;
        }
    }

    public void DisableCheckpoint()
    {
        _isActive = false;
        _renderer.color = disableColor;
    } 

}
