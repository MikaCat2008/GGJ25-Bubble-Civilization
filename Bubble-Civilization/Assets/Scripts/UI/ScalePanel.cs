using System;
using BubbleApi;
using UnityEngine;
using UnityEngine.UI;


public class ScalePanel : MonoBehaviour
{
    [SerializeField] bool mode;
    [SerializeField] GameObject scale;

    private Image image;
    private RectTransform rectTransform;

    void Start()
    {
        this.image = this.scale.GetComponent<Image>();
        this.rectTransform = this.scale.GetComponent<RectTransform>();

        //this.SetValue(0.5f);
        //this.SetColor(new Color(0, 199, 255));
    }

    private void Update()
    {
        double v;

        if (this.mode)
            v = Math.Sin(GlobalStorage.storage.timer.ticks / 10d);
        else
            v = Math.Cos(GlobalStorage.storage.timer.ticks / 10d);

        this.SetValue((v + 1) / 2d);
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
