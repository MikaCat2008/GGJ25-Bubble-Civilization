using BubbleApi;
using UnityEngine;


public class Api : MonoBehaviour
{
    public void Awake()
    {
        GlobalStorage.Initialize();

        Debug.Log(GlobalStorage.storage.bubbles[0].buildings.container[0].GetBuildingType());

        GlobalStorage.systems.house.StartBuilding(
            0, GlobalStorage.storage.bubbles[0]
        );

        GlobalStorage.buildingUpdater.OnBuildingDone += (Building building, Bubble bubble) =>
        {
            BuildingSystem system = GlobalStorage.systems.GetBuildingSystem(building.GetBuildingType());

            Debug.Log($"Будівля {system.BuildingToString(building)} добудувалась !");
        };
        GlobalStorage.buildingUpdater.OnBreak += (Building building, Bubble bubble) =>
        {
            BuildingSystem system = GlobalStorage.systems.GetBuildingSystem(building.GetBuildingType());

            Debug.Log($"Будівля {system.BuildingToString(building)} зламалась !");
        };
    }

    public void FixedUpdate()
    {
        if (GlobalStorage.storage.timer.speed == 0)
            return;

        foreach (Bubble bubble in GlobalStorage.storage.bubbles.Values)
            GlobalStorage.buildingUpdater.Update(bubble);

        GlobalStorage.storage.timer.Tick();
    }
}
