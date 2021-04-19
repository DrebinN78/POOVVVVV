using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateform : MonoBehaviour
{
    public List<Transform> pointsTransform;
    public float speed = 1f;

    private List<Vector3> points = new List<Vector3>();
    private Vector3 targetPoint;
    private Vector3 directionNormalized;
    private int idxPoint = 0;

    void Awake()
    {
        pointsTransform.Add(gameObject.transform);

        foreach (Transform pointTransform in pointsTransform)
        {
            points.Add(pointTransform.position);
        }

        targetPoint = points[idxPoint];
        directionNormalized = (targetPoint - gameObject.transform.position).normalized;
    }

    void Update()
    {
        if (points.Count > 1)
        {
            gameObject.transform.position += Time.deltaTime * speed * directionNormalized;
            if (Vector3.Angle((targetPoint - gameObject.transform.position).normalized, directionNormalized) > 90)
            {
                idxPoint++;
                if (idxPoint >= points.Count)
                    idxPoint = 0;
                targetPoint = points[idxPoint];
                directionNormalized = (targetPoint - gameObject.transform.position).normalized;
            }
        }
    }
}
