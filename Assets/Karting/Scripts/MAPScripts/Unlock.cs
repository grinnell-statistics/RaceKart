using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
[ CLASS CURRENTLY NOT IN USE]
 * Class: Unlock
 * - Removes the 'lock' image from the choices of assets 
 * - in the garage scene as the user crosses different levels
 */
public class Unlock : MonoBehaviour
{
    public GameObject lock1Wheel;
    public GameObject lock2Wheel;
    public GameObject lock1Engine;
    public GameObject lock2Engine;
    public GameObject lock1Car;
    public GameObject lock2Car;
    
    public static int prevSceneNum;

    void Start()
    {
        prevSceneNum = Indestructable.prevScene; 
    }

    public void ChangeLock()
    {
        //if the user has just cleared Level 1 Part 1
        if (prevSceneNum > 9)
        {
            //unlock wheel 1 and engine 1 after first track completed
            lock1Wheel.SetActive(false);
            lock1Engine.SetActive(false);

            if (prevSceneNum > 14)
            {
                //unlock wheel 2 and engine 2 after second track completed
                lock2Wheel.SetActive(false);
                lock2Engine.SetActive(false);

                 if (prevSceneNum > 18)
                {
                    //unlock car 1 after third track completed
                    lock1Car.SetActive(false);

                    if (prevSceneNum > 27)
                    {
                        lock2Engine.SetActive(false);
                    }
                }
            }
        }

        
    }
}
