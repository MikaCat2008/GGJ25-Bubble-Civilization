using UnityEngine;
using UnityEngine.EventSystems;


public class BuildingMenuChoice_Header : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] GameObject buildingMenuChoiceElement;

    private BuildingMenuChoice buildingMenuChoice;

    void Start()
    {
        this.buildingMenuChoice = this.buildingMenuChoiceElement.GetComponent<BuildingMenuChoice>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        this.buildingMenuChoice.OnHeaderDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.buildingMenuChoice.OnHeaderUp();
    }
}
