using UnityEngine;

// [RequireComponent(typeof(MeshRenderer))]
public class Ground : ScrollingObject
{
    private MeshRenderer GroundMeshRenderer;

    protected override void Awake()
    {
        base.Awake();
        GroundMeshRenderer = GetComponent<MeshRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        // Manejo específico para el cielo si es necesario
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x ;
        GroundMeshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }

}
