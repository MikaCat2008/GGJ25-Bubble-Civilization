using UnityEngine;
using UnityEngine.EventSystems;


public class BuildingMenu_Header : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] GameObject buildingMenuElement;

    private BuildingMenu buildingMenu;

    void Start()
    {
        this.buildingMenu = this.buildingMenuElement.GetComponent<BuildingMenu>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.buildingMenu.OnHeaderDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.buildingMenu.OnHeaderUp();
    }
}
