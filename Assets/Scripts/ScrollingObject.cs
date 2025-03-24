using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public abstract class ScrollingObject : MonoBehaviour
{
    protected MeshRenderer meshRenderer;
    protected float scrollSpeed = 1f;

    protected virtual void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    protected virtual void Update()
    {
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x;
        meshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }

    public virtual void SetScrollSpeed(float speed)
    {
        scrollSpeed = speed;
    }
}