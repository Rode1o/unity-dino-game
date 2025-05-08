using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{    public float parallaxFactor = 0.5f;
    public float tileWidth = 10f; // Asigna el ancho real de tu tile, en unidades del mundo

    public float extraOffset = 2.0f;
    private Transform[] tiles;

    void Start()
    {
        int count = transform.childCount;
        tiles = new Transform[count];
        for (int i = 0; i < count; i++)
            tiles[i] = transform.GetChild(i);
    }

    void Update()
    {
        float move = GameManager.Instance.gameSpeed * parallaxFactor * Time.deltaTime;
        transform.position += Vector3.left * move;

        // Bordes de la cámara
        float cameraLeft = Camera.main.transform.position.x - (Camera.main.orthographicSize * Camera.main.aspect) - extraOffset;

        for (int i = 0; i < tiles.Length; i++)
        {
            // Verifica si el tile salió de la pantalla completamente (ajusta el criterio según tus necesidades)
            if (tiles[i].position.x + tileWidth / 2 < cameraLeft)
            {
                // Encuentra la posición del tile más a la derecha
                float maxRight = float.MinValue;
                for (int j = 0; j < tiles.Length; j++)
                {
                    float rightEdge = tiles[j].position.x + tileWidth / 2;
                    if (rightEdge > maxRight)
                        maxRight = rightEdge;
                }

                // Coloca este tile justo después del más a la derecha
                tiles[i].position = new Vector3(maxRight + tileWidth, tiles[i].position.y, tiles[i].position.z);
            }
        }
    }
}
