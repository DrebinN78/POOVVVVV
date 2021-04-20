using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] bool debugMode = false;

    [Header("X direction")]
    [SerializeField] float xSpeed = 3f;
    private float xEffect = 0f;
    private int dirX = 1; // 1 = right, -1 = left
    
    [Header("Y direction")]
    [SerializeField] bool isOnGround = false;
    [SerializeField] float maxYSpeed = 5f;
    [SerializeField] float yAcceleration = 1f;
    private int gravity = -1;
    private float ySpeed = 0f;

    [Header("Ground and Wall Checks")]
    [SerializeField] Transform GroundCheckPoint;
    [SerializeField] Transform RightWallCheckPoint;
    [SerializeField] Transform LeftWallCheckPoint;
    private BoxCollider2D[] groundCheckColliders;

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
        UpdateRightWallCheck();
        UpdateLeftWallCheck();
        UpdatePosition();
    }

    void UpdateInput()
    {
        dirX = 0;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            dirX = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
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
            ySpeed += yAcceleration * gravity * Time.deltaTime;
            ySpeed = Mathf.Clamp(ySpeed, -maxYSpeed, maxYSpeed);
        }
    }

    void UpdateGroundCheck()
    {
        BoxCollider2D colliderFound = null;
        foreach (BoxCollider2D collider in groundCheckColliders)
        {
            if (IsPointInsideBox(new Vector2(GroundCheckPoint.position.x, GroundCheckPoint.position.y), collider))
            {
                colliderFound = collider;
                break;
            }
        }

        if (colliderFound != null)
        {
            isOnGround = true;
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            float offsetY = GroundCheckPoint.position.y - transform.position.y;
            if (gravity == -1)
            {
                position.y = colliderFound.bounds.max.y - offsetY;
            }
            else
            {
                position.y = colliderFound.bounds.min.y - offsetY;
            }
            //position.y = colliderFound.bounds.max.y + offsetY * gravity;
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
        transform.Rotate(Vector3.forward, 180);
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

    private void UpdateRightWallCheck()
    {
        BoxCollider2D colliderFound = null;
        foreach (BoxCollider2D collider in groundCheckColliders)
        {
            if (IsPointInsideBox(new Vector2(RightWallCheckPoint.position.x, RightWallCheckPoint.position.y), collider))
            {
                colliderFound = collider;
                break;
            }
        }

        if (colliderFound != null)
        {
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            float offsetX = RightWallCheckPoint.position.x - transform.position.x;
            if (gravity == -1)
            {
                position.x = colliderFound.bounds.min.x - offsetX;
                if (dirX == 1) dirX = 0;
            }
            else
            {
                position.x = colliderFound.bounds.max.x - offsetX;
                if (dirX == -1) dirX = 0;
            }
            //position.y = colliderFound.bounds.max.y + offsetY * gravity;
            transform.position = position;
        }
    }

    private void UpdateLeftWallCheck()
    {
        BoxCollider2D colliderFound = null;
        foreach (BoxCollider2D collider in groundCheckColliders)
        {
            if (IsPointInsideBox(new Vector2(LeftWallCheckPoint.position.x, LeftWallCheckPoint.position.y), collider))
            {
                colliderFound = collider;
                break;
            }
        }

        if (colliderFound != null)
        {
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            float offsetX = LeftWallCheckPoint.position.x - transform.position.x;
            if (gravity == -1)
            {
                position.x = colliderFound.bounds.max.x - offsetX;
                if (dirX == -1) dirX = 0;
            }
            else
            {
                position.x = colliderFound.bounds.min.x - offsetX;
                if (dirX == 1) dirX = 0;
            }
            transform.position = position;
        }
    }

    public void SetAllGroundColliders(BoxCollider2D[] listBoxCollider)
    {
        groundCheckColliders = listBoxCollider;
    }

    private void OnGUI()
    {
        if (!debugMode) return;
        
        GUILayout.BeginVertical();
        GUILayout.Label("xEffect = " + xEffect);
        GUILayout.Label("isOnGround = " + isOnGround);
        GUILayout.Label("gravity = " + gravity);
        GUILayout.Label("ySpeed = " + ySpeed);
        GUILayout.EndVertical();
    }
}
