using TMPro;
using BubbleApi;
using UnityEngine;


public class BuildingMenu : MonoBehaviour
{
    [SerializeField] TMP_Text properties;
    [SerializeField] TMP_Text errorMessage;

    private bool holding;
    private Vector2 menuHoldPosition;
    private Vector2 mouseHoldPosition;
    private RectTransform rectTransform;

    void Start()
    {
        this.holding = false;
        this.menuHoldPosition = new Vector2();
        this.mouseHoldPosition = new Vector2();
        this.rectTransform = this.gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (this.holding)
        {
            Vector2 mousePosition = Input.mousePosition;

            this.rectTransform.position = this.menuHoldPosition + (mousePosition - this.mouseHoldPosition);

            WindowManager.window.windowPosition = this.rectTransform.position;
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

    public void SetProperties(string text)
    {
        this.properties.text = text;
    }

    public void SetErrorMessage(string text)
    {
        Building building = WindowManager.GetActiveBuilding();

        if (building.data.requireRepair)
            this.errorMessage.text = "Будівля зламана! Відремонтуйте.";
        else
            this.errorMessage.text = text;
    }

    public virtual void Show()
    {
        this.rectTransform = this.gameObject.GetComponent<RectTransform>();

        Vector2 windowPosition = WindowManager.window.windowPosition;
        this.rectTransform.position = windowPosition;

        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
        BuildingPlacementUI.SetBuildingStatus(
            WindowManager.GetActiveBuilding().id, BuildingStatus.Ok
        );
    }

    public void OnRepair()
    {
        Building building = WindowManager.GetActiveBuilding();

        if (!building.data.requireRepair)
            return;

        Bubble bubble = GlobalStorage.storage.currentBubble;
        BuildingSystem system = GlobalStorage.systems.GetBuildingSystem(
            building.GetBuildingType()
        );

        system.RepairBuilding(building, bubble);
    }

    public void OnDestroyClick()
    {
        Bubble bubble = GlobalStorage.storage.currentBubble;
        Building building = WindowManager.GetActiveBuilding();
        
        BuildingSystem system = GlobalStorage.systems.GetBuildingSystem(
            building.GetBuildingType()
        );

        system.Destroy(building, bubble);

        this.Hide();

        WindowManager.OpenBuildingMenu(null);
    }
}
