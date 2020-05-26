using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Deletes coin when collding with them
// Move this function to the coin object later so player object doesn't need two colliders
public class PlayerController : MonoBehaviour
{
    public int collectedCoins = 0;

    public bool isSlowed = false;

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
    }
}
