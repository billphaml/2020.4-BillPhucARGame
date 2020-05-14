using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRock : MonoBehaviour
{
    private static GameObject rock = null;
    
    // Stack to store rock created
    private static Stack<Object> rocks = new Stack<Object>();

    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if(GameVariables.isAddObject){
            Vector3 position = gameObject.transform.position;
            position += new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(-10f, 10f));
            
            ASL.ASLHelper.InstanitateASLObject("Obstacle_Rock_Variant", position, Quaternion.identity,GameObject.Find("Level(Clone)").GetComponent<ASL.ASLObject>().m_Id, string.Empty, OnRockCreated, ClaimRejected, MoveRockWithFloats);
            GameVariables.isAddObject=false;
        }
        
        // Remove rock on click
        if(GameVariables.isRemoveObject&&rocks.Count>0){
            GameObject obj = (GameObject)rocks.Peek();
            rocks.Pop();
            obj.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                obj.GetComponent<ASL.ASLObject>().DeleteObject();
            });
            GameVariables.isRemoveObject=false;

        }

    }
    // Gets called after the player has spawned in, receieves a reference to the player
    public static void OnRockCreated(GameObject _myGameObject)
    {
        // Gets a reference to the player from the passed in object
        rock = _myGameObject;
        rocks.Push(rock);
        
    }
    // Gets called if a claim to the player is rejected
    public static void ClaimRejected(string _id, int _cancelledCallbacks)
    {
        // Do nothing lol
        // We don't care if a claim is rejected
    }
    // Gets called if player's ASL.Object script receives floats
    public static void MoveRockWithFloats(string _id, float[] _floats)
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
