namespace BubbleApi
{
    public class GreenHouse_BuildingSystem : BuildingSystem
    {
        public override void Update(Building building, Bubble bubble)
        {
            if (this.storage.timer.ticks % 300 == 0)
            {
                this.Generate(building, bubble);
            }
        }

        public Building Build(int id, Bubble bubble)
        {
            if (bubble.resources.food < 5 || bubble.resources.materials < 5)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            Building greenHouse = this.Build(id, bubble, BuildingType.GreenHouse);

            greenHouse.data = new GreenHouse_BuildingData();
            bubble.resources.food -= 5;
            bubble.resources.materials -= 5;

            return greenHouse;
        }

        public void SetGeneratingMode(Building building, GeneratingMode generatingMode)
        {
            GreenHouse_BuildingData data = this.GetData<GreenHouse_BuildingData>(building, BuildingType.GreenHouse);

            data.SetGeneratingMode(generatingMode);
        }

        public void SetCapacity(Building building, int capacity)
        {
            GreenHouse_BuildingData data = this.GetData<GreenHouse_BuildingData>(building, BuildingType.GreenHouse);

            data.capacity = capacity;
        }

        public void Hire(Building building, Bubble bubble)
        {
            GreenHouse_BuildingData data = this.GetData<GreenHouse_BuildingData>(building, BuildingType.GreenHouse);

            if (data.requireRepair)
                throw new BubbleApiException(
                    BubbleApiExceptionType.RequireRepair
                );

            if (data.count >= data.capacity)
                throw new BubbleApiException(
                    BubbleApiExceptionType.BuildingIsFull
                );

            if (bubble.resources.freePopulation < 1)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            data.count += 1;
            bubble.resources.freePopulation -= 1;
        }

        public void Generate(Building building, Bubble bubble)
        {
            GreenHouse_BuildingData data = this.GetData<GreenHouse_BuildingData>(building, BuildingType.GreenHouse);

            if (data.requireRepair)
                throw new BubbleApiException(
                    BubbleApiExceptionType.RequireRepair
                );

            if (bubble.resources.energy < 1)
            {
                this.BreakBuilding(building);

                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );
            }

            GeneratingMode generatingMode = data.GetGeneratingMode();

            if (generatingMode == GeneratingMode.Food)
            {
                bubble.resources.food += 2 * data.count;
            }
            else if (generatingMode == GeneratingMode.Oxygen)
            {
                bubble.resources.oxygen += data.count;
            }
            else if (generatingMode == GeneratingMode.Materials)
            {
                bubble.resources.materials += data.count;
            }

            bubble.resources.energy -= 1;
        }

        public void RepairBuilding(Building building, Bubble bubble)
        {
            if (bubble.resources.food < 2 || bubble.resources.materials < 2)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            this.RepairBuilding(building);
            bubble.resources.food -= 2;
            bubble.resources.materials -= 2;
        }

        public override void Destroy(Building building, Bubble bubble)
        {
            GreenHouse_BuildingData data = this.GetData<GreenHouse_BuildingData>(building, BuildingType.GreenHouse);

            bubble.resources.freePopulation += data.count;
            base.Destroy(building, bubble);
        }

        public override string BuildingToString(Building building)
        {
            GreenHouse_BuildingData data = this.GetData<GreenHouse_BuildingData>(building, BuildingType.GreenHouse);

            GeneratingMode generatingMode = data.GetGeneratingMode();

            string modeText = this.GeneratingModeToString(generatingMode);
            string brokenText = this.BrokenText(building);

            return brokenText + $"Теплиця<режим={modeText} працівники={data.count}/{data.capacity}>";
        }

        private string GeneratingModeToString(GeneratingMode generatingMode)
        {
            if (generatingMode == GeneratingMode.Free)
                return "вільна";
            if (generatingMode == GeneratingMode.Food)
                return "їжа";
            if (generatingMode == GeneratingMode.Oxygen)
                return "кисень";
            return "будівельні матеріали";
        }
    }
}
