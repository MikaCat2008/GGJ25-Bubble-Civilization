#nullable enable


namespace BubbleApi
{
    public abstract class BuildingSystem
    {
        public Storage? storage;

        public abstract void Update(Building building);
        public abstract string BuildingToString(Building building);

        public void CheckType(Building building, BuildingType requiredType)
        {
            BuildingType type = building.GetBuildingType();

            if (type != requiredType)
                throw new BubbleApiException($"Building's type is {type}, but must be {requiredType}.");
        }
    }
}
