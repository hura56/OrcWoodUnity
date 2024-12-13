using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Image damageFlash;
    [SerializeField] private float flashDuration = 0.5f;
    [SerializeField] private Color flashColor = new Color(1, 0, 0, 0.5f);

    private Coroutine flashCoroutine;

    public void TriggerFlash()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }
        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        damageFlash.color = flashColor;
        float elapsed = 0f;

        while (elapsed < flashDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(flashColor.a, 0, elapsed / flashDuration);
            damageFlash.color = new Color(flashColor.r, flashColor.g, flashColor.b, alpha);
            yield return null;
        }
        
        damageFlash.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0);
        flashCoroutine = null;
    }
}
