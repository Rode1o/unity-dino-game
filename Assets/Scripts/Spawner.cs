using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    [System.Serializable]
    public struct CollectableInfo
    {
        public GameObject prefab;
        public Sprite uiSprite; // Sprite que se mostrará en el popup
    }

    [Header("Dangerous Objects")]
    public SpawnableObject[] dangerousObjects;

    [Header("Collectables")]
    public CollectableInfo[] collectables;
    private HashSet<int> collectedItems = new HashSet<int>();
    private GameObject currentCollectable;
    private int currentCollectableIndex = -1;

    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    [Header("Spawn Height Settings")]
    public float minHeight = -5f;
    public float maxHeight = 5f;

    [Header("Collectable Spawn Settings")]
    public float collectableTimeout = 10f;
    public float nextCollectableDelay = 2f;
    public float collectableMinHeight = -3f;
    public float collectableMaxHeight = 3f;

    private void Start()
    {
        // Iniciar el sistema
        ResetCollectableSystem();
        // Iniciar spawns
        InvokeRepeating(nameof(Spawn), 0f, Random.Range(minSpawnRate, maxSpawnRate));
        // Spawn primer coleccionable
        SpawnNextCollectable();
    }

    private void OnDisable()
    {
        CancelInvoke();
        if (currentCollectable != null)
        {
            Destroy(currentCollectable);
        }
    }

    private void ResetCollectableSystem()
    {
        collectedItems.Clear();
        if (currentCollectable != null)
        {
            Destroy(currentCollectable);
        }
        currentCollectable = null;
        currentCollectableIndex = -1;
        CancelInvoke(nameof(TimeoutCollectable));
        CancelInvoke(nameof(SpawnNextCollectable));
    }

    private void Spawn()
    {
        float spawnChance = Random.value;

        foreach (var obj in dangerousObjects)
        {
            if (spawnChance < obj.spawnChance)
            {
                Vector3 randomPosition = transform.position;
                randomPosition.y = Random.Range(minHeight, maxHeight);

                GameObject obstacle = Instantiate(obj.prefab);
                obstacle.transform.position += randomPosition;
                break;
            }

            spawnChance -= obj.spawnChance;
        }
    }

    private void SpawnNextCollectable()
    {
        // Si ya hay un coleccionable, destruirlo
        if (currentCollectable != null)
        {
            Destroy(currentCollectable);
            currentCollectable = null;
        }

        // Si ya se recolectaron todos, no hacer nada
        if (collectedItems.Count >= collectables.Length)
        {
            Debug.Log("Todos los coleccionables han sido recolectados!");
            return;
        }

        // Obtener coleccionables disponibles
        List<int> availableCollectables = new List<int>();
        for (int i = 0; i < collectables.Length; i++)
        {
            if (!collectedItems.Contains(i))
            {
                availableCollectables.Add(i);
            }
        }

        // Seleccionar uno aleatorio
        int randomIndex = Random.Range(0, availableCollectables.Count);
        currentCollectableIndex = availableCollectables[randomIndex];

        // Crear el coleccionable
        Vector3 spawnPosition = transform.position;
        spawnPosition.y = Random.Range(collectableMinHeight, collectableMaxHeight);

        currentCollectable = Instantiate(collectables[currentCollectableIndex].prefab);
        currentCollectable.transform.position += spawnPosition;

        // Configurar el tracker
        CollectableTracker tracker = currentCollectable.AddComponent<CollectableTracker>();
        tracker.Initialize(this, currentCollectableIndex);

        // Programar el timeout
        Invoke(nameof(TimeoutCollectable), collectableTimeout);

        Debug.Log($"Spawneado coleccionable {currentCollectableIndex}");
    }

    private void TimeoutCollectable()
    {
        Debug.Log($"Timeout del coleccionable {currentCollectableIndex}");
        if (currentCollectable != null)
        {
            Destroy(currentCollectable);
            currentCollectable = null;
        }
        
        // Programar siguiente spawn
        Invoke(nameof(SpawnNextCollectable), nextCollectableDelay);
    }

    public void OnCollectableCollected(int collectableIndex)
    {
        Debug.Log($"Coleccionable {collectableIndex} recolectado!");
        
        // Mostrar el popup con la información del coleccionable
        CollectableInfo info = collectables[collectableIndex];
        UIManager.Instance.ShowCollectablePopup(info.uiSprite, collectableIndex);

        collectedItems.Add(collectableIndex);
        currentCollectable = null;
        currentCollectableIndex = -1;
        
        // Cancelar el timeout actual
        CancelInvoke(nameof(TimeoutCollectable));
        
        // Programar siguiente spawn
        Invoke(nameof(SpawnNextCollectable), nextCollectableDelay);
    }
}
