using UnityEngine;
using UnityEngine.Events;

public abstract class BaseScreen : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent onShow;
    public UnityEvent onHide;

    protected CanvasGroup canvasGroup;
    protected Animator animator;
    protected bool isShowing;

    // Nombres de los parámetros del animator
    protected const string SHOW_TRIGGER = "Show";
    protected const string HIDE_TRIGGER = "Hide";

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        animator = GetComponent<Animator>();
        
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        // Inicialmente oculto
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        isShowing = false;
    }

    public virtual void Show()
    {
        if (isShowing) return;
        isShowing = true;
        
        // Preparar para mostrar
        gameObject.SetActive(true);
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        // Activar animación si existe animator
        if (animator != null)
        {
            animator.SetTrigger(SHOW_TRIGGER);
        }

        onShow?.Invoke();
    }

    public virtual void Hide()
    {
        if (!isShowing) return;
        isShowing = false;

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        // Activar animación si existe animator
        if (animator != null)
        {
            animator.SetTrigger(HIDE_TRIGGER);
        }
        else
        {
            // Si no hay animator, ocultar inmediatamente
            FinishHide();
        }

        onHide?.Invoke();
    }

    // Este método debe ser llamado al final de la animación de ocultamiento
    public virtual void FinishHide()
    {
        gameObject.SetActive(false);
        canvasGroup.alpha = 0f;
    }

    public virtual void Toggle()
    {
        if (isShowing)
            Hide();
        else
            Show();
    }
} 