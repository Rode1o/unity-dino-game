using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Sky : ScrollingObject
{
    private MeshRenderer SkyMeshRenderer;

    protected override void Awake()
    {
        base.Awake();
        SkyMeshRenderer = GetComponent<MeshRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        // Manejo específico para el cielo si es necesario
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x;
        SkyMeshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }

}
