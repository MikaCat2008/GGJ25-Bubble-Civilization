using BubbleApi;


public class BuildingMenu_PowerStation : BuildingMenu
{
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
            GlobalStorage.systems.powerStation.Hire(building, bubble);
        }
        catch (BubbleApiException exception)
        {
            if (exception.type == BubbleApiExceptionType.BuildingIsFull)
                this.SetErrorMessage("В електростанції немає місця!");
            else if (exception.type == BubbleApiExceptionType.NotEnoughResources)
                this.SetErrorMessage("Не вистачає ресурсів!");
            else
                this.SetErrorMessage(exception.type.ToString());
        }

        this.UpdateProperties();
    }

    private void UpdateProperties() 
    {
        Building building = WindowManager.GetActiveBuilding();
        PowerStation_BuildingData data = (PowerStation_BuildingData)building.data;

        this.SetProperties(
            $"Кількість працівників: {data.count} / {data.capacity}"
        );
    }
}
