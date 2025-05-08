using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public RectTransform fillImageRect; // RectTransform de la imagen de "fill"
    private int totalCollectables = 10; // Número total de coleccionables esperados
    private int collected = 0;          // Contador de coleccionables recolectados

    private void Start()
    {
        // Opcional: Inicializar si es necesario, usando valores por defecto.
        Initialize(totalCollectables);
    }

    public void Initialize(int total)
    {
        totalCollectables = total;
        collected = 0;
        UpdateProgressBar();
    }

    public void Collect()
    {
        if (collected < totalCollectables)
        {
            collected++;
            UpdateProgressBar();
        }
    }

    private void UpdateProgressBar()
    {
        // Calcula el progreso como un valor entre 0 y 1
        float progress = (float)collected / totalCollectables;
        
        // Usa el progreso para actualizar la escala X de la imagen de "fill"
        if (fillImageRect != null)
        {
            fillImageRect.localScale = new Vector3(progress, 1, 1);
        }
    }
}