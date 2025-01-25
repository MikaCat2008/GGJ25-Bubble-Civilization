using System.Collections;
using UnityEngine;

public class MenuTitleAnimation : MonoBehaviour
{

    public float animationDelay = 2.2f;
    private WaitForSeconds animationDelayWFS;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        
        animationDelayWFS = new WaitForSeconds(animationDelay);
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1, 1, 1, 0f);

        StartCoroutine(Appear());
    }

    IEnumerator Appear()
    {
        yield return animationDelayWFS;
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        //animating
        int Step = 40;
        for (int i = 0; i <= Step; i++)
        {
            float newAlpha= (float)i / (float)Step;
            spriteRenderer.color = new Color(1, 1, 1, newAlpha);
            yield return new WaitForSeconds(0.1f);
        }
        spriteRenderer.color = new Color(1, 1, 1, 1f);
  }
}
