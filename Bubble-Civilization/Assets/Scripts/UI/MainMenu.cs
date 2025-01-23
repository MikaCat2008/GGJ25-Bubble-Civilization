using System;
using BubbleApi;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    //[SerializeField] GameObject ActualGame;
    [SerializeField] AudioSource AudioSourceGO;
    [SerializeField] Slider volumeSlider;

    //private GameObject MainMenuGO;

    public void Start()
    {
        GlobalStorage.Initialize();
        GlobalStorage.storage.timer.speed = 1;

        GlobalStorage.systems.house.StartBuilding(
            0, GlobalStorage.storage.bubbles[0]
        );

        GlobalStorage.buildingUpdater.OnBuildingDone += (Building building, Bubble bubble) =>
        {

        };
    }

    public void menuStrart()
    {
        this.gameObject.SetActive(false);
        //ActualGame.SetActive(true);
    }

    public void changeVolume()
    {
        AudioSourceGO.volume = volumeSlider.value;
    }

    public void menuQuit()
    {
        Application.Quit();
    }

}
