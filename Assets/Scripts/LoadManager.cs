using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager
{
    
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void LoadManagerScene()
    {
        Debug.Log("Im in");
        string SceneName = "ManagerScene";
        if(!SceneManager.GetSceneByName(SceneName).IsValid())
        {
            SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        }

    }
}
