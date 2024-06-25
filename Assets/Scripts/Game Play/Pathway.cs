using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathway : MonoBehaviour
{
    public int pathwayID;
    public PathwayData pathwayData;
    public Vector2 startPoint;
    public List<Vector2> wayPoint;

    public void InitPathway(PathwayData  data)
    {
        pathwayData = data;
        startPoint = data.startPosition;
        wayPoint = data.waypoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color =Color.green;
        Gizmos.DrawLine(startPoint,wayPoint[0]);
        for (int i = 0; i < wayPoint.Count - 1; i++)
        {
            Gizmos.DrawLine(wayPoint[i], wayPoint[i + 1]);
        }
    }
}
