using BubbleApi;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    void Start()
    {
        if (GlobalStorage.Initialize())
            GlobalStorage.currentSceneType = SceneType.MainMenu;
    }

    public void FixedUpdate()
    {
        SceneType sceneType = GlobalStorage.currentSceneType;

        if (sceneType == SceneType.MainMenu)
            return;

        foreach (Bubble bubble in GlobalStorage.storage.bubbles)
            GlobalStorage.buildingUpdater.Update(bubble);

        GlobalStorage.storage.timer.Tick();
    }

    public void OpenMainMenu()
    {
        GlobalStorage.currentSceneType = SceneType.MainMenu;

        SceneManager.LoadScene("MainMenu");
    }

    public void OpenMap()
    {
        GlobalStorage.currentSceneType = SceneType.Map;

        SceneManager.LoadScene("Map");
    }

    public void OpenSettings()
    { 
    
    }

    public void CloseSettings()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
