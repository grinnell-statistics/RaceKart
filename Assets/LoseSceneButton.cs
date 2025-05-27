using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using KartGame.KartSystems;

public class LoseSceneButton : MonoBehaviour
{ 
    public GameObject sandboxValue;
    public GameObject normalLevel;

    // Start is called before the first frame update
    void Start()
    {
        if (ArcadeKart.SandboxChosen == true)
        {
            sandboxValue.SetActive(true);
        } 
        else 
         {
             normalLevel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
