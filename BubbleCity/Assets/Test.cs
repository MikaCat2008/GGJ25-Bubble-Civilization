using BubbleApi;
using UnityEngine;


public class Test : MonoBehaviour
{
    Bubble bubble;

    void Start()
    {
        ResourcesContainer resources = new ResourcesContainer(
            100, 100, 100, 100, 100, 100, 100
        );
        BuildingsContainer buildings = new BuildingsContainer(3);

        this.bubble = new Bubble(resources, buildings);

        Debug.Log(this.bubble.buildings.GetBuildingType(2));

        this.bubble.buildings.SetBuildingType(2, BuildingType.OxygenFactory);

        Debug.Log(this.bubble.buildings.GetBuildingType(2));
    }

    void Update()
    {

    }
}
