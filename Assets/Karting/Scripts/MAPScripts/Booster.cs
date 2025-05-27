using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KartGame.UI;
using KartGame.KartSystems;
using UnityEngine.SceneManagement;


/* Class: Booster
   Purpose: The same as SetCar, except it sets the car if the user
            chooses the challenge mode
 */
public class Booster : MonoBehaviour
{
    public Text TopSpeed;
    public Text Acceleration;
    public Text AccelerationCurve;
    public Text Suspension;
    public Text CoastingDrag;
    public Text Steer;
    public Text Grip;
    
    [HideInInspector]
    public static int count;

    // array of parameter names that have been altered 
    // in the booster scene
    public static string [] parameterName;

    // array of changed values 
    public static float[] changedValue;
    
    // array of player kart models
    public GameObject[] carsForArrange;

    // 2D array to be used to arrange the player kart prefabs
    private GameObject[,] allCars = new GameObject[4, 4];

    float Temp;

    private static GameObject displayCar;

    void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        
        // used in the AlterValuesBooster class to keep track of the
        // characteristics that have been changed 
        count = 0; 
        changedValue = new float[2];
        parameterName = new string[2];
        
        if (scene.name == "Booster")
        {
            int c = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    allCars[i, j] = carsForArrange[c];
                    c++;
                }
            }

            displayCar = Instantiate<GameObject>(allCars[DataManager.y, DataManager.x]);
            ArcadeKart current = displayCar.GetComponent<ArcadeKart>();

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

            TopSpeed.text = current.baseStats.TopSpeed.ToString("0.0");
            Acceleration.text = current.baseStats.Acceleration.ToString("0.0");
            Steer.text = current.baseStats.Steer.ToString("0.0");
            CoastingDrag.text = current.baseStats.CoastingDrag.ToString("0.0");
            AccelerationCurve.text = current.baseStats.AccelerationCurve.ToString("0.0");
            Grip.text = current.baseStats.Grip.ToString("0.0");
            Suspension.text= current.baseStats.AccelerationCurve.ToString("0.0");

            displayCar.SetActive(false);
        }
    }

    public void Start()
    {
        LoadSceneButton button = GameObject.Find("StartButton").GetComponent<LoadSceneButton>();
        button.SceneName = DataManager.gameData.level;
    }
   

    


}