using TMPro;
using BubbleApi;
using UnityEngine;


public class BuildingMenu_ShipDock : BuildingMenu
{
    [SerializeField] TMP_Text modeText;

    public override void Show()
    {
        Building building = WindowManager.GetActiveBuilding();
        ShipDock_BuildingData data = (ShipDock_BuildingData)building.data;

        this.modeText.text = this.DockModeToString(data.GetDockMode());
        this.SetErrorMessage("");
        this.UpdateProperties();

        base.Show();
    }

    public void OnHire()
    {
        Bubble bubble = GlobalStorage.storage.currentBubble;
        Building building = WindowManager.GetActiveBuilding();

        try
        {
            GlobalStorage.systems.shipDock.Hire(building, bubble);
        }
        catch (BubbleApiException exception)
        {
            if (exception.type == BubbleApiExceptionType.BuildingIsFull)
                this.SetErrorMessage("В доці немає місця!");
            else if (exception.type == BubbleApiExceptionType.NotEnoughResources)
                this.SetErrorMessage("Не вистачає ресурсів!");
            else
                this.SetErrorMessage(exception.type.ToString());
        }

        this.UpdateProperties();
    }

    public void OnBuild()
    {
        Bubble bubble = GlobalStorage.storage.currentBubble;
        Building building = WindowManager.GetActiveBuilding();

        try
        {
            GlobalStorage.systems.shipDock.BuildShip(building, bubble);
        }
        catch (BubbleApiException exception)
        {
            if (exception.type == BubbleApiExceptionType.BuildingIsFull)
                this.SetErrorMessage("В доці немає місця!");
            else if (exception.type == BubbleApiExceptionType.NotEnoughResources)
                this.SetErrorMessage("Не вистачає ресурсів!");
            else
                this.SetErrorMessage(exception.type.ToString());
        }

        this.UpdateProperties();
    }

    public void OnChangeMode()
    {
        Building building = WindowManager.GetActiveBuilding();
        ShipDock_BuildingData data = (ShipDock_BuildingData)building.data;

        DockMode dockMode = data.GetDockMode();

        if (dockMode == DockMode.Free)
            dockMode = DockMode.Exploration;
        else if (dockMode == DockMode.Exploration)
            dockMode = DockMode.Transfer;
        else if (dockMode == DockMode.Transfer)
            dockMode = DockMode.Free;

        data.SetDockMode(dockMode);
        this.modeText.text = this.DockModeToString(dockMode);
    }

    private void UpdateProperties() 
    {
        Building building = WindowManager.GetActiveBuilding();
        ShipDock_BuildingData data = (ShipDock_BuildingData)building.data;

        this.SetProperties(
            $"Кількість працівників: {data.count} / {data.capacity}\n" +
            $"Кількість працівників: {data.ships} / {data.shipsCapacity}"
        );
    }

    private string DockModeToString(DockMode mode)
    {
        if (mode == DockMode.Exploration)
            return "Дослідження";
        else if (mode == DockMode.Transfer)
            return "Доставка";
        return "Вільна";
    }
}
