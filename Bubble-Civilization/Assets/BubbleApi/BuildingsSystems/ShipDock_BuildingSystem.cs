namespace BubbleApi
{
    public class ShipDock_BuildingSystem : BuildingSystem
    {
        public override void Update(Building building, Bubble bubble)
        {
            if (this.storage.timer.ticks % 1800 == 0)
            {
                if (bubble.resources.energy < 1)
                    throw new BubbleApiException(
                        BubbleApiExceptionType.NotEnoughResources
                    );

                bubble.resources.energy -= 1;
            }
        }

        public Building Build(int id, Bubble bubble)
        {
            if (bubble.resources.food < 30 || bubble.resources.materials < 100)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            Building shipDock = this.Build(id, bubble, BuildingType.ShipDock);

            shipDock.data = new ShipDock_BuildingData();
            bubble.resources.food -= 30;
            bubble.resources.materials -= 100;

            return shipDock;
        }

        public void SetDockMode(Building building, DockMode dockMode)
        {
            ShipDock_BuildingData data = this.GetData<ShipDock_BuildingData>(building, BuildingType.ShipDock);

            data.SetDockMode(dockMode);
        }

        public void SetCapacity(Building building, int capacity)
        {
            ShipDock_BuildingData data = this.GetData<ShipDock_BuildingData>(building, BuildingType.ShipDock);

            data.capacity = capacity;
        }

        public void Hire(Building building, Bubble bubble)
        {
            ShipDock_BuildingData data = this.GetData<ShipDock_BuildingData>(building, BuildingType.ShipDock);

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

        public void BuildShip(Building building, Bubble bubble)
        {
            ShipDock_BuildingData data = this.GetData<ShipDock_BuildingData>(building, BuildingType.ShipDock);

            if (data.requireRepair)
                throw new BubbleApiException(
                    BubbleApiExceptionType.RequireRepair
                );

            if (data.ships >= data.shipsCapacity)
                throw new BubbleApiException(
                    BubbleApiExceptionType.BuildingIsFull
                );

            if (bubble.resources.food < 20 || bubble.resources.materials < 50)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            data.ships += 1;
            
            bubble.resources.food -= 20;
            bubble.resources.materials-= 50;
        }

        public void RepairBuilding(Building building, Bubble bubble)
        {
            if (bubble.resources.food < 15 || bubble.resources.materials < 50)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            this.RepairBuilding(building);
            bubble.resources.food -= 15;
            bubble.resources.materials -= 50;
        }

        public override void Destroy(Building building, Bubble bubble)
        {
            ShipDock_BuildingData data = this.GetData<ShipDock_BuildingData>(building, BuildingType.ShipDock);

            bubble.resources.freePopulation += data.count;
            base.Destroy(building, bubble);
        }

        public override string BuildingToString(Building building)
        {
            ShipDock_BuildingData data = this.GetData<ShipDock_BuildingData>(building, BuildingType.ShipDock);

            DockMode dockMode = data.GetDockMode();

            string modeText = this.DockModeToString(dockMode);
            string brokenText = this.BrokenText(building);

            return brokenText + $"Корабельна дока<режим={modeText} працівники={data.count}/{data.capacity}>";
        }

        private string DockModeToString(DockMode dockMode)
        {
            if (dockMode == DockMode.Free)
                return "вільний";
            if (dockMode == DockMode.Exploration)
                return "дослідження";
            return "доставка";
        }
    }
}
