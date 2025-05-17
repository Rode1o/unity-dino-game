using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CollectionPanel : BaseScreen
{
    [Header("Collection Slots")]
    public Image[] collectionSlots;  // Array de las 4 imágenes
    
    [Header("Visual Settings")]
    public Color unlockedColor = Color.white;
    public Color lockedColor = Color.gray;

    private HashSet<int> collectedItems = new HashSet<int>();

    protected override void Awake()
    {
        base.Awake();
        InitializeSlots();
    }

    private void InitializeSlots()
    {
        foreach (var slot in collectionSlots)
        {
            // Configurar el estado inicial de cada slot
            if (slot != null)
            {
                slot.color = lockedColor;
            }
        }
    }

    public void OnItemCollected(int collectableIndex, Sprite collectableSprite)
    {
        if (collectableIndex >= 0 && collectableIndex < collectionSlots.Length && !collectedItems.Contains(collectableIndex))
        {
            UnlockSlot(collectableIndex, collectableSprite);
            collectedItems.Add(collectableIndex);
            
            // Verificar si hemos completado la colección
            if (collectedItems.Count >= collectionSlots.Length)
            {
                OnCollectionComplete();
            }
        }
    }

    private void UnlockSlot(int slotIndex, Sprite collectableSprite)
    {
        if (collectionSlots[slotIndex] != null)
        {
            collectionSlots[slotIndex].sprite = collectableSprite;
            collectionSlots[slotIndex].color = unlockedColor;
        }

        // Aquí puedes agregar una animación usando el Animator si lo deseas
        if (animator != null)
        {
            animator.SetTrigger("Unlock_" + slotIndex);
        }
    }

    private void OnCollectionComplete()
    {
        Debug.Log("¡Colección completada!");
        
        // Notificar al GameManager para dar la recompensa
        GameManager.Instance.OnCollectionComplete();
    }

    public void ResetCollection()
    {
        collectedItems.Clear();
        InitializeSlots();
    }
} 