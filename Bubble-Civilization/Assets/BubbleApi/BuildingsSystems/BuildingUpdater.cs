namespace BubbleApi
{
    public class BuildingUpdater
    {
        public SystemsContainer systems;

        public BuildingUpdater(SystemsContainer systems)
        {
            this.systems = systems;
        }

        private bool UpdateBreaking(Building building)
        {
            bool status = false;

            if (status)
            {
                this.systems.building.BreakBuilding(building);
            }

            return status;
        }

        private void UpdateBuilding(Building building, Bubble bubble)
        {
            BuildingType type = building.GetBuildingType();

            if (type == BuildingType.Empty)
                return;

            if (this.UpdateBreaking(building))
                return;

            BuildingSystem system = this.systems.GetBuildingSystem(type);
            system.Update(building, bubble);
        }

        public void Update(Bubble bubble)
        {
            foreach (Building building in bubble.buildings.container.Values)
            {
                try
                {
                   this.UpdateBuilding(building, bubble);
                }
                catch (BubbleApiException exception)
                {
                    if (exception.type == BubbleApiExceptionType.NotEnoughResources)
                    {
                        this.systems.building.BreakBuilding(building);
                    }
                }
            }

            if (bubble.resources.oxygen <= 0)
                throw new BubbleApiException(
                    BubbleApiExceptionType.LackOfOxygen
                );

            if (bubble.resources.pollution >= 1000)
                throw new BubbleApiException(
                    BubbleApiExceptionType.TooMuchPollution
                );

            if (bubble.resources.population <= 0)
                throw new BubbleApiException(
                    BubbleApiExceptionType.LackOfPopulation
                );
        }
    }
}
