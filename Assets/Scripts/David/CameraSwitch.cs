using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    //Player Infos
    [SerializeField]
    private GameObject player;
    private Vector2 screenPos;

    //Camera Infos
    private Camera camera;
    //Camera Width & Height in Units
    private float cameraWidth, cameraHeight;


    void Awake()
    {
        camera = GetComponent<Camera>();

        cameraHeight = camera.orthographicSize * 2;
        cameraWidth = cameraHeight * camera.aspect;
    }

    private void Start()
    {
        
    }

    void Update()
    {
        screenPos = camera.WorldToScreenPoint(player.transform.position);

        if (screenPos.x < 0)
        {
            Debug.Log("Switch Camera Left");
            transform.position = new Vector2(transform.position.x - cameraWidth, transform.position.y);
        }

        if (screenPos.x > Screen.width)
        {
            Debug.Log("Switch Camera Right");
            transform.position = new Vector2(transform.position.x + cameraWidth, transform.position.y);
        }

        if(screenPos.y < 0)
        {
            Debug.Log("Switch Camera Down");
            transform.position = new Vector2(transform.position.x, transform.position.y - cameraHeight);
        }

        if(screenPos.y > Screen.height)
        {
            Debug.Log("Switch Camera Up");
            transform.position = new Vector2(transform.position.x, transform.position.y + cameraHeight);
        }

    }

    void MoveCameraX()
    {


    }
}
