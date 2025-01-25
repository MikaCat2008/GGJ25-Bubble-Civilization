using TMPro;
using System.Collections.Generic;
using BubbleApi;
using UnityEngine;


public class BuildingMenuChoice : MonoBehaviour
{
    [SerializeField] TMP_Text houseResources_foodText;
    [SerializeField] TMP_Text houseResources_materialsText;

    [SerializeField] TMP_Text mineResources_foodText;
    [SerializeField] TMP_Text mineResources_materialsText;

    [SerializeField] TMP_Text powerStationResources_foodText;
    [SerializeField] TMP_Text powerStationResources_materialsText;

    [SerializeField] TMP_Text greenHouseResources_foodText;
    [SerializeField] TMP_Text greenHouseResources_materialsText;

    [SerializeField] TMP_Text shipDockResources_foodText;
    [SerializeField] TMP_Text shipDockResources_materialsText;

    [SerializeField] TMP_Text airPurificationStationResources_foodText;
    [SerializeField] TMP_Text airPurificationStationResources_materialsText;

    private void Start()
    {
        Dictionary<ActionType, ResourcesContainer> actions = GlobalStorage.actionUpdater.actions;

        ResourcesContainer houseBuildResources = actions[ActionType.House_Build];
        ResourcesContainer mineBuildResources = actions[ActionType.Mine_Build];
        ResourcesContainer powerStationBuildResources = actions[ActionType.PowerStation_Build];
        ResourcesContainer greenHouseBuildResources = actions[ActionType.GreenHouse_Build];
        ResourcesContainer shipDockBuildResources = actions[ActionType.ShipDock_Build];
        ResourcesContainer airPurificationStationBuildResources = actions[ActionType.AirPurificationStation_Build];

        this.houseResources_foodText.text = (-houseBuildResources.food).ToString();
        this.houseResources_materialsText.text = (-houseBuildResources.materials).ToString();

        this.mineResources_foodText.text = (-mineBuildResources.food).ToString();
        this.mineResources_materialsText.text = (-mineBuildResources.materials).ToString();

        this.powerStationResources_foodText.text = (-powerStationBuildResources.food).ToString();
        this.powerStationResources_materialsText.text = (-powerStationBuildResources.materials).ToString();

        this.greenHouseResources_foodText.text = (-greenHouseBuildResources.food).ToString();
        this.greenHouseResources_materialsText.text = (-greenHouseBuildResources.materials).ToString();

        this.shipDockResources_foodText.text = (-shipDockBuildResources.food).ToString();
        this.shipDockResources_materialsText.text = (-shipDockBuildResources.materials).ToString();

        this.airPurificationStationResources_foodText.text = (-airPurificationStationBuildResources.food).ToString();
        this.airPurificationStationResources_materialsText.text = (-airPurificationStationBuildResources.materials).ToString();
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    private void Build(BuildingType type)
    {
        Debug.Log($"Select place for building {type}");
    }

    public void Build_House()
    {
        this.Build(BuildingType.House);
    }

    public void Build_Mine()
    {
        this.Build(BuildingType.Mine);
    }

    public void Build_PowerStation()
    {
        this.Build(BuildingType.PowerStation);
    }

    public void Build_GreenHouse()
    {
        this.Build(BuildingType.GreenHouse);
    }

    public void Build_ShipDock()
    {
        this.Build(BuildingType.ShipDock);
    }

    public void Build_AirPurificationStation()
    {
        this.Build(BuildingType.AirPurificationStation);
    }
}
