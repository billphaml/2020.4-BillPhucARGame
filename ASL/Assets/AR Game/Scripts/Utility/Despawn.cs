/*
 ******************************************************************************
 * Despawn.cs
 * Authors: Bill Pham & Phuc Tran
 * 
 * Destroys a object after it has existed for a set amount of time.
 ******************************************************************************
*/

using UnityEngine;

public class Despawn : MonoBehaviour
{
    public float despawnTime = 30.0f;

    private float timeSinceSpawn = 0.0f;

    void Update()
    {
        if (timeSinceSpawn > despawnTime)
        {
            Destroy(gameObject);
        }
        else
        {
            timeSinceSpawn += Time.deltaTime;
        }
    }
}
