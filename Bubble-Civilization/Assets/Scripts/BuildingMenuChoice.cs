using TMPro;
using System;
using System.Collections.Generic;
using BubbleApi;
using UnityEngine;
using UnityEngine.UI;


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

    [SerializeField] GameObject buildHouse;
    [SerializeField] GameObject buildMine;
    [SerializeField] GameObject buildPowerStation;
    [SerializeField] GameObject buildGreenHouse;
    [SerializeField] GameObject buildShipDock;
    [SerializeField] GameObject buildAirPurificationStation;

    private bool holding;
    private Vector2 menuHoldPosition;
    private Vector2 mouseHoldPosition;
    private RectTransform rectTransform;

    [SerializeField] GameObject openButton;

    private void Start()
    {
        this.holding = false;
        this.menuHoldPosition = new Vector2();
        this.mouseHoldPosition = new Vector2();
        this.rectTransform = this.gameObject.GetComponent<RectTransform>();

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

    void Update()
    {
        if (this.holding)
        {
            Vector2 mousePosition = Input.mousePosition;

            this.rectTransform.position = this.menuHoldPosition + (mousePosition - this.mouseHoldPosition);
        }
    }

    public void OnHeaderDown()
    {
        this.holding = true;
        this.menuHoldPosition = this.rectTransform.position;
        this.mouseHoldPosition = Input.mousePosition;
    }

    public void OnHeaderUp()
    {
        this.holding = false;
    }
    
    public void Show()
    {
        this.gameObject.SetActive(true);
        this.openButton.SetActive(false);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
        this.openButton.SetActive(true);
     
        Image image = this.GetBuildingImage((BuildingType)BuildingPlacementUI.currentBuildingType);
        image.color = new Color(255, 255, 255);
    
        BuildingPlacementUI.currentBuildingType = null;
    }

    private void Build(BuildingType type)
    {
        Image image;

        if (BuildingPlacementUI.currentBuildingType != null)
        { 
            image = this.GetBuildingImage((BuildingType)BuildingPlacementUI.currentBuildingType);
            image.color = new Color(255, 255, 255);
        }

        BuildingPlacementUI.currentBuildingType = type;

        image = this.GetBuildingImage(type);
        image.color = new Color(0, 255, 0);
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

    private Image GetBuildingImage(BuildingType type)
    {
        GameObject imageObject;

        if (type == BuildingType.House)
            imageObject = this.buildHouse;
        else if (type == BuildingType.Mine)
            imageObject = this.buildMine;
        else if (type == BuildingType.PowerStation)
            imageObject = this.buildPowerStation;
        else if (type == BuildingType.GreenHouse)
            imageObject = this.buildGreenHouse;
        else if (type == BuildingType.ShipDock)
            imageObject = this.buildShipDock;
        else if (type == BuildingType.AirPurificationStation)
            imageObject = this.buildAirPurificationStation;
        else
            throw new Exception();

        return imageObject.GetComponent<Image>();
    }
}
