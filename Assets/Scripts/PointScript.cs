using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScript : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();
    float timer = 0;
    public int pointLength;

    private void Start()
    {
        GeneratePointsrandomly();
    }
    public void GeneratePointsrandomly()
    {
        PlayerManager.instance.pointCounter = 0;
        foreach(var point in points)
        {
            point.gameObject.SetActive(false);
        }
        float[] arrX = { 3f, 0f, -3f };
        int x = Random.Range(0, 3);
        float xPos = arrX[x];
        pointLength = Random.Range(1, points.Count);
        for(int i = 0; i < pointLength; i++)
        {

            points[i].gameObject.SetActive(true);
            points[i].localPosition = new Vector3(xPos, points[i].localPosition.y, 30 + i);
        }
    }

    public void ReplacePoint(Transform point)
    {
        
    }
}
