using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*RandomSpawn();*/
    }
    public void RandomSpawn()
    {
        float[] arrX = { 3f, 0f, -3f };
        int x = Random.Range(0, 3);
        float xPos = arrX[x];


        int[] arrZ = { 100, 150, 200 };
        int z = Random.Range(0, 3);
        float zPos = arrZ[z];


        transform.localPosition = new Vector3(xPos, transform.localPosition.y, zPos);
    }

    public void NewRandomSpawn()
    {
        float[] arrX = { 3f, 0f, -3f };
        int x = Random.Range(0, 3);
        float xPos = arrX[x];


        int[] arrZ = { 30, 40, 50 };
        int z = Random.Range(0, 3);
        float zPos = arrZ[z];


        transform.localPosition = new Vector3(xPos, transform.localPosition.y, zPos);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * PlayerManager.instance.moveSpeed * Time.deltaTime);
        /*if(transform.localPosition.z <= 0)
        {
            RandomSpawn();
        }*/
    }
}
