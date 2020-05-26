using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    private float timeSinceSpawn = 0.0f; 

    // Update is called once per frame
    void Update()
    {
        if (timeSinceSpawn > 30)
        {
            Destroy(gameObject);
        }
        else
        {
            timeSinceSpawn += Time.deltaTime;
        }
    }
}
