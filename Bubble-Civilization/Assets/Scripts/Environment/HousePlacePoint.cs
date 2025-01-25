using BubbleApi;
using UnityEngine;

public class HousePlacePoint : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] GameObject building;

    void Start()
    {
        sprite.sortingOrder = 10;
        SetInitAlpha();
    }

    private void SetInitAlpha()
    {
        sprite.color = new Color(1,1,1,0.4f);
    }

    public void OnMouseDown()
    {
        Building building = GlobalStorage.storage.currentBubble.buildings.GetBuilding(this.id);
        BuildingType currentType = building.GetBuildingType();

        if (currentType != BuildingType.Empty)
        {
            if (currentType != BuildingType.Building)
                WindowManager.OpenBuildingMenu(building);

            return;
        }

        if (BuildingPlacementUI.currentBuildingType == null)
            return;

        try
        {
            currentType = (BuildingType)BuildingPlacementUI.currentBuildingType;

            GlobalStorage.systems.GetBuildingSystem(currentType).StartBuilding(
                this.id, GlobalStorage.storage.currentBubble
            );

            BuildingPlacementUI.SetBuildingType(this.id, BuildingType.Building);
        }
        catch (BubbleApiException exception) 
        {
            Debug.Log(exception);
        }
    }
    public void OnMouseEnter()
    {
        Highlight();
    }
    public void OnMouseExit()
    {
        UnHighlight();
    }

    private void Highlight()
    {
        sprite.color = new Color(1, 1, 1, 1);
    }

    private void UnHighlight()
    {
        sprite.color = new Color(1, 1, 1, 0.4f);
    }

    public void SetBuildingSprite(Sprite sprite)
    {
        SpriteRenderer spriteRenderer = this.building.GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = sprite;
    }
}
