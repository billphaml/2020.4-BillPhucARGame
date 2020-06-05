using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Deletes coin when collding with them
// Move this function to the coin object later so player object doesn't need two colliders
public class PlayerController : MonoBehaviour
{
    public int collectedCoins = 0;
    public TextMeshPro TmpPrefab;
    public bool isSlowed = false;
    private GameObject MainCamera;
    private float Speed = 10.0f;

    void Awake ()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Delete coins when colliding
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            other.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                other.GetComponent<ASL.ASLObject>().DeleteObject();
                collectedCoins += 1;
                GameVariables.collectCoins = collectedCoins;
            });
        }

        if (other.gameObject.tag == "Slow")
        {
            isSlowed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Slow")
        {
            isSlowed = false;
        }
        
        if (other.gameObject.tag == "Rock")
        {
            
        }
    }
    
    void Update(){
        if (TmpPrefab.transform.rotation.eulerAngles.y != MainCamera.transform.rotation.eulerAngles.y)
        {
            TmpPrefab.transform.rotation = Quaternion.Lerp(TmpPrefab.transform.rotation, MainCamera.transform.rotation, Speed * Time.deltaTime);

        }
    }
    
}
