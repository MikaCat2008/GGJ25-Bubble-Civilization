using UnityEngine;
using System;

public class HousePlacePoint : MonoBehaviour
{
    public event Action<HousePlacePoint> OnPlacePointClicked;

    [SerializeField] SpriteRenderer sprite;
    
    void Start()
    {
        sprite.sortingOrder = 10;
        SetInitAlpha();
    }

    private void SetInitAlpha()
    {
        sprite.color = new Color(1,1,1,0.4f);
    }

    public void OnMouseDown()
    {
        //Debug.Log("OnPlacePointClicked");
        OnPlacePointClicked?.Invoke(this);
    }
    public void OnMouseEnter()
    {
        //Debug.Log("OnPlacePointEntered");
        Highlight();
    }
    public void OnMouseExit()
    {
        //Debug.Log("OnPlacePointExited");
        UnHighlight();
    }

    private void Highlight()
    {
        sprite.color = new Color(1, 1, 1, 1);
    }

    private void UnHighlight()
    {
        sprite.color = new Color(1, 1, 1, 0.4f);
    }
}
