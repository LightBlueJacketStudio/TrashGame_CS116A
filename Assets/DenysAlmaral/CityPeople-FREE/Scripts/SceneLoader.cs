using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Scenes to Load")]
    public string[] scenesToLoad = new string[]
    {
        "PlayerScene",
        "NPCScene",
        ""
    };
    
    void Awake()
    {
        Debug.Log("SceneLoader: Starting to load scenes...");
        
        foreach (string sceneName in scenesToLoad)
        {
            Debug.Log("SceneLoader: Attempting to load " + sceneName);
            
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
                Debug.Log("SceneLoader: Loaded " + sceneName);
            }
            else
            {
                Debug.Log("SceneLoader: " + sceneName + " already loaded");
            }
        }
    }
}