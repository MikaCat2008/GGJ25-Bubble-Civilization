using System;
using UnityEngine;
using BubbleApi;
using System.Collections.Generic;

public class Resource
{
    //for UI to subscribe and update
    public event Action<int> OnResourceAmountChanged;

    public int Quantity = 0;
    public int Capacity = 10000;

    public ResourceType Type;
    public string Name;
    public string IconPath;

    public string ToDebugString()
    {
        return Name + ": " + Quantity.ToString();
    }
}

public class PopulationResource : Resource
{
    public PopulationResource()
    {
        Type = ResourceType.Population;
        Name = "Населення";
        IconPath = "Assets/Resources/Images/resources/PopulationIcon.png";
    }
}

public class FoodResource : Resource
{
    public FoodResource()
    {
        Type = ResourceType.Food;
        Name = "Провіант";
        IconPath = "Assets/Resources/Images/resources/FoodIcon.png";
    }
}

public class OxygenResource : Resource
{
    public OxygenResource()
    {
        Type = ResourceType.Oxygen;
        Name = "Кисень";
        IconPath = "Assets/Resources/Images/resources/OxygenIcon.png";
    }
}

public class MaterialsResource : Resource
{
    public MaterialsResource()
    {
        Type = ResourceType.Materials;
        Name = "Матеріали";
        IconPath = "Assets/Resources/Images/resources/MaterialIcon.png";
    }
}

public class FuelResource : Resource
{
    public FuelResource()
    {
        Type = ResourceType.Fuel;
        Name = "Паливо";
        IconPath = "Assets/Resources/Images/resources/FuelIcon.png";
    }
}