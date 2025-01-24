using BubbleApi;


public class BuildingMenu_House : BuildingMenu
{
    public override void Show()
    {
        this.SetErrorMessage("");
        this.UpdateProperties();

        base.Show();
    }

    public void OnSettle()
    {
        Bubble bubble = GlobalStorage.storage.currentBubble;
        Building building = WindowManager.GetActiveBuilding();

        try
        {
            GlobalStorage.systems.house.Settle(building, bubble);
        }
        catch (BubbleApiException exception)
        {
            if (exception.type == BubbleApiExceptionType.BuildingIsFull)
                this.SetErrorMessage("В будинку немає місця!");
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
        House_BuildingData data = (House_BuildingData)building.data;

        this.SetProperties(
            $"Кількість жителів: {data.count} / {data.capacity}"
        );
    }
}
