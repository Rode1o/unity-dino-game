using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public RectTransform fillImageRect;      // Imagen de fill de la barra de vida
    public HealthComponent healthComponent;  // Asigna el HealthComponent del Player en el Inspector

    private void Start()
    {
        if (healthComponent != null)
            healthComponent.OnHealthChanged += UpdateHealthBar;
        UpdateHealthBar(healthComponent.currentHealth, healthComponent.maxHealth);
    }

    private void OnDestroy()
    {
        if (healthComponent != null)
            healthComponent.OnHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(float current, float max)
    {
        float progress = (max > 0f) ? current / max : 0f;
        if (fillImageRect != null)
            fillImageRect.localScale = new Vector3(progress, 1, 1);
    }
}
