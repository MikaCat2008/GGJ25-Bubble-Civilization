using System;
using BubbleApi;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    //[SerializeField] GameObject ActualGame;
    [SerializeField] AudioSource AudioSourceGO;
    [SerializeField] Slider volumeSlider;


    [SerializeField] GameObject MainMenuBG;
    [SerializeField] GameObject MainMenuContainer;
    [SerializeField] GameObject GameMenuContainer;
    [SerializeField] GameObject EndMenuContainer;


    //private GameObject MainMenuGO;
    public void Start()
    {
        volumeSlider.value = 0.4f;
        AudioSourceGO.volume = volumeSlider.value;
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

    public void menuEngGame()
    {
        this.gameObject.SetActive(true);
        MainMenuBG.SetActive(true);
        MainMenuContainer.SetActive(false);
        GameMenuContainer.SetActive(false);
        EndMenuContainer.SetActive(true);
    }

    public static void menuRestart()
    {
        //showEndGameScreen
    }
}
