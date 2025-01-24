using System;
using BubbleApi;
using UnityEngine;


public static class WindowManager
{
    public static Window window;

    public static void OpenBuildingMenu(Building building)
    {
        WindowManager.window.OpenBuildingMenu(building);
    }

    public static Building? GetActiveBuilding()
    {
        return WindowManager.window.activeBuilding;
    }
}


public class Window : MonoBehaviour
{
    [SerializeField] GameObject houseWindow;
    [SerializeField] GameObject mineWindow;
    [SerializeField] GameObject powerStationWindow;
    [SerializeField] GameObject greenHouseWindow;
    [SerializeField] GameObject shipDockWindow;
    [SerializeField] GameObject airPurificationStationWindow;

    public Building? activeBuilding;

    private void Awake()
    {
        WindowManager.window = this;
    }

    public void OpenBuildingMenu(Building? building)
    {
        if (building == null)
        {
            this.activeBuilding = null;

            return;
        }

        BuildingMenu buildingMenu;

        if (this.activeBuilding != null)
        {
            buildingMenu = this.GetBuildingMenu(this.activeBuilding);
            buildingMenu.Hide();
        }

        this.activeBuilding = building;

        buildingMenu = this.GetBuildingMenu(building);
        buildingMenu.Show();
    }

    private BuildingMenu GetBuildingMenu(Building building)
    {
        BuildingType buildingType = building.GetBuildingType();

        if (buildingType == BuildingType.House)
            return this.houseWindow.GetComponent<BuildingMenu_House>();
        //else if (buildingType == BuildingType.Mine)
        //    return this.mineWindow.GetComponent<BuildingMenu_Mine>();
        //else if (buildingType == BuildingType.PowerStation)
        //    return this.powerStationWindow.GetComponent<BuildingMenu_PowerStation>();
        //else if (buildingType == BuildingType.GreenHouse)
        //    return this.greenHouseWindow.GetComponent<BuildingMenu_GreenHouse>();
        //else if (buildingType == BuildingType.ShipDock)
        //    return this.shipDockWindow.GetComponent<BuildingMenu_ShipDock>();
        //else if (buildingType == BuildingType.AirPurificationStation)
        //    return this.airPurificationStationWindow.GetComponent<BuildingMenu_AirPurification>();
        else
            throw new Exception();
    }
}
