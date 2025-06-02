using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class ColorController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float colorResetDelay = 1f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Light2D light2D;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        light2D = GetComponentInChildren<Light2D>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;

        // Işık başta kapalı olsun, sadece renk değişiminde açılacak
        if (light2D != null)
            light2D.enabled = false;
    }

    public void ChangeToRandomColor()
    {
        if (spriteRenderer == null)
            return;

        StopAllCoroutines();

        Color randomColor = GetRandomColor();
        spriteRenderer.color = randomColor;

        // Eğer Light2D varsa, ışığın rengini de değiştir ve aç
        if (light2D != null)
        {
            light2D.color = randomColor;
            light2D.enabled = true;
        }

        StartCoroutine(BackToOriginalColor());
    }

    private IEnumerator BackToOriginalColor()
    {
        yield return new WaitForSeconds(colorResetDelay);

        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;

        if (light2D != null)
            light2D.enabled = false;
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}