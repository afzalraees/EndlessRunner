using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdleManager : MonoBehaviour
{
    float[] arrX = { 3.3f, 0f, -3.3f };



    // Start is called before the first frame update
    void Start()
    {
        SetPosition();
    }

    void SetPosition()
    {
        
        int x = Random.Range(0, arrX.Length);
        float xPos = arrX[x];


        float[] arrZ = { PlayerManager.instance.moveSpeed/5 * 30,
            PlayerManager.instance.moveSpeed/5 * 35,
            PlayerManager.instance.moveSpeed/5 * 40 };

        int z = Random.Range(0, arrZ.Length);
        float zPos = arrZ[z];

        transform.localPosition = new Vector3(xPos, transform.localPosition.y, zPos);
    }
    void RepositionHurdle()
    {
        if (transform.localPosition.z <= 0)
        {
            int x = Random.Range(0, 3);
            float xPos = arrX[x];

            float[] arrZ = { PlayerManager.instance.moveSpeed/5 * 30,
            PlayerManager.instance.moveSpeed/5 * 35,
            PlayerManager.instance.moveSpeed/5 * 40 };

            int z = Random.Range(0, 3);
            float zPos = arrZ[z];
            
            transform.localPosition = new Vector3(xPos, transform.localPosition.y, zPos);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * PlayerManager.instance.moveSpeed * Time.deltaTime);
        RepositionHurdle();
        
    }
}
