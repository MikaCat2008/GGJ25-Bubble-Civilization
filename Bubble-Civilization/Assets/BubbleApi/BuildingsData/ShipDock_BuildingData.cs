namespace BubbleApi
{
    public enum DockMode
    {
        Free,
        Exploration,
        Transfer
    }

    public class ShipDock_BuildingData : BuildingData
    {
        public int count;
        public int capacity;
        public int ships;
        public int shipsCapacity;

        private byte dockMode;

        public ShipDock_BuildingData() : base()
        {
            this.count = 0;
            this.capacity = 0;
            this.ships = 0;
            this.shipsCapacity = 0;

            this.dockMode = (byte)DockMode.Free;
        }

        public void SetDockMode(DockMode dockMode)
        {
            this.dockMode = (byte)dockMode;
        }

        public DockMode GetDockMode()
        {
            return (DockMode)this.dockMode;
        }
    }
}
