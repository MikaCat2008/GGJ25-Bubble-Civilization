using System;
using BubbleApi;
using UnityEngine;
using UnityEngine.UI;


public class ScalePanel : MonoBehaviour
{
    [SerializeField] GameObject scale;

    private Image image;
    private RectTransform rectTransform;

    void Start()
    {
        this.image = this.scale.GetComponent<Image>();
        this.rectTransform = this.scale.GetComponent<RectTransform>();
    }

    public void SetValue(double value)
    {
        Vector2 offset = this.rectTransform.offsetMax;
        offset.y = (float)(-4 - 224 * (1 - value));

        this.rectTransform.offsetMax = offset;
    }

    public void SetColor(Color color)
    {
        this.image.color = color;
    }
}
