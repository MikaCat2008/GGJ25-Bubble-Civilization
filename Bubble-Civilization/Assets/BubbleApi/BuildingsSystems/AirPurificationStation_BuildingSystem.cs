namespace BubbleApi
{
    public class AirPurificationStation_BuildingSystem : BuildingSystem
    {
        public override void OnUpdate(Building building, Bubble bubble)
        {
            this.Purify(building, bubble);
        }

        public void StartBuilding(int id, Bubble bubble)
        {
            this.actionUpdater.ProcessAction(bubble, ActionType.AirPurificationStation_Build);
            
            this.buildingUpdater.StartBuilding(id, bubble, BuildingType.AirPurificationStation);
            bubble.buildings.SetBuildingType(id, BuildingType.Building);
        }

        public override Building Build(int id, Bubble bubble)
        {
            Building airPurificationStation = this.Build(id, bubble, BuildingType.AirPurificationStation);

            airPurificationStation.data = new AirPurificationStation_BuildingData();

            return airPurificationStation;
        }

        public void SetCapacity(Building building, int capacity)
        {
            AirPurificationStation_BuildingData data = this.GetData<AirPurificationStation_BuildingData>(building, BuildingType.AirPurificationStation);

            data.capacity = capacity;
        }

        public void Hire(Building building, Bubble bubble)
        {
            AirPurificationStation_BuildingData data = this.GetData<AirPurificationStation_BuildingData>(building, BuildingType.AirPurificationStation);

            if (data.requireRepair)
                throw new BubbleApiException(
                    BubbleApiExceptionType.RequireRepair
                );

            if (data.count >= data.capacity)
                throw new BubbleApiException(
                    BubbleApiExceptionType.BuildingIsFull
                );

            this.actionUpdater.ProcessAction(bubble, ActionType.AirPurificationStation_Hire);

            data.count += 1;
        }

        public void Purify(Building building, Bubble bubble)
        {
            AirPurificationStation_BuildingData data = this.GetData<AirPurificationStation_BuildingData>(building, BuildingType.AirPurificationStation);

            if (data.requireRepair)
                throw new BubbleApiException(
                    BubbleApiExceptionType.RequireRepair
                );

            this.actionUpdater.ProcessAction(bubble, ActionType.AirPurificationStation_Update, building);
            
            bubble.resources.pollution -= 5 * data.count;
        }

        public override void RepairBuilding(Building building, Bubble bubble)
        {
            this.actionUpdater.ProcessAction(bubble, ActionType.AirPurificationStation_Repair);

            base.RepairBuilding(building, bubble);
        }

        public override void Destroy(Building building, Bubble bubble)
        {
            AirPurificationStation_BuildingData data = this.GetData<AirPurificationStation_BuildingData>(building, BuildingType.AirPurificationStation);

            bubble.resources.freePopulation += data.count;
            base.Destroy(building, bubble);
        }

        public override string BuildingToString(Building building)
        {
            AirPurificationStation_BuildingData data = this.GetData<AirPurificationStation_BuildingData>(building, BuildingType.AirPurificationStation);

            string brokenText = this.BrokenText(building);

            return brokenText + $"Очисна споруда[{building.id}]<працівники={data.count}/{data.capacity}>";
        }
    }
}
