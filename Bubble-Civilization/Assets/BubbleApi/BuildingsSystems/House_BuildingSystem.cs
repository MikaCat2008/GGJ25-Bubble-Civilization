namespace BubbleApi
{
    public class House_BuildingSystem : BuildingSystem
    {
        public override void OnUpdate(Building building, Bubble bubble)
        {
            this.actionUpdater.ProcessAction(bubble, ActionType.House_Update, building);
        }

        public override void StartBuilding(int id, Bubble bubble)
        {
            this.actionUpdater.ProcessAction(bubble, ActionType.House_Build);

            this.buildingUpdater.StartBuilding(id, bubble, BuildingType.House);
            bubble.buildings.SetBuildingType(id, BuildingType.Building);
        }

        public override Building Build(int id, Bubble bubble)
        {
            Building house = this.Build(id, bubble, BuildingType.House);

            house.data = new House_BuildingData();

            return house;
        }

        public void SetCapacity(Building building, int capacity)
        {
            House_BuildingData data = this.GetData<House_BuildingData>(building, BuildingType.House);

            if (capacity < 0)
                throw new BubbleApiException(
                    BubbleApiExceptionType.IncorrectBuildingData
                ); 

            data.capacity = capacity;
        }

        public void Settle(Building building, Bubble bubble)
        {
            House_BuildingData data = this.GetData<House_BuildingData>(building, BuildingType.House);

            if (data.requireRepair)
                throw new BubbleApiException(
                    BubbleApiExceptionType.RequireRepair
                );

            if (data.count >= data.capacity)
                throw new BubbleApiException(
                    BubbleApiExceptionType.BuildingIsFull
                );

            this.actionUpdater.ProcessAction(bubble, ActionType.House_Settle);

            data.count += 1;
        }

        public override void RepairBuilding(Building building, Bubble bubble)
        {
            this.actionUpdater.ProcessAction(bubble, ActionType.House_Repair);

            base.RepairBuilding(building, bubble);
        }

        public override void Destroy(Building building, Bubble bubble)
        {
            House_BuildingData data = this.GetData<House_BuildingData>(building, BuildingType.House);

            bubble.resources.population -= data.count;
            base.Destroy(building, bubble);
        }

        public override string BuildingToString(Building building)
        {
            House_BuildingData data = this.GetData<House_BuildingData>(building, BuildingType.House);

            string brokenText = this.BrokenText(building);

            return brokenText + $"Будинок[{building.id}]<населення={data.count}/{data.capacity}>";
        }
    }
}
