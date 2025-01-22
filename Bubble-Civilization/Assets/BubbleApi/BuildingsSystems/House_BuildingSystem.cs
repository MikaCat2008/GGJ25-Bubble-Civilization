namespace BubbleApi
{
    public class House_BuildingSystem : BuildingSystem
    {
        //public override void Update(Building building)
        //{
        //    House_BuildingData data = this.GetData<House_BuildingData>(building, BuildingType.House);


        //}

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
                    BubbleApiExceptionType.HouseIsFull
                );

            if (bubble.resources.food < 5)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            data.count += 1;
            bubble.resources.food -= 5;
        }

        public override void RepairBuilding(Building building, Bubble bubble)
        {
            House_BuildingData data = this.GetData<House_BuildingData>(building, BuildingType.House);

            if (bubble.resources.food < 2)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            base.RepairBuilding(building, bubble);
            bubble.resources.food -= 2;
        }

        public override string BuildingToString(Building building)
        {
            this.CheckType(building, BuildingType.House);
            House_BuildingData data = (House_BuildingData)building.data;

            string brokenText = this.BrokenText(building);

            return brokenText + $"Будинок<населення={data.count}/{data.capacity}>";
        }
    }
}
