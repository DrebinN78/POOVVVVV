using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    enum Direction { Right, Left, Up, Down};

    //Player Infos
    [SerializeField]
    private GameObject player;
    private Vector2 screenPos;

    //Camera Infos
    private Camera cam;
    //Camera Width & Height in Units
    private float cameraWidth, cameraHeight;


    void Awake()
    {
        cam = GetComponent<Camera>();

        cameraHeight = cam.orthographicSize * 2;
        cameraWidth = cameraHeight * cam.aspect;
    }

    private void Start()
    {
        
    }

    void Update()
    {
        screenPos = cam.WorldToScreenPoint(player.transform.position);

        CameraSwitchManager();
    }

    void CameraSwitchManager()
    {
        if (screenPos.x < 0)
        {
            Debug.Log("Switch Camera Left");
            transform.position = new Vector3(transform.position.x - cameraWidth, transform.position.y, transform.position.z);
        }

        if (screenPos.x > Screen.width)
        {
            Debug.Log("Switch Camera Right");
            transform.position = new Vector3(transform.position.x + cameraWidth, transform.position.y, transform.position.z);
        }

        if (screenPos.y < 0)
        {
            Debug.Log("Switch Camera Down");
            transform.position = new Vector3(transform.position.x, transform.position.y - cameraHeight, transform.position.z);
        }

        if (screenPos.y > Screen.height)
        {
            Debug.Log("Switch Camera Up");
            transform.position = new Vector3(transform.position.x, transform.position.y + cameraHeight, transform.position.z);
        }
    }
}
