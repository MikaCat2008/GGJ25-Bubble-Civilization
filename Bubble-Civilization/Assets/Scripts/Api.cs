using BubbleApi;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Api : MonoBehaviour
{
    void Awake()
    {
        GlobalStorage.initialized = false;
        GlobalStorage.Initialize();

        GlobalStorage.buildingUpdater.OnBuildingDone += (Building building, Bubble bubble) =>
        {
            Building activeBuilding = WindowManager.GetActiveBuilding();
            BuildingType type = building.GetBuildingType();

            if (type == BuildingType.House)
                GlobalStorage.systems.house.SetCapacity(building, 20);
            else if (type == BuildingType.Mine)
                GlobalStorage.systems.mine.SetCapacity(building, 10);
            else if (type == BuildingType.PowerStation)
                GlobalStorage.systems.powerStation.SetCapacity(building, 10);
            else if (type == BuildingType.GreenHouse)
                GlobalStorage.systems.greenHouse.SetCapacity(building, 10);
            else if (type == BuildingType.ShipDock)
                GlobalStorage.systems.shipDock.SetCapacity(building, 10);
            else if (type == BuildingType.AirPurificationStation)
                GlobalStorage.systems.airPurificationStation.SetCapacity(building, 10);

            BuildingPlacementUI.SetBuildingType(building.id, building.GetBuildingType());
        };
        GlobalStorage.buildingUpdater.OnBreak += (Building building, Bubble bubble) =>
        {
            Debug.Log(GlobalStorage.systems.GetBuildingSystem(building.GetBuildingType()).BuildingToString(building));

            Building activeBuilding = WindowManager.GetActiveBuilding();

            if (building == activeBuilding)
                WindowManager.SetErrorMessage("");

            BuildingPlacementUI.SetBuildingStatus(building.id, BuildingStatus.Broken);
        };
        GlobalStorage.buildingUpdater.OnRepaired += (Building building, Bubble bubble) =>
        {
            Building activeBuilding = WindowManager.GetActiveBuilding();

            if (building == activeBuilding)
                WindowManager.SetErrorMessage("");

            BuildingPlacementUI.SetBuildingStatus(building.id, BuildingStatus.Ok);
        };
        GlobalStorage.buildingUpdater.OnDestroyed += (Building building, Bubble bubble) =>
        {
            Building activeBuilding = WindowManager.GetActiveBuilding();

            BuildingPlacementUI.SetBuildingType(building.id, BuildingType.Empty);
        };
    }

    void FixedUpdate()
    {
        GlobalStorage.resourcesUpdater.Update();

        if (GlobalStorage.storage.timer.speed == 0)
            return;

        try
        {
            foreach (Bubble bubble in GlobalStorage.storage.bubbles.Values)
                GlobalStorage.buildingUpdater.Update(bubble);
        }
        catch (BubbleApiException exception)
        {
            GlobalStorage.Reinitialize();

            Debug.Log(exception.type);

            SceneManager.LoadScene("MainMenuScene");

            return;
        }

        GlobalStorage.storage.timer.Tick();
    }
}
