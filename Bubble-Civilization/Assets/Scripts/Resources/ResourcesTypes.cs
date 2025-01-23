using System.IO;
using UnityEngine;
using BubbleApi;
using System.Collections.Generic;

public class Resource
{
    public ResourceType Type;
    public string Name;
    public string IconPath;
}

public class FuelResource : Resource
{
    FuelResource()
    {
        Type = ResourceType.Fuel;
        Name = "Паливо";
        IconPath = "Assets/Resources/Images/resources/FuelIcon.png";
    }
}

public class PopulationResource : Resource
{
    PopulationResource()
    {
        Type = ResourceType.Population;
        Name = "Населення";
        IconPath = "Assets/Resources/Images/resources/PopulationIcon.png";
    }
}

public class FoodResource : Resource
{
    FoodResource()
    {
        Type = ResourceType.Food;
        Name = "Провіант";
        IconPath = "Assets/Resources/Images/resources/FoodIcon.png";
    }
}

public class OxygenResource : Resource
{
    OxygenResource()
    {
        Type = ResourceType.Oxygen;
        Name = "Кисень";
        IconPath = "Assets/Resources/Images/resources/OxygenIcon.png";
    }
}

public class MaterialsResource : Resource
{
    MaterialsResource()
    {
        Type = ResourceType.Materials;
        Name = "Матеріали";
        IconPath = "Assets/Resources/Images/resources/MaterialIcon.png";
    }
}

