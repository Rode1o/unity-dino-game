using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectablePopup : BaseScreen
{
    [Header("UI References")]
    public Image collectableIcon;
    public float autoHideDelay = 2f;

    private Coroutine autoHideCoroutine;

    public void Initialize(Sprite sprite)
    {
        if (collectableIcon != null)
            collectableIcon.sprite = sprite;
    }

    public override void Show()
    {
        // Cancelar el auto-hide anterior si existe
        if (autoHideCoroutine != null)
        {
            StopCoroutine(autoHideCoroutine);
        }

        base.Show();

        // Iniciar nuevo auto-hide
        autoHideCoroutine = StartCoroutine(AutoHideCoroutine());
    }

    private IEnumerator AutoHideCoroutine()
    {
        yield return new WaitForSeconds(autoHideDelay);
        Hide();
    }

    public override void Hide()
    {
        if (autoHideCoroutine != null)
        {
            StopCoroutine(autoHideCoroutine);
            autoHideCoroutine = null;
        }

        base.Hide();
    }

    protected override void Awake()
    {
        base.Awake();
        
        // Asegurarse de que tenemos todas las referencias necesarias
        if (collectableIcon == null)
            Debug.LogError("CollectablePopup: Falta referencia a collectableIcon");
    }
} 