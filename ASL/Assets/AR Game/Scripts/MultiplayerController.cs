using UnityEngine;

// Handles player spawning and controlling
public class MultiplayerController : MonoBehaviour
{
    // Speed of all players
    public float moveSpeed = 0.3f;

    public float slowSpeed = 0.1f;

    // Reference to the joystick
    private Joystick joystick;

    // Reference to the player
    private static GameObject player = null;

    private bool preStatus = false;

    // Start is called before the first frame update
    void Start()
    {
        // Spawns in the player and calls the function OnPlayerCreated()
        ASL.ASLHelper.InstanitateASLObject("Player", new Vector3(0, 1.0f, 0), Quaternion.identity,
            string.Empty, string.Empty, OnPlayerCreated, ClaimRejected, MovePlayerWithFloats);
//
        // Gets a reference to the joystick from the hierarchy
        joystick = FindObjectOfType<Joystick>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
                joystick.Horizontal * slowSpeed + Input.GetAxis("Horizontal") * slowSpeed, // X Dir
                0.0f,                                                                      // Y Dir
                joystick.Vertical * slowSpeed + Input.GetAxis("Vertical") * slowSpeed,     // Z Dir
                0.0f    // Unused
                };
                player.GetComponent<ASL.ASLObject>().SendFloatArray(direction);
            }
            else
            {
                float[] direction = new float[]
                {
                joystick.Horizontal * moveSpeed + Input.GetAxis("Horizontal") * moveSpeed, // X Dir
                0.0f,                                                                      // Y Dir
                joystick.Vertical * moveSpeed + Input.GetAxis("Vertical") * moveSpeed,     // Z Dir
                0.0f    // Unused
                };
                player.GetComponent<ASL.ASLObject>().SendFloatArray(direction);
            }

            if (GameVariables.gameStarted == false && preStatus == true){
                player.GetComponent<ASL.ASLObject>().SendAndSetLocalPosition(new Vector3(0, 1.0f, 0));
            }
        });

        preStatus = GameVariables.gameStarted;
    }

    // Gets called after the player has spawned in, receieves a reference to the player
    public static void OnPlayerCreated(GameObject _myGameObject)
    {
        player = _myGameObject;
        // Gets a reference to the player from the passed in object
        var gametransform = _myGameObject.transform;
        var playercmp = gametransform.GetChild(0);
        
        // Assigns the player object a random color
        playercmp.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

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

        // Applies rigidbody physics to the player using floats containing direction
        temp.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(
            _floats[0], _floats[1], _floats[2]));
    }
}
