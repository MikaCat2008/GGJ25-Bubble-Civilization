namespace BubbleApi
{
    public enum GeneratingMode
    {
        Free,
        Food,
        Oxygen,
        Materials
    }

    public class GreenHouse_BuildingData : BuildingData
    {
        public int count;
        public int capacity;

        private byte generatingMode;

        public GreenHouse_BuildingData() : base()
        {
            this.count = 0;
            this.capacity = 0;
            this.generatingMode = (byte)GeneratingMode.Free;
        }

        public void SetGeneratingMode(GeneratingMode generatingMode)
        {
            this.generatingMode = (byte)generatingMode;
        }

        public GeneratingMode GetGeneratingMode()
        {
            return (GeneratingMode)this.generatingMode;
        }
    }
}
