/*
 ******************************************************************************
 * PlayerController.cs
 * Authors: Bill Pham & Phuc Tran
 * 
 * This class handles the status of the player such as collisions with coin
 * objects or spells. This class doesn't handle player movement.
 ******************************************************************************
*/

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Variable for the total coins the player has collected
    public int collectedCoins = 0;

    // Variable for the player nametag
    public TextMeshPro TmpPrefab;

    // Player crowd control status
    public bool isSlowed = false;

    // Reference to main camera
    private GameObject MainCamera;

    // Player default speed
    private float Speed = 10.0f;

    // Reference to the player's local score
    public Text m_CoinsCollected;

    void Awake ()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Start()
    {
        m_CoinsCollected = GameObject.Find("Score Text").GetComponent<Text>();

        /// Set score text
        m_CoinsCollected.text = "Score : " + collectedCoins;
    }

    // Delete coins when colliding and adds a point to score
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            other.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                other.GetComponent<ASL.ASLObject>().DeleteObject();
            });

            collectedCoins += 1;
            m_CoinsCollected.text = "Score : " + collectedCoins;
        }

        if (other.gameObject.tag == "Slow")
        {
            isSlowed = true;
        }
    }

    // Detects if colliding with a crowd control effect
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
    
    void Update() {
        if (!GameVariables.gameStarted)
        {
            collectedCoins = 0;
            m_CoinsCollected.text = "Score : " + collectedCoins;
        }

        // Rotates player nametag towards the camera
        if (TmpPrefab.transform.rotation.eulerAngles.y != MainCamera.transform.rotation.eulerAngles.y)
        {
            TmpPrefab.transform.rotation = Quaternion.Lerp(TmpPrefab.transform.rotation, MainCamera.transform.rotation, Speed * Time.deltaTime);
        }
    }  
}

