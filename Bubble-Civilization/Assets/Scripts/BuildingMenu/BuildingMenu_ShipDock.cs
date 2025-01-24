using TMPro;
using BubbleApi;
using UnityEngine;


public class BuildingMenu_ShipDock : BuildingMenu
{
    [SerializeField] TMP_Text modeText;

    public override void Show()
    {
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
        ShipDock_BuildingData data = (ShipDock_BuildingData)building.data;

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
        {
            this.modeText.text = "Дослідження";

            dockMode = DockMode.Exploration;
        }
        else if (dockMode == DockMode.Exploration)
        {
            this.modeText.text = "Доставка";

            dockMode = DockMode.Transfer;
        }
        else if (dockMode == DockMode.Transfer)
        {
            this.modeText.text = "Вільна";

            dockMode = DockMode.Free;
        }

        data.SetDockMode(dockMode);
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
}
