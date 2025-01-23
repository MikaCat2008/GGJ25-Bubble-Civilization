using UnityEngine;


public enum BubbleType
{
    Locked,
    Accesible,
    Inaccesible
}

public class MapBubble : MonoBehaviour
{
    private BubbleType type;

    void Start()
    {
        this.SetBubbleType(BubbleType.Locked);
    }

    void Update()
    {
        
    }

    public void SetBubbleType(BubbleType type)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (type == BubbleType.Locked)
            spriteRenderer.color = new Color(150, 150, 150, 100);
        else if (type == BubbleType.Accesible)
            spriteRenderer.color = new Color(20, 200, 20, 100);
        else if (type == BubbleType.Inaccesible)
            spriteRenderer.color = new Color(200, 20, 20, 100);

        this.type = type;
    }
}
