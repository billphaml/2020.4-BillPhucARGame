using UnityEngine;
using System;

// Handles player spawning and controlling
public class MultiplayerController : MonoBehaviour
{
    // Speed of all players
    public float moveSpeed = 10f;

    public float slowSpeed = 3f;

    // Reference to the joystick
    private Joystick joystick;

    // Reference to the player
    private static GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        // Spawns in the player and calls the function OnPlayerCreated()
        ASL.ASLHelper.InstanitateASLObject("Player", new Vector3(0, 1.0f, 0), Quaternion.identity,
            string.Empty, string.Empty, OnPlayerCreated, ClaimRejected, MovePlayerWithFloats);

        // Gets a reference to the joystick from the hierarchy
        joystick = FindObjectOfType<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player==null) return;
        
        player.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
        {
            // Movement type 3 [Current]
            // Moves the player by sending float array for xyz direction to all clients and each
            // client uses rigidbody physics on the player gameobject that was affected, affects
            // all clients
            // Note 1: This can potentially be very laggy to constantly be sending floats, perhaps
            // only send when input is detected
            // Note 2: PC player speed is faster than android speed. This could be because PC is
            // receiving both joystick and input speeds which is unintentional, fix this later
            if (player.GetComponent<PlayerController>().isSlowed == true)
            {
                float[] direction = new float[]
                {
                    joystick.Horizontal * slowSpeed * Time.deltaTime + Input.GetAxis("Horizontal") * slowSpeed * Time.deltaTime, // X Dir
                    0.0f,                                                                      // Y Dir
                    joystick.Vertical * slowSpeed * Time.deltaTime + Input.GetAxis("Vertical") * slowSpeed * Time.deltaTime,     // Z Dir
                    0.0f    // Unused
                };
                player.GetComponent<ASL.ASLObject>().SendFloatArray(direction);
            }
            else
            {
                float[] direction = new float[]
                {
                    joystick.Horizontal * moveSpeed * Time.deltaTime + Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, // X Dir
                    0.0f,                                                                      // Y Dir
                    joystick.Vertical * moveSpeed * Time.deltaTime + Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime,     // Z Dir
                    0.0f    // Unused
                };
                player.GetComponent<ASL.ASLObject>().SendFloatArray(direction);
            }
        });
    }

    // Gets called after the player has spawned in, receieves a reference to the player
    public static void OnPlayerCreated(GameObject _myGameObject)
    {
        // Sets the internal reference to the player
        player = _myGameObject;
    
        player.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
        {
            float[] myValue = new float[4];
            myValue[0] = UnityEngine.Random.Range(0.0f, 1.0f);
            myValue[1] = UnityEngine.Random.Range(0.0f, 1.0f);;
            myValue[2] = UnityEngine.Random.Range(0.0f, 1.0f);
            myValue[3] = 10f+ASL.GameLiftManager.GetInstance().m_PeerId;
            player.GetComponent<ASL.ASLObject>().SendFloatArray(myValue);
        player.transform.GetChild(1).GetComponent<TMPro.TextMeshPro>().text=ASL.GameLiftManager.GetInstance().m_Username;

        });

    }

    // Gets called if a claim to the player is rejected
    public static void ClaimRejected(string _id, int _cancelledCallbacks)
    {
        // Do nothing lol
        // We don't care if a claim is rejected
    }

    // Gets called if player's ASL.Object script receives floats
    public static void MovePlayerWithFloats(string _id, float[] _floats)
    {
        // Stores a temp reference the the ASL Object script in a player
        ASL.ASLObject temp;

        // Gets the player matching the id of the player that called the send floats function
        ASL.ASLHelper.m_ASLObjects.TryGetValue(_id, out temp);
        if(_floats[3]>=10f){
            temp.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(_floats[0], _floats[1], _floats[2], 1.0f);
            int peerID = Convert.ToInt32(_floats[3]) - 10;
            string username = ASL.GameLiftManager.GetInstance().m_Players[peerID];

            temp.transform.GetChild(1).GetComponent<TMPro.TextMeshPro>().text = username;

        } else    {
        Vector3 dir = new Vector3(_floats[0], _floats[1], _floats[2]);

        // If there is input else stop the player
        if (dir != Vector3.zero)
        {
            // Applies rigidbody physics to the player using floats containing direction
            temp.gameObject.GetComponent<Rigidbody>().AddForce(dir);
        }
        else
        {
            temp.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        }

    }
}
