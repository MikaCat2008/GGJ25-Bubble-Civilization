using System;
using BubbleApi;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    
    [SerializeField] GameObject MainMenuBG;
    [SerializeField] GameObject MainMenuContainer;
    [SerializeField] GameObject GameMenuContainer;
    [SerializeField] GameObject EndMenuContainer;


    //private GameObject MainMenuGO;
    public void Start()
    {
        volumeSlider.value = 0.4f;

        AudioManager.Instance.ChangeVolume(volumeSlider.value);
    }

    public void menuStrart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void changeVolume()
    {
        AudioManager.Instance.ChangeVolume(volumeSlider.value);
    }

    public void menuQuit()
    {
        Application.Quit();
    }

    public void menuRestart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
