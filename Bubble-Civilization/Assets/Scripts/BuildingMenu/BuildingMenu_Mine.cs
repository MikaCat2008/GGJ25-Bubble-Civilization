using TMPro;
using BubbleApi;
using UnityEngine;


public class BuildingMenu_Mine : BuildingMenu
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
            GlobalStorage.systems.mine.Hire(building, bubble);
        }
        catch (BubbleApiException exception)
        {
            if (exception.type == BubbleApiExceptionType.BuildingIsFull)
                this.SetErrorMessage("В шахті немає місця!");
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
        Mine_BuildingData data = (Mine_BuildingData)building.data;

        MiningMode miningMode = data.GetMiningMode();

        if (miningMode == MiningMode.Free)
        { 
            this.modeText.text = "Паливо";

            miningMode = MiningMode.Fuel;
        }
        else if (miningMode == MiningMode.Fuel)
        {
            this.modeText.text = "Матеріали";

            miningMode = MiningMode.Materials;
        }
        else if (miningMode == MiningMode.Materials)
        {
            this.modeText.text = "Вільна";
            
            miningMode = MiningMode.Free;
        }

        data.SetMiningMode(miningMode);
    }

    private void UpdateProperties() 
    {
        Building building = WindowManager.GetActiveBuilding();
        Mine_BuildingData data = (Mine_BuildingData)building.data;

        this.SetProperties(
            $"Кількість працівників: {data.count} / {data.capacity}"
        );
    }
}
