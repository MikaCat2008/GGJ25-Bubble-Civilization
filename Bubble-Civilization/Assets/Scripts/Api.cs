using BubbleApi;
using UnityEngine;


public class Api : MonoBehaviour
{
    void Awake()
    {
        GlobalStorage.Initialize();

        //GlobalStorage.systems.house.StartBuilding(
        //    0, GlobalStorage.storage.bubbles[0]
        //);

        GlobalStorage.buildingUpdater.OnBreak += (Building building, Bubble bubble) =>
        {
            Building activeBuilding = WindowManager.GetActiveBuilding();

            if (building == activeBuilding)
                WindowManager.SetErrorMessage("");
        };
        GlobalStorage.buildingUpdater.OnRepaired += (Building building, Bubble bubble) =>
        {
            Building activeBuilding = WindowManager.GetActiveBuilding();

            if (building == activeBuilding)
                WindowManager.SetErrorMessage("");
        };
    }

    private void Start()
    {
        Building building = GlobalStorage.storage.currentBubble.buildings.GetBuilding(0);

        WindowManager.OpenBuildingMenu(building);
    }

    void FixedUpdate()
    {
        GlobalStorage.resourcesUpdater.Update();

        if (GlobalStorage.storage.timer.speed == 0)
            return;

        foreach (Bubble bubble in GlobalStorage.storage.bubbles.Values)
            GlobalStorage.buildingUpdater.Update(bubble);

        GlobalStorage.storage.timer.Tick();
    }
}
