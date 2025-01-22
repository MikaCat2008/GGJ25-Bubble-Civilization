namespace BubbleApi
{
    public class House_BuildingData : BuildingData
    {
        public int count;
        public int capacity;

        public House_BuildingData() : base()
        {
            this.count = 0;
            this.capacity = 0;
        }
    }
}
