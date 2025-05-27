using UnityEngine;

/// <summary>
/// This class inherits from TargetObject and represents a PickupObject.
/// </summary>
/// 
//***************************************
//  Modified for data purposes by Ryuta
//  Modified for Creating a 4th Checkpoint/Finish Line 
//             + Preventing the player from going backwards
//               in the track by Priyanka
//***************************************
public class PickupObject : TargetObject
{
    [Header("PickupObject")]

    [Tooltip("New Gameobject (a VFX for example) to spawn when you trigger this PickupObject")]
    public GameObject spawnPrefabOnPickup;

    [Tooltip("Destroy the spawned spawnPrefabOnPickup gameobject after this delay time. Time is in seconds.")]
    public float destroySpawnPrefabDelay = 10;
    
    [Tooltip("Destroy this gameobject after collectDuration seconds")]
    public float collectDuration = 0f;

    public GameObject obstacle;
    public GameObject StartLine;
    public GameObject FinishLine;

    void Start() {
        Register();
    }

    void OnCollect()
    {
        //Gets the MeshRenderer Component of the FinishLine
        MeshRenderer m = FinishLine.GetComponent<MeshRenderer>();

        if (CollectSound)
        {
            AudioUtility.CreateSFX(CollectSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        }

        if (spawnPrefabOnPickup)
        {
            var vfx = Instantiate(spawnPrefabOnPickup, CollectVFXSpawnPoint.position, Quaternion.identity);
            Destroy(vfx, destroySpawnPrefabDelay);
        }
               
        Objective.OnUnregisterPickup(this);

        TimeManager.OnAdjustTime(TimeGained);

        // Collecting checkpoint times
        if (gameObject.name == "FInishLine")
        {
            //add 1 second to the final score for every booster selected
            DataManager.gameData.time = TimeManager.timeTaken + (Booster.count * 1f);  
            // if the player successfully completes the game, the time to restar
            // is the finish time
            DataManager.gameData.restart = TimeManager.timeTaken;
        }
        else if (gameObject.name == "Checkpoint (2)")
        {
            //disables the obstacle preventing the user from going backwards
            //until they cross the third checkpoint 
            obstacle.SetActive(false);
            Debug.Log("Checkpoint 3 Crossed");

            DataManager.gameData.c3 = TimeManager.timeTaken;
        }
        else if (gameObject.name == "Checkpoint (1)")
        {
            //disables the initial Start Line
            StartLine.SetActive(false);
            //makes the Finish Line (i.e. the 4th checkpoint) visible
            m.enabled = true;
            Debug.Log("Checkpoint 2 Crossed");

            DataManager.gameData.c2 = TimeManager.timeTaken;
        }
        else if (gameObject.name == "Checkpoint")
        {
            DataManager.gameData.c1 = TimeManager.timeTaken;
        }
    

        Destroy(gameObject, collectDuration);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if ((layerMask.value & 1 << other.gameObject.layer) > 0 && other.gameObject.CompareTag("Player"))
        {
            OnCollect();
        }
    }

    public float CheckDistanceTwoPoints(Vector3 target, Vector3 startPos)
    {
        return Vector3.Distance(target, startPos);
    }
}
