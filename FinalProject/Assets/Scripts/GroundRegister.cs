using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRegister : MonoBehaviour
{
    // NOTES

    // Goal of this script is to register when the player is on the path versus when they
    // stray further and further away so different things can happen as a result

    // Locational bools to know when the player is where
    [SerializeField] bool onMainPath, onNearPath, onFarFromPath;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
