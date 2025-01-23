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

        Building house = GlobalStorage.storage.bubbles[0].buildings.GetBuilding(0);
        House_BuildingSystem houseSystem = GlobalStorage.systems.house;

        Debug.Log(houseSystem.BuildingToString(house));
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
