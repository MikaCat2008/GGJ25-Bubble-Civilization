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

    public static void OpenBuildingMenuChoice()
    {
        WindowManager.window.OpenBuildingMenuChoice();
    }

    public static Building? GetActiveBuilding()
    {
        return WindowManager.window.activeBuilding;
    }

    public static void SetErrorMessage(string text)
    {
        Building? building = WindowManager.GetActiveBuilding();

        if (building == null)
            return;

        BuildingMenu buildingMenu = WindowManager.window.GetBuildingMenu(building);

        buildingMenu.SetErrorMessage(text);
    }
}


public class Window : MonoBehaviour
{
    [SerializeField] GameObject buildingMenuChoiceElement;

    [SerializeField] GameObject houseWindow;
    [SerializeField] GameObject mineWindow;
    [SerializeField] GameObject powerStationWindow;
    [SerializeField] GameObject greenHouseWindow;
    [SerializeField] GameObject shipDockWindow;
    [SerializeField] GameObject airPurificationStationWindow;

    public Vector2 windowPosition;
    public Building? activeBuilding;
    public BuildingMenuChoice buildingMenuChoice;

    private void Awake()
    {
        WindowManager.window = this;

        this.windowPosition = new Vector2(200, 200);
        this.buildingMenuChoice = buildingMenuChoiceElement.GetComponent<BuildingMenuChoice>();
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

    public void OpenBuildingMenuChoice()
    {
        this.buildingMenuChoice.Show();
    }

    public BuildingMenu GetBuildingMenu(Building building)
    {
        BuildingType buildingType = building.GetBuildingType();

        if (buildingType == BuildingType.House)
            return this.houseWindow.GetComponent<BuildingMenu_House>();
        else if (buildingType == BuildingType.Mine)
            return this.mineWindow.GetComponent<BuildingMenu_Mine>();
        else if (buildingType == BuildingType.PowerStation)
            return this.powerStationWindow.GetComponent<BuildingMenu_PowerStation>();
        else if (buildingType == BuildingType.GreenHouse)
            return this.greenHouseWindow.GetComponent<BuildingMenu_GreenHouse>();
        else if (buildingType == BuildingType.ShipDock)
            return this.shipDockWindow.GetComponent<BuildingMenu_ShipDock>();
        else if (buildingType == BuildingType.AirPurificationStation)
            return this.airPurificationStationWindow.GetComponent<BuildingMenu_AirPurification>();
        else
            throw new Exception();
    }
}
