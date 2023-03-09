using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float speed;
    private Vector2 StartPosition;
    private RectTransform _rectTransform;

    private void Start()
    {
        speed = 4f;
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed*Time.deltaTime);
        if (_rectTransform.anchoredPosition.x <= -568.0f)
        {
            _rectTransform.anchoredPosition += new Vector2(_rectTransform.rect.width, 0f);
        }
    }
}
