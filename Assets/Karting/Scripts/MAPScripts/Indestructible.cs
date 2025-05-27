using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * This class keeps in track the previous scene number,
 * which is sent through the LoadScene class.
 * It sends the previous scene number to other classes
 */

public class Indestructable : MonoBehaviour {

    // For sake of example, assume -1 indicates first scene
    public static int prevScene = -1;

    void Awake() {
        DontDestroyOnLoad(this.gameObject) ;
    }
}

