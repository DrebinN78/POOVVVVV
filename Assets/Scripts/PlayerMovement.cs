using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool debugMode = false;

    [SerializeField] float xSpeed = 3f;
    private float xEffect = 0f;
    private int dirX = 1; // 1 = right, -1 = left
    
    [SerializeField] bool isOnGround = false;

    [SerializeField] float gravity = -5f;
    private float ySpeed = 0f;

    //Ground detection
    //par raycast
    [SerializeField] float rayLenght = 1f;
    private RaycastHit hit;

    //par boxCollider
    private BoxCollider2D[] groundCheckColliders;
    [SerializeField] Transform GroundCheckPoint;

    private void Start()
    {
        groundCheckColliders = FindObjectsOfType<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        UpdateGroundCheck();
        UpdateGravity();
        UpdatePosition();
    }

    void UpdateInput()
    {
        dirX = 0;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            dirX = 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Q))
        {
            dirX = -1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            ChangeGravity();
            isOnGround = false;
        }
    }

    void UpdatePosition()
    {
        Vector3 newPos = transform.position;
        newPos.x += xSpeed * Time.deltaTime * dirX + xEffect * Time.deltaTime;
        newPos.y += ySpeed * Time.deltaTime;
        transform.position = newPos;
    }

    void UpdateGravity()
    {
        if (isOnGround)
        {
            ySpeed = 0f;
        }
        else
        {
            ySpeed = gravity;
        }
    }

    void UpdateGroundCheck()
    {
        //raycast vers le bas
        //Ray ray = new Ray(transform.position, -transform.up);
        //if (debugMode)
        //{
        //    Debug.DrawRay(ray.origin, ray.direction * rayLenght, Color.red);
        //}
        //if (Physics.Raycast(ray.origin, ray.direction, out hit, rayLenght))
        //{
        //    if (hit.transform.gameObject.CompareTag("Ground"))
        //    {
        //        ySpeed = 0f;
        //    }
        //}

        //
        BoxCollider2D colliderFound = null;
        foreach (BoxCollider2D collider in groundCheckColliders)
        {
            if (IsPointInsideBox(GroundCheckPoint.position, collider))
            {
                colliderFound = collider;
                break;
            }
        }

        if (colliderFound != null)
        {
            isOnGround = true;
            Vector2 position = transform.position;
            float offsetY = GroundCheckPoint.position.y - transform.position.y;
            position.y = colliderFound.bounds.max.y - offsetY;
            transform.position = position;
        }
        else
        {
            isOnGround = false;
        }
    }

    void ChangeGravity()
    {
        gravity *= -1;
    }

    public void TapisRoulant(float xEffectSpeed)
    {
        xEffect = xEffectSpeed;
    }

    private bool IsPointInsideBox(Vector2 point, BoxCollider2D boxCollider)
    {
        if (point.x < boxCollider.bounds.min.x) return false;
        if (point.y < boxCollider.bounds.min.y) return false;
        if (point.x > boxCollider.bounds.max.x) return false;
        if (point.y > boxCollider.bounds.max.y) return false;
        return true;
    }
}
