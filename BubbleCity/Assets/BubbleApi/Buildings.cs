namespace BubbleApi
{
    public enum BuildingType
    {
        Empty = 0,
        OxygenFactory = 1
    }

    public class BuildingsContainer
    {
        BuildingType[] container;

        public BuildingsContainer(byte count)
        {
            this.container = new BuildingType[count];

            for (byte i = 0; i < count; i++)
            {
                this.container[i] = BuildingType.Empty;
            }
        }

        public BuildingType GetBuildingType(byte id)
        {
            return this.container[id];
        }

        public void SetBuildingType(byte id, BuildingType type)
        {
            this.container[id] = type;
        }
    }
}
