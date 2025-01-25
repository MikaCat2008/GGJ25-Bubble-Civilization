using BubbleApi;
using UnityEngine;


public enum BuildingStatus
{
    Ok, Broken
}


public static class BuildingPlacementUI
{
    public static HousePlacement placement;
    public static BuildingType? currentBuildingType;

    public static void SetBuildingType(int id, BuildingType type)
    {
        BuildingPlacementUI.placement.SetBuildingType(id, type);
    }

    public static void SetBuildingStatus(int id, BuildingStatus status)
    {
        BuildingPlacementUI.placement.SetBuildingStatus(id, status);
    }

    public static void SetCurrentBuildingType(int id)
    {
        if (BuildingPlacementUI.currentBuildingType == null)
            return;

        BuildingPlacementUI.placement.SetBuildingType(
            id, (BuildingType)BuildingPlacementUI.currentBuildingType
        );
    }
}


public class HousePlacement : MonoBehaviour
{
    [SerializeField] GameObject place1;
    [SerializeField] GameObject place2;
    [SerializeField] GameObject place3;
    [SerializeField] GameObject place4;
    [SerializeField] GameObject place5;
    [SerializeField] GameObject place6;
    [SerializeField] GameObject place7;
    [SerializeField] GameObject place8;

    [SerializeField] Sprite sprite_building;
    [SerializeField] Sprite sprite_house;
    [SerializeField] Sprite sprite_mine;
    [SerializeField] Sprite sprite_greenHouse;
    [SerializeField] Sprite sprite_powerStation;
    [SerializeField] Sprite sprite_shipDock;
    [SerializeField] Sprite sprite_airPurificationStation;

    void Start()
    {
        BuildingPlacementUI.placement = this;
    }

    public void SetBuildingType(int id, BuildingType type)
    {
        GameObject place = this.GetPlacePoint(id);

        Sprite sprite;

        if (type == BuildingType.Building)
            sprite = this.sprite_building;
        else if (type == BuildingType.House)
            sprite = this.sprite_house;
        else if (type == BuildingType.Mine)
            sprite = this.sprite_mine;
        else if (type == BuildingType.PowerStation)
            sprite = this.sprite_powerStation;
        else if (type == BuildingType.GreenHouse)
            sprite = this.sprite_greenHouse;
        else if (type == BuildingType.ShipDock)
            sprite = this.sprite_shipDock;
        else if (type == BuildingType.AirPurificationStation)
            sprite = this.sprite_airPurificationStation;
        else
            sprite = null;

        HousePlacePoint placePoint = place.GetComponent<HousePlacePoint>();
        placePoint.SetBuildingSprite(sprite);
    }

    public void SetBuildingStatus(int id, BuildingStatus status)
    {
        GameObject place = this.GetPlacePoint(id);
        HousePlacePoint placePoint = place.GetComponent<HousePlacePoint>();

        placePoint.SetBuildingStatus(status);
    }

    public GameObject GetPlacePoint(int id)
    {
        if (id == 1)
            return this.place1;
        else if (id == 2)
            return this.place2;
        else if (id == 3)
            return this.place3;
        else if (id == 4)
            return this.place4;
        else if (id == 5)
            return this.place5;
        else if (id == 6)
            return this.place6;
        else if (id == 7)
            return this.place7;
        else if (id == 8)
            return this.place8;
        return null;
    }
}
