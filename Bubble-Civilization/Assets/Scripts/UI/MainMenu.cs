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

}
