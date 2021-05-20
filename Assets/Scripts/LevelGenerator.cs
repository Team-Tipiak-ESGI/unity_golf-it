using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> straights = new List<GameObject>();
    public List<GameObject> corners = new List<GameObject>();
    public List<GameObject> end = new List<GameObject>();
    public List<GameObject> start = new List<GameObject>();
    public int length = 10;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 position = new Vector3(0, 0, 0);
        Quaternion rotation = Quaternion.identity;
        
        float rotationY = 0;
        
        float positionX = 0;
        float positionZ = 0;

        for (int i = 0; i < length; i++)
        {
            GameObject prefab;

            if (i == 0)
            {
                // Start
                prefab = start[Random.Range(0, start.Count)];
                prefab.tag = "Respawn";
            }
            else if (i == length - 1)
            {
                // End
                prefab = end[Random.Range(0, end.Count)];
            }
            else
            {
                // Other
                int random = Random.Range(0, 10);

                if (random > 2)
                {
                    // Straight
                    prefab = straights[Random.Range(0, straights.Count)];
                }
                else
                {
                    // Corner
                    prefab = corners[Random.Range(0, corners.Count)];

                    int rand = Random.Range(0, 2);
                    
                    rotation.eulerAngles = new Vector3(0, rotationY + rand == 0 ? -90 : 0, 0);
                    rotationY += 90;

                    if (rand == 0)
                    {
                        positionZ = -1;
                    }
                    else
                    {
                        positionZ = 1;
                    }
                }
            }

            Instantiate(prefab, position, rotation);

            rotation.eulerAngles = new Vector3(0, rotationY, 0);

            float x = prefab.GetComponent<Renderer>().bounds.size.x * (float) Math.Cos(rotationY * Math.PI / 180);
            float z = prefab.GetComponent<Renderer>().bounds.size.z * (float) Math.Sin(rotationY * Math.PI / 180) * positionZ;

            position.x += x;
            position.z += z;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}