namespace BubbleApi
{
    public class ShipDock_BuildingSystem : BuildingSystem
    {
        public override void OnUpdate(Building building, Bubble bubble)
        {
            this.actionUpdater.ProcessAction(bubble, ActionType.ShipDock_Update, building);
        }

        public override void StartBuilding(int id, Bubble bubble)
        {
            this.actionUpdater.ProcessAction(bubble, ActionType.ShipDock_Build);

            this.buildingUpdater.StartBuilding(id, bubble, BuildingType.ShipDock);
            bubble.buildings.SetBuildingType(id, BuildingType.Building);
        }

        public override Building Build(int id, Bubble bubble)
        {
            Building shipDock = this.Build(id, bubble, BuildingType.ShipDock);

            shipDock.data = new ShipDock_BuildingData();

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

            this.actionUpdater.ProcessAction(bubble, ActionType.ShipDock_Hire);

            data.count += 1;
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

            this.actionUpdater.ProcessAction(bubble, ActionType.ShipDock_BuildShip);

            data.ships += 1;
        }

        public override void RepairBuilding(Building building, Bubble bubble)
        {
            this.actionUpdater.ProcessAction(bubble, ActionType.ShipDock_Repair);

            base.RepairBuilding(building, bubble);
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

            return brokenText + $"Корабельна дока[{building.id}]<режим={modeText} працівники={data.count}/{data.capacity}>";
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
