using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using KartGame.KartSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState{Play, Won, Lost}

//***************************************
//  Modified for data purposes by Ryuta
//***************************************
//  Modified to alter engine sound by
//      Reina and Priyanka
//**************************************
public class GameFlowManager : MonoBehaviour
{
    [Header("Parameters")]
    [Tooltip("Duration of the fade-to-black at the end of the game")]
    public float endSceneLoadDelay = 3f;
    [Tooltip("The canvas group of the fade-to-black screen")]
    public CanvasGroup endGameFadeCanvasGroup;

    [Header("Win")]
    [Tooltip("This string has to be the name of the scene you want to load when winning")]
    public string winSceneName = "WinScene";
    [Tooltip("String = name of scene player gets to after completing race in Sandbox mode")]
    public string sandboxWinSceneName = "VariablesChoose";
    [Tooltip("Duration of delay before the fade-to-black, if winning")]
    public float delayBeforeFadeToBlack = 4f;
    [Tooltip("Duration of delay before the win message")]
    public float delayBeforeWinMessage = 2f;
    [Tooltip("Sound played on win")]
    public AudioClip victorySound;
 
    [Tooltip("Prefab for the win game message")]
    public DisplayMessage winDisplayMessage;

    public static int  prevSceneNum;
    public static int  rampTouchCounter;
    public PlayableDirector raceCountdownTrigger;

    [Header("Lose")]
    [Tooltip("This string has to be the name of the scene you want to load when losing")]
    public string loseSceneName = "LoseScene";
    [Tooltip("Prefab for the lose game message")]
    public DisplayMessage loseDisplayMessage;

    public GameState gameState { get; private set; }

    public bool autoFindKarts = true;
    public ArcadeKart playerKart;
    GameObject engineVroomOn;
    //public GameObject aiCar;
    //GameObject aiEngineVroomOn;

    public float topSpeedOffset;
    public float accelerationOffset;
    public float accelerationCurveOffset;
    public float suspensionOffset;
    public float steerOffset;
    public float dragOffset;
    public float gripOffset;

    public static bool gameResetBool;
    ArcadeKart[] karts;
    ObjectiveManager m_ObjectiveManager;
    TimeManager m_TimeManager;
    float m_TimeLoadEndGameScene;
    string m_SceneToLoad;
    float elapsedTimeBeforeEndScene = 0;

    void Awake()
    {
      // records the # of the previous scene 
      prevSceneNum = Indestructable.prevScene;
      
    }

    void Start()
    {

         //creates an instance of the engine sound object of the PlayerKart
        engineVroomOn = playerKart.GetComponentInChildren<ArcadeEngineAudio>().gameObject;
        gameResetBool = false;

        if (autoFindKarts)
        {
            karts = FindObjectsOfType<ArcadeKart>();
            if (karts.Length > 0)
            {
                if (!playerKart) playerKart = karts[0];
            }
            DebugUtility.HandleErrorIfNullFindObject<ArcadeKart, GameFlowManager>(playerKart, this);
        }

        m_ObjectiveManager = FindObjectOfType<ObjectiveManager>();
		DebugUtility.HandleErrorIfNullFindObject<ObjectiveManager, GameFlowManager>(m_ObjectiveManager, this);

        m_TimeManager = FindObjectOfType<TimeManager>();
        DebugUtility.HandleErrorIfNullFindObject<TimeManager, GameFlowManager>(m_TimeManager, this);

        AudioUtility.SetMasterVolume(0.5f);

        winDisplayMessage.gameObject.SetActive(false);
        loseDisplayMessage.gameObject.SetActive(false);

        m_TimeManager.StopRace();
        
        foreach (ArcadeKart k in karts)
        {
			k.SetCanMove(false);
        }
        

        //run race countdown animation
        ShowRaceCountdownAnimation();

        StartCoroutine(ShowObjectivesRoutine());
        StartCoroutine(CountdownThenStartRaceRoutine());

        rampTouchCounter = 0;
        topSpeedOffset = 0f;
        accelerationOffset = 0f;
        accelerationCurveOffset = 0f;
        suspensionOffset = 0f;
        steerOffset = 0f;
        dragOffset = 0f;
        gripOffset = 0f;
    }

    IEnumerator CountdownThenStartRaceRoutine() {
        yield return new WaitForSeconds(3f);
        playerKart.gameObject.SetActive(true);
        StartRace();
    }

    void StartRace() {
        foreach (ArcadeKart k in karts)
        {
			k.SetCanMove(true);
        }
        m_TimeManager.StartRace();
    }

    void ShowRaceCountdownAnimation() {
        raceCountdownTrigger.Play();
    }

    IEnumerator ShowObjectivesRoutine() {
        while (m_ObjectiveManager.Objectives.Count == 0)
            yield return null;
        yield return new WaitForSecondsRealtime(0.2f);
        for (int i = 0; i < m_ObjectiveManager.Objectives.Count; i++)
        {
           if (m_ObjectiveManager.Objectives[i].displayMessage)m_ObjectiveManager.Objectives[i].displayMessage.Display();
           yield return new WaitForSecondsRealtime(1f);
        }
    }


    void Update()
    {

        //activates the engine sound of the PlayerKart if the engine sound toggle is on
        engineVroomOn.SetActive(InGameMenuManager.engineIsOn);
        
        if (gameResetBool == true)
        {
            CompileData(false);
            DataManager.SetComponents();
            DataManager.DebugAll();
            StartCoroutine(DataManager.instance.SendData());
        }
        

        if (gameState != GameState.Play)
        {
            elapsedTimeBeforeEndScene += Time.deltaTime;
            if(elapsedTimeBeforeEndScene >= endSceneLoadDelay)
            {

                float timeRatio = 1 - (m_TimeLoadEndGameScene - Time.time) / endSceneLoadDelay;
                endGameFadeCanvasGroup.alpha = timeRatio;

                float volumeRatio = Mathf.Abs(timeRatio);
                float volume = Mathf.Clamp(0.5f - volumeRatio, 0, 0.5f);
                AudioUtility.SetMasterVolume(volume);

                // See if it's time to load the end scene (after the delay)
                if (Time.time >= m_TimeLoadEndGameScene)
                {
                    SceneManager.LoadScene(m_SceneToLoad);
                    gameState = GameState.Play;
                }
            }
        }
        else
        {
            if (m_ObjectiveManager.AreAllObjectivesCompleted())
                EndGame(true);

            if (m_TimeManager.IsFinite && m_TimeManager.IsOver)
                EndGame(false);
        }
    }

    void EndGame(bool win)
    {
        // unlocks the cursor before leaving the scene, to be able to click buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        m_TimeManager.StopRace();

        // Remember that we need to load the appropriate end scene after a delay
        gameState = win ? GameState.Won : GameState.Lost;
        endGameFadeCanvasGroup.gameObject.SetActive(true);
        
        if (win)
        {
            //if the user came through the sandbox, take them to the 
            //variables choosing scene instead of the standard win scene
            if (prevSceneNum == 18)
            {
                m_SceneToLoad = sandboxWinSceneName;
            }
            else
            {
                m_SceneToLoad = winSceneName;
            }
            m_TimeLoadEndGameScene = Time.time + endSceneLoadDelay + delayBeforeFadeToBlack;

            // play a sound on win
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = victorySound;
            audioSource.playOnAwake = false;
            audioSource.outputAudioMixerGroup = AudioUtility.GetAudioGroup(AudioUtility.AudioGroups.HUDVictory);
            audioSource.PlayScheduled(AudioSettings.dspTime + delayBeforeWinMessage);

            // create a game message
            winDisplayMessage.delayBeforeShowing = delayBeforeWinMessage;
            winDisplayMessage.gameObject.SetActive(true);
        }
        else
        {
            m_SceneToLoad = loseSceneName;
            m_TimeLoadEndGameScene = Time.time + endSceneLoadDelay + delayBeforeFadeToBlack;

            // create a game message
            loseDisplayMessage.delayBeforeShowing = delayBeforeWinMessage;
            loseDisplayMessage.gameObject.SetActive(true);
        }
        CompileData(win);
        DataManager.SetComponents();
        DataManager.DebugAll();
        StartCoroutine(DataManager.instance.SendData());
    }

    /*initialize data upon victory of the player*/
    void CompileData(bool win)
    {
        foreach (bool item in Ramps.rampTouch)
            {
                switch(item)
                {
                    case true:
                        rampTouchCounter++;
                        break;
                    case false:
                        break;
                }
            }   

        switch(DataManager.gameData.level)
        {
            case "WindingTrack":
                  accelerationOffset = 1f;
                  accelerationCurveOffset = 0.1f;
                  suspensionOffset = -0.3f;
                  steerOffset = 1.5f;
                  dragOffset = -10f;
                  gripOffset = 0.2f;
                  break;
            case "MountainTrack":
                  topSpeedOffset = -5f;
                  accelerationOffset = 1f;
                  accelerationCurveOffset = 0.2f;
                  steerOffset = -3f;
                  dragOffset = 5f;
                  gripOffset = 0.25f;
                  break;
        }


        //Debug.Log ("Steer: " + playerKart.finalStats.Steer);
        DataManager.gameData.win = win;
        DataManager.gameData.timeTopSpeed = playerKart.timeToTopSpeed;
        DataManager.gameData.topSpeed = playerKart.topSpeedAchieved;
        DataManager.gameData.maxVel = playerKart.finalStats.TopSpeed + topSpeedOffset;
        DataManager.gameData.acceleration = playerKart.finalStats.Acceleration + accelerationOffset;
        DataManager.gameData.accelCurve = playerKart.finalStats.AccelerationCurve + accelerationCurveOffset;
        DataManager.gameData.suspension = playerKart.finalStats.Suspension + suspensionOffset;
        DataManager.gameData.drag = playerKart.finalStats.CoastingDrag + dragOffset;
        DataManager.gameData.steer = playerKart.finalStats.Steer + steerOffset;
        DataManager.gameData.grip = playerKart.finalStats.Grip + gripOffset;
        DataManager.gameData.gravity = playerKart.finalStats.AddedGravity;
        DataManager.gameData.ramps = rampTouchCounter;
        
    }

}
