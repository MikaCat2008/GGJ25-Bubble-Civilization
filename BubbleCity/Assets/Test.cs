using BubbleApi;
using UnityEngine;


public class Test : MonoBehaviour
{
    public Storage storage;

    void Start()
    {
        this.storage = new Storage();

        ResourcesContainer resources = new ResourcesContainer(
            100, 100, 100, 100, 100, 100, 100
        );
        BuildingsContainer buildings = new BuildingsContainer();

        Bubble bubble = new Bubble(resources, buildings);

        this.storage.bubbles.Add(bubble);

        Debug.Log(storage.bubbles[0].buildings.GetBuildingType(2));

        bubble.buildings.SetBuildingType(2, BuildingType.GreenHouse_Oxygen);

        Debug.Log(storage.bubbles[0].buildings.GetBuildingType(2));
    }

    void Update()
    {

    }
}
