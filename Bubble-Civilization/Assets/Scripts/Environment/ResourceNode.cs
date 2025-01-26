using UnityEngine;
using System;

public class ResourceNode : MonoBehaviour
{
    //Затонулий корабель
    //Поклади мінералів
    //Закинута база

    public event Action OnResourceNodeClicked;

    public void OnMouseDown()
    {
        Debug.Log("You clicked on resource.", this);
        OnResourceNodeClicked?.Invoke();
    }
}
