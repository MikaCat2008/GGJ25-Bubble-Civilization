using System;


namespace BubbleApi
{
    public enum BubbleApiExceptionType
    {
        AnotherBuildingType,
        BuildingIsFull,
        TooMuchPollution,
        LackOfOxygen,
        LackOfPopulation,
        NotEnoughResources,
        IncorrectBuildingData,
        RequireRepair
    }

    public class BubbleApiException : Exception 
    {
        public BubbleApiExceptionType type;

        public BubbleApiException(BubbleApiExceptionType type) 
            : base(type.ToString())
        {
            this.type = type;
        }
    }
}
