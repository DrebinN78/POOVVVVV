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
    [HideInInspector] public float ySpeed = 0f;

    [Header("Ground and Wall Checks")]
    [SerializeField] Transform GroundCheckPoint;
    [SerializeField] Transform RightWallCheckPoint;
    [SerializeField] Transform LeftWallCheckPoint;
    private List<BoxCollider2D> groundList = new List<BoxCollider2D>();

    [Header("Animation")]
    [SerializeField] Animator animator;

    [Header("MapBound - Gamemanager Should Give Those")]
    [SerializeField] float minXMap = 0;
    [SerializeField] float maxXMap = 40;
    [SerializeField] float minYMap = -20;
    [SerializeField] float maxYMap = 20;

    private void Start()
    {
        SetAllGroundColliders(TilesManager.Instance.solidTiles);
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
        MapChangingPosition();
    }

    void UpdateInput()
    {
        dirX = 0;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            dirX = 1;
            animator.SetBool("GoRight", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
        {
            dirX = -1;
            animator.SetBool("GoRight", false);
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
        foreach (BoxCollider2D collider in groundList)
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
            transform.position = position;
        }
        else
        {
            isOnGround = false;
        }
    }

    public void ChangeGravity()
    {
        gravity *= -1;
        animator.SetBool("InverseGravity", gravity == 1);
        
        Vector3 newLocalPos = GroundCheckPoint.localPosition;
        newLocalPos.y *= -1;
        GroundCheckPoint.localPosition = newLocalPos;
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
        foreach (BoxCollider2D collider in groundList)
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
            position.x = colliderFound.bounds.min.x - offsetX;
            transform.position = position;

            if (dirX == 1) dirX = 0;
        }
    }

    private void UpdateLeftWallCheck()
    {
        BoxCollider2D colliderFound = null;
        foreach (BoxCollider2D collider in groundList)
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
            position.x = colliderFound.bounds.max.x - offsetX;
            transform.position = position;

            if (dirX == -1) dirX = 0;
        }
    }

    public void SetAllGroundColliders(List<BoxCollider2D> listBoxCollider)
    {
        groundList.Clear();
        groundList = listBoxCollider;
    }

    private void OnGUI()
    {
        if (!debugMode) return;
        
        GUILayout.BeginVertical();
        GUILayout.Label("xEffect = " + xEffect);
        GUILayout.Label("isOnGround = " + isOnGround);
        GUILayout.Label("gravity = " + gravity);
        GUILayout.Label("ySpeed = " + ySpeed);
        GUILayout.Label("groundList" + groundList);
        GUILayout.EndVertical();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ennemy"))
        {
            transform.position = GameManager.Instance.Respawn();
        }
    }

    void MapChangingPosition()
    {
        Vector3 newPos = transform.position;
        if (transform.position.x > maxXMap)
        {
            newPos.x = minXMap + 1;
            TilesManager.Instance.LoadNewLevel(TilesManager.Instance.GetRightLevel());
        }
        else if (transform.position.x < minXMap)
        {
            newPos.x = maxXMap -1;
            TilesManager.Instance.LoadNewLevel(TilesManager.Instance.GetLeftLevel());
        }

        if (transform.position.y > maxYMap)
        {
            newPos.y = minYMap;
            TilesManager.Instance.LoadNewLevel(TilesManager.Instance.GetUpLevel());
        }
        else if (transform.position.y < minYMap)
        {
            newPos.y = maxYMap;
            TilesManager.Instance.LoadNewLevel(TilesManager.Instance.GetDownLevel());
        }

        transform.position = newPos;
    }
}
