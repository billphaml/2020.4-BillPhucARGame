/*
 ******************************************************************************
 * SpellController.cs
 * This class handles the spell system for the game. The spell system allows
 * for a player for fire one of two spells using UI buttons and aiming using
 * their camera direction.
 * Authors: Bill Pham & Phuc Tran
 * 
 * Note:
 * Spell One - Left side button
 * Spell Two - Top side button
 ******************************************************************************
*/

using UnityEngine;
using UnityEngine.UI;

public class SpellController : MonoBehaviour
{
    // Reference to the UI buttons for spells
    [SerializeField]
    private Button spOne = default, spTwo = default;

    // Reference to UI text for displaying spell cooldowns
    [SerializeField]
    private Text spOneCooldownDisplay = default, spTwoCooldownDisplay = default;

    // Values for spell cooldowns
    [SerializeField]
    private float spOneCooldown = 10.0f, spTwoCoolDown = 20.0f;

    // Values for tracking current cooldown time
    private float spOneTimestamp, spTwoTimestamp;

    // Layers for the raycast to ignore
    [SerializeField]
    private LayerMask ignore = default;

    void Start()
    {
        // Listeners to call specific functions upon click
        spOne.onClick.AddListener(FireSlowSpell);
        spTwo.onClick.AddListener(FireRockSpell);
    }

    void Update()
    {
        // Updates cooldown display for spell one
        if (spOneTimestamp >= Time.time)
        {
            spOneCooldownDisplay.text = (Mathf.Round(spOneTimestamp - Time.time)).ToString();
        }
        else
        {
            spOneCooldownDisplay.text = "";
        }

        // Updates cooldown display for spell two
        if (spTwoTimestamp >= Time.time)
        {
            spTwoCooldownDisplay.text = (Mathf.Round(spTwoTimestamp - Time.time)).ToString();
        }
        else
        {
            spTwoCooldownDisplay.text = "";
        }
    }

    // Fires a spell that leaves a circular AOE zone that slows players who
    // travel over it
    private void FireSlowSpell()
    {
        if (spOneTimestamp <= Time.time)
        {
            // Try to get a new position
            Vector3 pos = GetPos();

            // Return cause we didn't hit anything
            if (pos == Vector3.zero)
            {
                return;
            }

            // Spawn the spell into the level
            ASL.ASLHelper.InstanitateASLObject("Slow Spell", pos, Quaternion.identity);

            spOneTimestamp = Time.time + spOneCooldown;
        }
    }

    // Fires a spell that spawns a rock into the map to block players
    private void FireRockSpell()
    {
        if (spTwoTimestamp <= Time.time)
        {
            // Try to get a new position
            Vector3 pos = GetPos();

            // Return cause we didn't hit anything
            if (pos == Vector3.zero)
            {
                return;
            }

            Vector3 modifiedPos = new Vector3(pos.x * 23, pos.y, pos.z * 23);

            // Spawn the spell into the level
            ASL.ASLHelper.InstanitateASLObject("Obstacle Rock", modifiedPos, Quaternion.identity, GameObject.Find("Level(Clone)").GetComponent<ASL.ASLObject>().m_Id);

            spTwoTimestamp = Time.time + spTwoCoolDown;
        }
    }

    // Gets a position to spawn spells by raycasting from the camera in the world
    // Only returns a position if it hits the map
    private Vector3 GetPos()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, int.MaxValue, ~ignore))
        {
            if (hit.transform.tag == "Map")
            {
                return hit.point;
            }
        }

        return Vector3.zero;
    }
}
