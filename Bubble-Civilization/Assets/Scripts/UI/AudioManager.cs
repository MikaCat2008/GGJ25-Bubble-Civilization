using System;
using BubbleApi;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource AudioSourceGO;


    private static AudioManager instance = null;
    public static AudioManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null  && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeVolume(float newValue)
    {
        AudioSourceGO.volume = newValue;
    }


}
