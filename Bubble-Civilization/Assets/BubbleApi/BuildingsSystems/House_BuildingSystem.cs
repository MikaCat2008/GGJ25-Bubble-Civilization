namespace BubbleApi
{
    public class House_BuildingSystem : BuildingSystem
    {
        public override void Update(Building building, Bubble bubble)
        {
            if (this.storage.timer.ticks % 600 == 0)
            {
                if (bubble.resources.food < 1)
                    throw new BubbleApiException(
                        BubbleApiExceptionType.NotEnoughResources
                    );

                bubble.resources.food -= 1;
            }
        }

        public Building Build(int id, Bubble bubble)
        {
            if (bubble.resources.food < 20 || bubble.resources.materials < 30)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            Building house = this.Build(id, bubble, BuildingType.House);

            house.data = new House_BuildingData();
            bubble.resources.food -= 20;
            bubble.resources.materials -= 30;

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

            if (bubble.resources.food < 5)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            data.count += 1;
            bubble.resources.food -= 5;
            bubble.resources.population += 1;
            bubble.resources.freePopulation += 1;
        }

        public void RepairBuilding(Building building, Bubble bubble)
        {
            if (bubble.resources.food < 10 || bubble.resources.materials < 15)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            this.RepairBuilding(building);
            bubble.resources.food -= 10;
            bubble.resources.materials -= 15;
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

            return brokenText + $"Будинок<населення={data.count}/{data.capacity}>";
        }
    }
}
