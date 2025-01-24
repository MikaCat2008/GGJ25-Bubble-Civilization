using System;
using UnityEngine;
using BubbleApi;
using System.Collections.Generic;

public class Resource
{
    //for UI to subscribe and update
    public event Action<int> OnResourceQuantityChanged;

    public int Quantity = 0;
    public int Capacity = 10000;

    public ResourceType Type;
    public string Name;
    public string IconPath;

    public string ToDebugString()
    {
        return Name + ": " + Quantity.ToString();
    }

    public void IncreaseQuantity(int Amount)
    {
        this.Quantity += Amount;
        OnResourceQuantityChanged?.Invoke(this.Quantity);
    }

    public void DecreaseQuantity(int Amount)
    {
        this.Quantity -= Amount;
        OnResourceQuantityChanged?.Invoke(this.Quantity);
    }


}

public class PopulationResource : Resource
{
    private float dieFromSufficationCoof = 0.1f;
    private float dieFromHungerCoof = 0.1f;

    public PopulationResource()
    {
        Type = ResourceType.Population;
        Name = "Населення";
        IconPath = "Assets/Resources/Images/resources/PopulationIcon.png";
    }

    public void Suffocate()
    {
        float deadPeopleAmount = dieFromSufficationCoof*Quantity;
        int changeAmount = (int)MathF.Floor(deadPeopleAmount);
        if (Quantity > changeAmount)
        {
            this.DecreaseQuantity(changeAmount);
        }
    }

    public void Starve()
    {
        float deadPeopleAmount = dieFromHungerCoof * Quantity;
        int changeAmount = (int)MathF.Floor(deadPeopleAmount);
        if (Quantity > changeAmount)
        {
            this.DecreaseQuantity(changeAmount);
        }
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