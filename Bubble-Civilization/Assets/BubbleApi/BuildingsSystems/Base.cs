#nullable enable


namespace BubbleApi
{
    public class BuildingSystem
    {
        public Storage? storage;

        public virtual void Update(Building building) { }
        
        public virtual void BreakBuilding(Building building)
        {
            building.data.requireRepair = true;
        }
        public virtual void RepairBuilding(Building building, Bubble bubble)
        {
            building.data.requireRepair = false;
        }

        public virtual string BuildingToString(Building building) 
        {
            return "Пусто";
        }

        public string BrokenText(Building building)
        {
            return building.data.requireRepair ? "(Зламано) " : "";
        }

        public T GetData<T>(Building building, BuildingType type) where T : BuildingData
        {
            this.CheckType(building, type);

            return (T)building.data;
        }

        public void CheckType(Building building, BuildingType requiredType)
        {
            BuildingType type = building.GetBuildingType();

            if (type != requiredType)
                throw new BubbleApiException(
                    BubbleApiExceptionType.AnotherBuildingType
                );
        }
    }
}
