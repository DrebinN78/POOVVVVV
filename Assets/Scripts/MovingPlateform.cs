using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateform : MonoBehaviour
{
    public List<Transform> pointsTransform;
    public float speed = 1f;

    private List<Vector3> points = new List<Vector3>();
    private Vector3 point;
    private Vector3 targetPoint;
    private float distance;
    private float actualDistance;
    private int idxPoint = 0;

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
        if (points.Count > 1)
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
    }
}
