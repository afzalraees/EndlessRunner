using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.back * PlayerManager.instance.moveSpeed * Time.deltaTime);
        if (transform.localPosition.z <= 0)
        {
            transform.parent.GetComponent<PointScript>().GeneratePointsrandomly();
        }
    }
}
