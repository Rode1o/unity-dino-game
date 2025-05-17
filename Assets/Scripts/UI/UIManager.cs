using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Screens")]
    public CollectablePopup collectablePopupPrefab;
    public CollectionPanel collectionPanel;
    
    private Dictionary<System.Type, BaseScreen> screens = new Dictionary<System.Type, BaseScreen>();
    private Canvas mainCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        mainCanvas = GetComponentInChildren<Canvas>();
        if (mainCanvas == null)
        {
            Debug.LogError("UIManager necesita un Canvas como hijo!");
            return;
        }

        // Registrar todas las pantallas que son hijas del canvas
        foreach (BaseScreen screen in mainCanvas.GetComponentsInChildren<BaseScreen>(true))
        {
            RegisterScreen(screen);
        }

        // Mostrar el panel de colección
        if (collectionPanel != null)
        {
            collectionPanel.Show();
        }
    }

    public void RegisterScreen(BaseScreen screen)
    {
        if (!screens.ContainsKey(screen.GetType()))
        {
            screens.Add(screen.GetType(), screen);
        }
    }

    public T GetScreen<T>() where T : BaseScreen
    {
        if (screens.TryGetValue(typeof(T), out BaseScreen screen))
        {
            return screen as T;
        }
        return null;
    }

    public void ShowCollectablePopup(Sprite collectableSprite, int collectableIndex)
    {
        // Mostrar el popup
        CollectablePopup popup = Instantiate(collectablePopupPrefab, mainCanvas.transform);
        popup.Initialize(collectableSprite);
        popup.Show();

        // Actualizar el panel de colección
        if (collectionPanel != null)
        {
            collectionPanel.OnItemCollected(collectableIndex, collectableSprite);
        }
    }

    public void ShowScreen<T>(bool hideOthers = false) where T : BaseScreen
    {
        if (hideOthers)
        {
            HideAllScreens();
        }

        T screen = GetScreen<T>();
        if (screen != null)
        {
            screen.Show();
        }
    }

    public void HideScreen<T>() where T : BaseScreen
    {
        T screen = GetScreen<T>();
        if (screen != null)
        {
            screen.Hide();
        }
    }

    public void HideAllScreens()
    {
        foreach (var screen in screens.Values)
        {
            screen.Hide();
        }
    }

    public void ResetCollection()
    {
        if (collectionPanel != null)
        {
            collectionPanel.ResetCollection();
        }
    }
} 