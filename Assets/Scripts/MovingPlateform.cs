using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateform : MonoBehaviour, IEntity
{
    public List<Transform> pointsTransform;
    public float speed = 1f;
    public float startDelay = 0f;

    private List<Vector3> points = new List<Vector3>();
    private Vector3 point;
    private Vector3 targetPoint;
    private float distance;
    private float actualDistance;
    private float actualStartDelay;
    private int idxPoint = 0;

    private Transform playerParent;

    void Awake()
    {
        pointsTransform.Add(gameObject.transform);

        foreach (Transform pointTransform in pointsTransform)
        {
            points.Add(pointTransform.position);
        }

        targetPoint = points[idxPoint];
        point = points[points.Count - 1];
        distance = Vector3.Distance(point, targetPoint);
    }

    void Update()
    {
        if (points.Count > 1 && actualStartDelay >= startDelay)
        {
            actualDistance += Time.deltaTime * speed;
            actualDistance = Mathf.Min(actualDistance, distance);
            gameObject.transform.position = Vector3.Lerp(point, targetPoint, actualDistance / distance);
            if (actualDistance == distance)
            {
                point = points[idxPoint];
                idxPoint++;
                if (idxPoint >= points.Count)
                    idxPoint = 0;
                targetPoint = points[idxPoint];
                distance = Vector3.Distance(point, targetPoint);
                actualDistance = 0;
            }
        }
        else if (actualStartDelay < startDelay)
        {
            actualStartDelay += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerParent = other.transform.parent;
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = playerParent;
        }
    }

    public void InitEntity(int p1, int p2, int p3, int p4, int p5, int p6)
    {
        
    }
}
