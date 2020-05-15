using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    // Time delay between coin spawns
    public float timeBetweenSpawn = 1f;
    
    // Determines if coins should be spawning
    public bool isSpawning = true;
    
    // Variable to store current time plus delay
    private float nextSpawnTime;
    
    private static Queue<Object> coins = new Queue<Object>();
    
    void Update()
    {
        // Amount of time pasted is greater than delay
        if (Time.time > nextSpawnTime&&GameVariables.gameStarted==true)
        {
            // Reset delay
            nextSpawnTime = Time.time + timeBetweenSpawn;
            
            // Determines coin spawn area by taking gameobject's (level) position plus a range in the x, z direction
            Vector3 position = gameObject.transform.position;
            position += new Vector3(Random.Range(-10f, 10f), 1f, Random.Range(-10f, 10f));
            
            // Creates a coin at a random location with a normal rotation orientation
            // Attaches coin the the gameobject (level)
            ASL.ASLHelper.InstanitateASLObject("Coin", position, Quaternion.identity, gameObject.GetComponent<ASL.ASLObject>().m_Id,string.Empty,OnCoinCreated);
            
            // Despawning coins once total coins equal max number of coins allowed
            if (coins.Count >= GameVariables.maxCoins) {
                GameObject coin = (GameObject)coins.Peek();
                coins.Dequeue();
                coin.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                {
                    coin.GetComponent<ASL.ASLObject>().DeleteObject();
                });
            }
        }
        
        // Game Stop
        if(GameVariables.gameStarted==false){
            while(coins.Count>0){
                GameObject coin = (GameObject)coins.Peek();
                coins.Dequeue();
                coin.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                {
                    coin.GetComponent<ASL.ASLObject>().DeleteObject();
                });
                
            }
        }
        
    }
    
    // Gets called after the coin has spawned in,
    public static void OnCoinCreated(GameObject _myGameObject)
    {
        coins.Enqueue(_myGameObject);
    }
}
