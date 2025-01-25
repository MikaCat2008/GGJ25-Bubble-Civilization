using System;
using BubbleApi;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class MainMenu_UI : MonoBehaviour
{
    //[SerializeField] GameObject ActualGame;
    [SerializeField] AudioSource AudioSourceGO;
    [SerializeField] Slider volumeSlider;

    [SerializeField] GameObject GameMenuContainer;
    [SerializeField] GameObject MainMenuButtons;

    //private GameObject MainMenuGO;
    public void Start()
    {
        volumeSlider.value = 0.4f;
        AudioSourceGO.volume = volumeSlider.value;
    }
    public void menuStrart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void menuOptions()
    {
        GameMenuContainer.SetActive(true);
        MainMenuButtons.SetActive(false);
    }

    public void menuBack()
    {
        GameMenuContainer.SetActive(false);
        MainMenuButtons.SetActive(true);
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
