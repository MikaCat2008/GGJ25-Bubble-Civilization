namespace BubbleApi
{
    public class Mine_BuildingSystem : BuildingSystem
    {
        //public override void Update(Building building)
        //{
        //    Mine_BuildingData data = this.GetData<Mine_BuildingData>(building, BuildingType.Mine);


        //}

        public Building Build(int id, Bubble bubble)
        {
            if (bubble.resources.food < 25 || bubble.resources.materials < 40)
                throw new BubbleApiException(
                    BubbleApiExceptionType.NotEnoughResources
                );

            Building mine = this.Build(id, bubble, BuildingType.Mine);

            mine.data = new Mine_BuildingData();

            return mine;
        }

        public override string BuildingToString(Building building)
        {
            Mine_BuildingData data = this.GetData<Mine_BuildingData>(building, BuildingType.Mine);

            return $"Шахта<режим={data.type}>";
        }
    }
}
