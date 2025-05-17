using UnityEngine;

public class CollectableTracker : MonoBehaviour
{
    private Spawner spawner;
    private int collectableIndex;

    public void Initialize(Spawner spawnerReference, int index)
    {
        spawner = spawnerReference;
        collectableIndex = index;
    }

    // Este método debe ser llamado cuando el jugador recoge el coleccionable
    public void OnCollected()
    {
        spawner.OnCollectableCollected(collectableIndex);
        Destroy(gameObject);
    }
} 