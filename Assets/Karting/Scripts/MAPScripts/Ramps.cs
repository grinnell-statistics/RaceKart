using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Class: Ramps
   Purpose: Keeps track of how many ramps have been used by the player
   Note: There are only 3 active ramps in each track
 */

public class Ramps : MonoBehaviour
{

    public int rampNumber;
    
    [HideInInspector]
    public static bool [] rampTouch;
    // Start is called before the first frame update
    void Start()
    {
        rampTouch = new bool[] { false, false, false, false, false, false, false};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider Col)
    {
        rampTouch[rampNumber] = true;
    }
}
