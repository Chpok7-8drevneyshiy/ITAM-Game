using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Video : MonoBehaviour
{
    void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Получаем размеры вьюпорта камеры в мировых координатах
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Получаем размеры спрайта в мировых координатах
        float spriteHeight = spriteRenderer.sprite.bounds.size.y;
        float spriteWidth = spriteRenderer.sprite.bounds.size.x;

        // Рассчитываем масштаб, необходимый для соответствия размеров спрайта и камеры
        Vector3 scale = transform.localScale;
        scale.x = camWidth / spriteWidth;
        scale.y = camHeight / spriteHeight;
        transform.localScale = scale;
    }
}
