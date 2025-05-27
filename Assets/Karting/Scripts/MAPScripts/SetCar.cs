using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using KartGame.KartSystems;

//   Class: SetCar
// Purpose: Sets car in the Game depending on what was chosen in the Garage Scene
public class SetCar : MonoBehaviour
{
    // array of player kart models
    public GameObject[] carsForArrange;

    // 2D array to be used to arrange the player kart prefabs
    private GameObject[,] allCars = new GameObject[4, 4];

    // camera that follows the player kart
    public CinemachineVirtualCamera vcam;

    // initial position of kart player in game scene
    private Vector3 iniPos;

    // initial rotation of kart player in game scene
    private Quaternion iniRot;

    public Booster booster;


    private static GameObject displayCar;

    void Awake()
    {

        DataManager.gameData.challenge1 = "";
        DataManager.gameData.challenge2 = "";

        int c = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                allCars[i, j] = carsForArrange[c];
                c++;
            }
        }

        iniPos = gameObject.transform.position;
        iniRot = gameObject.transform.rotation;
        Destroy(gameObject);
        displayCar = Instantiate<GameObject>(allCars[DataManager.y, DataManager.x], iniPos, iniRot, transform.parent);

        // make the Cinematic camera follow the selected car as the user moves
        vcam.m_Follow = displayCar.transform;
        vcam.m_LookAt = displayCar.transform;
        ArcadeKart current = displayCar.GetComponent<ArcadeKart>();

        // change the topSpeed/Acceleration/AccelerationCurve based on 
        // the type of engine the user chooses in the garage scene
        switch (DataManager.engineChoice)
        {
                //TheRev
                case 0:
                    current.baseStats.TopSpeed = 15;
                    current.baseStats.Acceleration = 15;
                    current.baseStats.AccelerationCurve = 0.8f;
                    break;
                //Tube
                case 1:
                    current.baseStats.TopSpeed = 22;
                    current.baseStats.Acceleration = 12;
                    current.baseStats.AccelerationCurve = 0.6f;
                    break;
                //Corazon   
                case 2:
                    current.baseStats.TopSpeed = 25;
                    current.baseStats.Acceleration = 5;
                    current.baseStats.AccelerationCurve = 0.4f;
                    break;
        }
        
        if (Booster.count > 0)
        {

            DataManager.gameData.challenge1 = Booster.parameterName[0];

            switch (Booster.parameterName[0])
            {
                case "TopSpeed":
                    current.baseStats.TopSpeed = Mathf.Ceil(Booster.changedValue[0]);
                    break;
                case "Acceleration":
                    current.baseStats.Acceleration = Mathf.Ceil(Booster.changedValue[0]);
                    break;
                case "AccelerationCurve":
                    current.baseStats.AccelerationCurve = Booster.changedValue[0];
                    break;
                case "CoastingDrag":
                    current.baseStats.CoastingDrag = Mathf.Ceil(Booster.changedValue[0]);
                    break;
                case "Grip":
                    current.baseStats.CoastingDrag = Booster.changedValue[0];
                    break;
                case "Suspension":
                    current.baseStats.Suspension = Booster.changedValue[0];
                    break;
                case "Steer":
                    current.baseStats.Steer= Mathf.Ceil(Booster.changedValue[0]);
                    break;
            }

            if (Booster.parameterName[1] !=null)
            {

                DataManager.gameData.challenge2 = Booster.parameterName[1];

                switch (Booster.parameterName[1])
                {
                    case "TopSpeed":
                        current.baseStats.TopSpeed = Mathf.Ceil(Booster.changedValue[1]);
                        break;
                    case "Acceleration":
                        current.baseStats.Acceleration = Mathf.Ceil(Booster.changedValue[1]);
                        break;
                    case "AccelerationCurve":
                        current.baseStats.AccelerationCurve = Booster.changedValue[1];
                        break;
                    case "CoastingDrag":
                        current.baseStats.CoastingDrag = Mathf.Ceil(Booster.changedValue[1]);
                        break;
                    case "Grip":
                        current.baseStats.CoastingDrag = Booster.changedValue[1];
                        break;
                    case "Suspension":
                        current.baseStats.Suspension = Booster.changedValue[1];
                        break;
                    case "Steer":
                        current.baseStats.Steer= Mathf.Ceil(Booster.changedValue[1]);
                        break;
                    default:
                        Debug.Log ("one booster chosen");
                        break;
                }
            }
        }


        GameObject.Find("GameManager").GetComponent<GameFlowManager>().playerKart = displayCar.GetComponent<ArcadeKart>();
        displayCar.SetActive(false);
    }

}
