using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceSound : MonoBehaviour
{

    // The area of the sound
    public Collider area;
    // The object to track
    public GameObject player;


    // Update is called once per frame
    void Update()
    {
        // Locate closest point on the collider to the player
        Vector3 closestPoint = area.ClosestPoint(player.transform.position);
        // Set position to closest point to the player
        transform.position = closestPoint;
    }
}
