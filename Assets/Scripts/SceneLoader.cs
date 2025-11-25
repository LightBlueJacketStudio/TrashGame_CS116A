using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader1 : MonoBehaviour
{
    void Start()
    {
        // loadING the EventScene
        SceneManager.LoadSceneAsync("EventScene", LoadSceneMode.Additive);
    }
}
