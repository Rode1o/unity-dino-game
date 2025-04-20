using UnityEngine;

public class GameEntity : MonoBehaviour
{
    protected float leftEdge;

    protected virtual void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }

    protected virtual void Update() 
    {
        transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}

public class Obstacle : GameEntity
{
}

public class Collectable : GameEntity
{
    
}
