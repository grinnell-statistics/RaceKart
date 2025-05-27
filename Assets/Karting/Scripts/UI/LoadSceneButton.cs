using UnityEngine;
using UnityEngine.SceneManagement;

namespace KartGame.UI
{
    //  Class: LoadSceneButton
    // Purpose: Manages loading a target scene
    public class LoadSceneButton : MonoBehaviour
    {
        [Tooltip("What is the name of the scene we want to load when clicking the button?")]
        public string SceneName;
        public static string prevScene = "";
        public static string currentScene = "";
 
        public virtual void Start() {
            currentScene = SceneManager.GetActiveScene().name;
        }

        // Function: LoadTargetScene
        //  Purpose: Loads scene depending on the name provided by user
        public void LoadTargetScene() 
        {
            prevScene = currentScene;
            Indestructable.prevScene = SceneManager.GetActiveScene().buildIndex;;
            SceneManager.LoadSceneAsync(SceneName);
        }
    }
}
