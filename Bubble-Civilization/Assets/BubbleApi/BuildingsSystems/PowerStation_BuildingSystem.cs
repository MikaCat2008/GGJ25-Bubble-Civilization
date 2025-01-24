namespace BubbleApi
{
    public class PowerStation_BuildingSystem : BuildingSystem
    {
        public override void OnUpdate(Building building, Bubble bubble)
        {
            this.GenerateElectricity(building, bubble);
        }

        public void StartBuilding(int id, Bubble bubble)
        {
            this.actionUpdater.ProcessAction(bubble, ActionType.PowerStation_Build);
            
            this.buildingUpdater.StartBuilding(id, bubble, BuildingType.PowerStation);
            bubble.buildings.SetBuildingType(id, BuildingType.Building);
        }

        public override Building Build(int id, Bubble bubble)
        {
            Building powerStation = this.Build(id, bubble, BuildingType.PowerStation);

            powerStation.data = new PowerStation_BuildingData();

            return powerStation;
        }

        public void SetCapacity(Building building, int capacity)
        {
            PowerStation_BuildingData data = this.GetData<PowerStation_BuildingData>(building, BuildingType.PowerStation);

            data.capacity = capacity;
        }

        public void Hire(Building building, Bubble bubble)
        {
            PowerStation_BuildingData data = this.GetData<PowerStation_BuildingData>(building, BuildingType.PowerStation);

            if (data.requireRepair)
                throw new BubbleApiException(
                    BubbleApiExceptionType.RequireRepair
                );

            if (data.count >= data.capacity)
                throw new BubbleApiException(
                    BubbleApiExceptionType.BuildingIsFull
                );

            this.actionUpdater.ProcessAction(bubble, ActionType.PowerStation_Hire);

            data.count += 1;
        }

        public void GenerateElectricity(Building building, Bubble bubble)
        {
            PowerStation_BuildingData data = this.GetData<PowerStation_BuildingData>(building, BuildingType.PowerStation);

            if (data.requireRepair)
                throw new BubbleApiException(
                    BubbleApiExceptionType.RequireRepair
                );

            this.actionUpdater.ProcessAction(bubble, ActionType.PowerStation_Build, building);

            bubble.resources.fuel += 2 * data.count;
        }

        public void RepairBuilding(Building building, Bubble bubble)
        {
            this.actionUpdater.ProcessAction(bubble, ActionType.PowerStation_Repair);
            
            this.RepairBuilding(building);
        }

        public override void Destroy(Building building, Bubble bubble)
        {
            PowerStation_BuildingData data = this.GetData<PowerStation_BuildingData>(building, BuildingType.PowerStation);

            bubble.resources.freePopulation += data.count;
            base.Destroy(building, bubble);
        }

        public override string BuildingToString(Building building)
        {
            PowerStation_BuildingData data = this.GetData<PowerStation_BuildingData>(building, BuildingType.PowerStation);

            string brokenText = this.BrokenText(building);

            return brokenText + $"Електростанція[{building.id}]<працівники={data.count}/{data.capacity}>";
        }
    }
}
